using System.Collections.Generic;
using UnityEngine;

/* A way to hold mesh data separate to Unity's mesh object */
public class MeshData
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<List<int>> submeshIndices = new List<List<int>>();

    /* Add a triangle object to this mesh holder */
    public void AddTriangle(Triangle triangle)
    {
        vertices.AddRange(triangle.vertices);
        normals.AddRange(triangle.normals);
        uvs.AddRange(triangle.uvs);

        //Make sure the submesh indices array is the right size
        if (submeshIndices.Count < triangle.submeshIndex + 1)
        {
            for (int i = submeshIndices.Count; i < triangle.submeshIndex + 1; i++)
            {
                submeshIndices.Add(new List<int>());
            }
        }

        //Update the index count for this triangle's submesh
        for (int i = 0; i < 3; i++)
        {
            submeshIndices[triangle.submeshIndex].Add(vertices.Count - triangle.vertices.Count + i);
        }
    }
    
    /* Convert this mesh holder into a Uunity mesh object */
    public Mesh AsUnityMesh()
    {
        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.SetUVs(1, uvs);

        //Populate indicies appropriately by submesh
        mesh.subMeshCount = submeshIndices.Count;
        for (int i = 0; i < submeshIndices.Count; i++)
        {
            mesh.SetTriangles(submeshIndices[i], i);
        }
        return mesh;
    }
}
