using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeshCutterManager : MonoSingleton<MeshCutterManager>
{
    private List<GameObject> CutTracker = new List<GameObject>();

    /* Damage a GameObject's mesh by an impact force */
    public void DamageMesh(GameObject toDamage, float impactForce)
    {
        CutTracker.Clear();
        if (!toDamage.GetComponent<ObjectMaterial>()) return;

        MaterialTypes materialType = toDamage.GetComponent<ObjectMaterial>().MaterialType;
        if (!ObjectMaterialManager.Instance.CanMaterialBreak(materialType)) return;
        if (ObjectMaterialManager.Instance.GetMaterialStrength(materialType) > impactForce) return;

        int ShatterAmount = (int)((((ObjectMaterialManager.Instance.GetMaterialDensity(materialType) - 100.0f) * -1.0f) / 10.0f) * (impactForce / 10.0f));
        Debug.Log("GameObject " + toDamage.name + " damaged! Shattering " + ShatterAmount + " times.");
        StartCoroutine(RecursivelyCutCoroutine(toDamage, ShatterAmount));
    }

    /* Recursively cut a GameObject's mesh (call RecursivelyCutCoroutine to cut once, then coroutine out down each of those two new meshes) */
    private IEnumerator RecursivelyCutCoroutine(GameObject toCut, int MaxCuts)
    {
        GameObject toCut2 = RandomCut(toCut); //This effectively halves our realtime workload

        StartCoroutine(RecursivelyCutSubCoroutine(toCut, MaxCuts / 2));
        StartCoroutine(RecursivelyCutSubCoroutine(toCut2, MaxCuts / 2));

        yield return new WaitForEndOfFrame();
    }
    private IEnumerator RecursivelyCutSubCoroutine(GameObject toCut, int MaxCuts)
    {
        RecursivelyCut(toCut, MaxCuts);
        yield return new WaitForEndOfFrame();
    }
    private void RecursivelyCut(GameObject toCut, int MaxCuts)
    {
        if (CutTracker.Count >= MaxCuts) return;
        if (!CutTracker.Contains(toCut)) CutTracker.Add(toCut);
        
        List<GameObject> NewEntries = new List<GameObject>();
        foreach (GameObject cutEntry in CutTracker)
        {
            if (cutEntry == null) continue;
            NewEntries.Add(RandomCut(cutEntry));
        }
        CutTracker.AddRange(NewEntries);

        RecursivelyCut(toCut, MaxCuts);
    }

    /* Perform a random cut on a mesh */
    private GameObject RandomCut(GameObject toCut)
    {
        List<Vector3> VertOut = new List<Vector3>();
        toCut.GetComponent<MeshFilter>().mesh.GetVertices(VertOut);
        if (VertOut.Count == 0) return null;
        Vector3 VertToUse = VertOut[Random.Range(0, VertOut.Count)];
        return MeshCutter.Instance.Cut(toCut, toCut.transform.TransformPoint(VertToUse), new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
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
