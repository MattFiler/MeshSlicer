using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    private LineRenderer laserLineRenderer;
    private void Start()
    {
        laserLineRenderer = GetComponent<LineRenderer>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = 0.5f;
        laserLineRenderer.endWidth = 0.5f;
    }

    [SerializeField] private GameObject vrHand;
    private List<GameObject> CutTracker = new List<GameObject>();

    /* On PC, allow mouse clicks to interact with a mesh, in VR do it from controller */
    void Update()
    {
        //PC click interaction
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (Input.GetKey(KeyCode.R))
                {
                    CutTracker.Clear();
                    RecursivelyCut(hit.collider.gameObject, 5);
                }
                else
                {
                    MeshCutter.Instance.Cut(hit.collider.gameObject, hit.point, Camera.main.transform.up);
                }
            }
        }

        //VR point interaction
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            RaycastHit hit;
            Ray ray = new Ray(vrHand.transform.position, vrHand.transform.forward);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                MeshCutter.Instance.Cut(hit.collider.gameObject, hit.point, Camera.main.transform.up);
                //DeleteTriangle(hit.triangleIndex, hit.transform.gameObject);
            }

            laserLineRenderer.SetPosition(0, ray.origin);
            laserLineRenderer.SetPosition(1, ray.origin + (1000.0f * ray.direction));
            laserLineRenderer.enabled = true;
        }

        //Mouse interaction lock
        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
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
