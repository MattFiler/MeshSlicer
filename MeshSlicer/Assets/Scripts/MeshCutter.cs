using System.Collections.Generic;
using UnityEngine;

/* The side this triangle is on when we cut the mesh */
public enum TriangleSide
{
    LEFT,
    RIGHT,
    MIXED
}

public class MeshCutter : MonoSingleton<MeshCutter>
{
    private bool currentlyCutting = false;
    private Mesh originalMesh = null;

    /* Split a given game object's mesh into left/right sides, fill the middle, and put back to the world */
    public void Cut(GameObject cutObject, Vector3 cutPoint, Vector3 cutDirection)
    {
        if (currentlyCutting) return;
        currentlyCutting = true;
        originalMesh = cutObject.GetComponent<MeshFilter>().mesh;

        //Create a plane to cut the game object with
        Plane cutPlane = new Plane(cutObject.transform.InverseTransformDirection(-cutDirection), cutObject.transform.InverseTransformPoint(cutPoint));

        //Iterate over each submesh and group triangles by left/right side of plane
        List<Vector3> newVerts = new List<Vector3>();
        MeshData leftMesh = new MeshData();
        MeshData rightMesh = new MeshData();
        for (int i = 0; i < originalMesh.subMeshCount; i++)
        {
            int[] submeshIndices = originalMesh.GetTriangles(i);
            for (int j = 0; j < submeshIndices.Length; j += 3)
            {
                //Create a representation of the current triangle
                List<Vector3> vertices = new List<Vector3>();
                List<Vector3> normals = new List<Vector3>();
                List<Vector2> uvs = new List<Vector2>();
                List<TriangleSide> sides = new List<TriangleSide>();
                for (int x = 0; x < 3; x++)
                {
                    int thisIndex = submeshIndices[j + x];

                    vertices.Add(originalMesh.vertices[thisIndex]);
                    normals.Add(originalMesh.normals[thisIndex]);
                    uvs.Add(originalMesh.uv[thisIndex]);

                    //Work out what side of the cutting plane this vert is on
                    sides.Add(cutPlane.GetSide(originalMesh.vertices[thisIndex]) ? TriangleSide.LEFT : TriangleSide.RIGHT);
                }
                Triangle currentTriangle = new Triangle(vertices.ToArray(), normals.ToArray(), uvs.ToArray(), i);

                //Work out if we're entirely on the left, right, or middle
                TriangleSide cumulativeSide = TriangleSide.LEFT;
                for (int x = 0; x < 3; x++)
                {
                    if (x > 0 && cumulativeSide != sides[x])
                    {
                        cumulativeSide = TriangleSide.MIXED;
                        break;
                    }
                    cumulativeSide = sides[x];
                }

                //Add the triangle to the appropriate side, or split it if it's across both
                switch (cumulativeSide)
                {
                    case TriangleSide.LEFT:
                        leftMesh.AddTriangle(currentTriangle);
                        break;
                    case TriangleSide.RIGHT:
                        rightMesh.AddTriangle(currentTriangle);
                        break;
                    case TriangleSide.MIXED:
                        CutTriangle(cutPlane, currentTriangle, sides, leftMesh, rightMesh, newVerts);
                        break;
                }
            }
        }

        //Fill across the cut within the mesh
        FillCut(newVerts, cutPlane, leftMesh, rightMesh);

        //Our original game object's mesh becomes the left mesh
        Mesh leftMeshFinal = leftMesh.AsUnityMesh();
        cutObject.GetComponent<MeshFilter>().mesh = leftMeshFinal;
        cutObject.AddComponent<MeshCollider>().sharedMesh = leftMeshFinal;
        cutObject.GetComponent<MeshCollider>().convex = true;

        //Update materials in the original mesh
        Material[] newMaterials = new Material[leftMeshFinal.subMeshCount];
        for (int i = 0; i < leftMeshFinal.subMeshCount; i++)
        {
            newMaterials[i] = cutObject.GetComponent<MeshRenderer>().material;
        }
        cutObject.GetComponent<MeshRenderer>().materials = newMaterials;

        //Create a new game object to apply the right mesh to
        GameObject offcutObject = new GameObject();
        offcutObject.transform.position = cutObject.transform.position + (Vector3.up * .05f);
        offcutObject.transform.rotation = cutObject.transform.rotation;
        offcutObject.transform.localScale = cutObject.transform.localScale;
        offcutObject.AddComponent<MeshRenderer>();
        offcutObject.AddComponent<Rigidbody>();

        //The new game object gets our right mesh
        Mesh finishedRightMesh = rightMesh.AsUnityMesh();
        offcutObject.AddComponent<MeshFilter>().mesh = finishedRightMesh;
        offcutObject.AddComponent<MeshCollider>().sharedMesh = finishedRightMesh;
        offcutObject.GetComponent<MeshCollider>().convex = true;

        //Set materials in our new right mesh
        newMaterials = new Material[finishedRightMesh.subMeshCount];
        for (int i = 0; i < finishedRightMesh.subMeshCount; i++)
        {
            newMaterials[i] = cutObject.GetComponent<MeshRenderer>().material;
        }
        offcutObject.GetComponent<MeshRenderer>().materials = newMaterials;

        //Done!
        currentlyCutting = false;
    }

    /* If a triangle crosses the left/right side of the plane, cut it into half (3 triangles) */
    private void CutTriangle(Plane cutPlane, Triangle triangle, List<TriangleSide> sides, MeshData leftMesh, MeshData rightMesh, List<Vector3> newVerts)
    {
        //We will create new triangles on the left/right
        Triangle leftTri = new Triangle(new Vector3[2], new Vector3[2], new Vector2[2], triangle.submeshIndex);
        Triangle rightTri = new Triangle(new Vector3[2], new Vector3[2], new Vector2[2], triangle.submeshIndex);

        //Work out the verts/norms/uvs we should use from the existing triangle for left/right
        bool doneLeft = false;
        bool doneRight = false;
        for (int i = 0; i < 3; i++)
        {
            switch (sides[i])
            {
                case TriangleSide.LEFT:
                    if (!doneLeft)
                    {
                        doneLeft = true;

                        leftTri.vertices[0] = triangle.vertices[i];
                        leftTri.vertices[1] = leftTri.vertices[0];

                        leftTri.uvs[0] = triangle.uvs[i];
                        leftTri.uvs[1] = leftTri.uvs[0];

                        leftTri.normals[0] = triangle.normals[i];
                        leftTri.normals[1] = leftTri.normals[0];

                        break;
                    }
                    leftTri.vertices[1] = triangle.vertices[i];
                    leftTri.normals[1] = triangle.normals[i];
                    leftTri.uvs[1] = triangle.uvs[i];
                    break;
                case TriangleSide.RIGHT:
                    if (!doneRight)
                    {
                        doneRight = true;

                        rightTri.vertices[0] = triangle.vertices[i];
                        rightTri.vertices[1] = rightTri.vertices[0];

                        rightTri.uvs[0] = triangle.uvs[i];
                        rightTri.uvs[1] = rightTri.uvs[0];

                        rightTri.normals[0] = triangle.normals[i];
                        rightTri.normals[1] = rightTri.normals[0];

                        break;
                    }
                    rightTri.vertices[1] = triangle.vertices[i];
                    rightTri.normals[1] = triangle.normals[i];
                    rightTri.uvs[1] = triangle.uvs[i];
                    break;
            }
        }
        
        //Work out the new left normal and UV
        float distance;
        cutPlane.Raycast(new Ray(leftTri.vertices[0], (rightTri.vertices[0] - leftTri.vertices[0]).normalized), out distance);

        float normalizedDistance = distance / (rightTri.vertices[0] - leftTri.vertices[0]).magnitude;
        Vector3 vertLeft = Vector3.Lerp(leftTri.vertices[0], rightTri.vertices[0], normalizedDistance);
        newVerts.Add(vertLeft);

        Vector3 normalLeft = Vector3.Lerp(leftTri.normals[0], rightTri.normals[0], normalizedDistance);
        Vector2 uvLeft = Vector2.Lerp(leftTri.uvs[0], rightTri.uvs[0], normalizedDistance);

        //Work out the new right normal and UV
        cutPlane.Raycast(new Ray(leftTri.vertices[1], (rightTri.vertices[1] - leftTri.vertices[1]).normalized), out distance);

        normalizedDistance = distance / (rightTri.vertices[1] - leftTri.vertices[1]).magnitude;
        Vector3 vertRight = Vector3.Lerp(leftTri.vertices[1], rightTri.vertices[1], normalizedDistance);
        newVerts.Add(vertRight);

        Vector3 normalRight = Vector3.Lerp(leftTri.normals[1], rightTri.normals[1], normalizedDistance);
        Vector2 uvRight = Vector2.Lerp(leftTri.uvs[1], rightTri.uvs[1], normalizedDistance);

        //If triangle 1 on left is valid, add it
        Vector3[] updatedVertices = new Vector3[] { leftTri.vertices[0], vertLeft, vertRight };
        Vector3[] updatedNormals = new Vector3[] { leftTri.normals[0], normalLeft, normalRight };
        Vector2[] updatedUVs = new Vector2[] { leftTri.uvs[0], uvLeft, uvRight };

        Triangle currentTriangle = new Triangle(updatedVertices, updatedNormals, updatedUVs, triangle.submeshIndex);
        if (TriangleIsValid(currentTriangle, updatedVertices, updatedNormals)) leftMesh.AddTriangle(currentTriangle);

        //If triangle 2 on left is valid, add it
        updatedVertices = new Vector3[] { leftTri.vertices[0], leftTri.vertices[1], vertRight };
        updatedNormals = new Vector3[] { leftTri.normals[0], leftTri.normals[1], normalRight };
        updatedUVs = new Vector2[] { leftTri.uvs[0], leftTri.uvs[1], uvRight };
        
        currentTriangle = new Triangle(updatedVertices, updatedNormals, updatedUVs, triangle.submeshIndex);
        if (TriangleIsValid(currentTriangle, updatedVertices, updatedNormals)) leftMesh.AddTriangle(currentTriangle);

        //If triangle 1 on right is valid, add it 
        updatedVertices = new Vector3[] { rightTri.vertices[0], vertLeft, vertRight };
        updatedNormals = new Vector3[] { rightTri.normals[0], normalLeft, normalRight };
        updatedUVs = new Vector2[] { rightTri.uvs[0], uvLeft, uvRight };

        currentTriangle = new Triangle(updatedVertices, updatedNormals, updatedUVs, triangle.submeshIndex);
        if (TriangleIsValid(currentTriangle, updatedVertices, updatedNormals)) rightMesh.AddTriangle(currentTriangle);

        //If triangle 2 on right is valid, add it 
        updatedVertices = new Vector3[] { rightTri.vertices[0], rightTri.vertices[1], vertRight };
        updatedNormals = new Vector3[] { rightTri.normals[0], rightTri.normals[1], normalRight };
        updatedUVs = new Vector2[] { rightTri.uvs[0], rightTri.uvs[1], uvRight };

        currentTriangle = new Triangle(updatedVertices, updatedNormals, updatedUVs, triangle.submeshIndex);
        if (TriangleIsValid(currentTriangle, updatedVertices, updatedNormals)) rightMesh.AddTriangle(currentTriangle);
    }

    /* Make sure the triangle isn't degenerate, and flip it if necessary */
    private bool TriangleIsValid(Triangle _triangle, Vector3[] _newVertices, Vector3[] _newNormals)
    {
        if (_newVertices[0] != _newVertices[1] && _newVertices[0] != _newVertices[2])
        {
            if (Vector3.Dot(Vector3.Cross(_newVertices[1] - _newVertices[0], _newVertices[2] - _newVertices[0]), _newNormals[0]) < 0)
            {
                //Flip the triangle
                Vector3 temp = _triangle.vertices[2];
                _triangle.vertices[2] = _triangle.vertices[0];
                _triangle.vertices[0] = temp;

                temp = _triangle.normals[2];
                _triangle.normals[2] = _triangle.normals[0];
                _triangle.normals[0] = temp;

                Vector2 temp2 = _triangle.uvs[2];
                _triangle.uvs[2] = _triangle.uvs[0];
                _triangle.uvs[0] = temp2;
            }
            return true;
        }
        //Degenerate shape
        return false;
    }

    /* Fill across the top of the new cut sections */
    private void FillCut(List<Vector3> newVerts, Plane cutPlane, MeshData leftMesh, MeshData rightMesh)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> meshPoly = new List<Vector3>();

        for (int i = 0; i < newVerts.Count; i++)
        {
            if (!vertices.Contains(newVerts[i]))
            {
                meshPoly.Clear();
                meshPoly.Add(newVerts[i]);
                meshPoly.Add(newVerts[i + 1]);

                vertices.Add(newVerts[i]);
                vertices.Add(newVerts[i + 1]);

                EvaluatePairs(newVerts, vertices, meshPoly);
                Fill(meshPoly, cutPlane, leftMesh, rightMesh);
            }
        }
    }

    /* Evaluate existing verts against new ones, and add new ones if valid */
    private void EvaluatePairs(List<Vector3> newVerts, List<Vector3> vertices, List<Vector3> meshPoly)
    {
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            for (int i = 0; i < newVerts.Count; i += 2)
            {
                if (newVerts[i] == meshPoly[meshPoly.Count - 1] && !vertices.Contains(newVerts[i + 1]))
                {
                    isDone = false;
                    meshPoly.Add(newVerts[i + 1]);
                    vertices.Add(newVerts[i + 1]);
                }
                else if (newVerts[i + 1] == meshPoly[meshPoly.Count - 1] && !vertices.Contains(newVerts[i]))
                {
                    isDone = false;
                    meshPoly.Add(newVerts[i]);
                    vertices.Add(newVerts[i]);
                }
            }
        }
    }

    /* Fill across the top of a set of verts by taking the mid-point and working outwards */
    private void Fill(List<Vector3> existingVerts, Plane cutPlane, MeshData leftMesh, MeshData rightMesh)
    {
        //Calculate the center of all verts to work from
        Vector3 centerPos = Vector3.zero;
        for (int i = 0; i < existingVerts.Count; i++)
        {
            centerPos += existingVerts[i];
        }
        centerPos = centerPos / existingVerts.Count;

        //Calculate the up and left directions of the new plane we're making
        Vector3 up = new Vector3()
        {
            x = cutPlane.normal.x,
            y = cutPlane.normal.y,
            z = cutPlane.normal.z
        };
        Vector3 left = Vector3.Cross(cutPlane.normal, up);

        //Create triangles to fill in the gap
        Vector3 displacement = Vector3.zero;
        Vector2 uv1 = Vector2.zero;
        Vector2 uv2 = Vector2.zero;
        for (int i = 0; i < existingVerts.Count; i++)
        {
            //Calculate uvs
            displacement = existingVerts[i] - centerPos;
            uv1 = new Vector2(0.5f + Vector3.Dot(displacement, left), 0.5f + Vector3.Dot(displacement, up));

            displacement = existingVerts[(i + 1) % existingVerts.Count] - centerPos;
            uv2 = new Vector2(0.5f + Vector3.Dot(displacement, left), 0.5f + Vector3.Dot(displacement, up));

            //For the front side
            Vector3[] vertices = new Vector3[] { existingVerts[i], existingVerts[(i + 1) % existingVerts.Count], centerPos };
            Vector3[] normals = new Vector3[] { -cutPlane.normal, -cutPlane.normal, -cutPlane.normal };
            Vector2[] uvs = new Vector2[] { uv1, uv2, new Vector2(0.5f, 0.5f) };

            Triangle currentTriangle = new Triangle(vertices, normals, uvs, originalMesh.subMeshCount + 1);
            if (TriangleIsValid(currentTriangle, vertices, normals)) leftMesh.AddTriangle(currentTriangle);

            //For the back side
            normals = new Vector3[] { cutPlane.normal, cutPlane.normal, cutPlane.normal };

            currentTriangle = new Triangle(vertices, normals, uvs, originalMesh.subMeshCount + 1);
            if (TriangleIsValid(currentTriangle, vertices, normals)) rightMesh.AddTriangle(currentTriangle);
        }
    }
}