using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCutterManager : MonoSingleton<MeshCutterManager>
{
    private List<GameObject> CutTracker = new List<GameObject>();

    /* Damage a GameObject's mesh */
    public void DamageMesh(GameObject toDamage)
    {
        CutTracker.Clear();
        if (!toDamage.GetComponent<ObjectMaterial>()) return;

        MaterialTypes materialType = toDamage.GetComponent<ObjectMaterial>().MaterialType;
        if (!ObjectMaterialManager.Instance.CanMaterialBreak(materialType)) return;

        int ShatterAmount = ((ObjectMaterialManager.Instance.GetMaterialDensity(materialType) - 100) * -1) / 10; //modulate this by the impact force
        RecursivelyCut(toDamage, ShatterAmount);
    }

    /* Recursively cut a GameObject's mesh */
    private void RecursivelyCut(GameObject toCut, int MaxCuts)
    {
        if (CutTracker.Count >= MaxCuts) return;
        if (!CutTracker.Contains(toCut)) CutTracker.Add(toCut);

        List<GameObject> NewEntries = new List<GameObject>();
        foreach (GameObject cutEntry in CutTracker)
        {
            if (cutEntry == null) continue;
            List<Vector3> VertOut = new List<Vector3>();
            cutEntry.GetComponent<MeshFilter>().mesh.GetVertices(VertOut);
            if (VertOut.Count == 0) continue;
            Vector3 VertToUse = VertOut[Random.Range(0, VertOut.Count)];
            NewEntries.Add(MeshCutter.Instance.Cut(cutEntry, cutEntry.transform.TransformPoint(VertToUse), new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
        }
        CutTracker.AddRange(NewEntries);

        RecursivelyCut(toCut, MaxCuts);
    }

    /* Delete a triangle at a given index in a given GameObject's mesh */
    private void DeleteTriangle(int index, GameObject obj)
    {
        if (!obj.GetComponent<MeshCollider>()) return;

        Destroy(obj.GetComponent<MeshCollider>());
        Mesh mesh = obj.transform.GetComponent<MeshFilter>().mesh;
        int[] oldTriangles = mesh.triangles;
        int[] newTriangles = new int[mesh.triangles.Length - 3];

        int i = 0;
        int j = 0;
        while (j < mesh.triangles.Length)
        {
            if (j != index * 3)
            {
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
            }
            else
            {
                j += 3;
            }
        }

        obj.transform.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
        obj.AddComponent<MeshCollider>();
    }
}
