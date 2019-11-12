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
                DeleteTriangle(hit.triangleIndex, hit.transform.gameObject);
            }

            laserLineRenderer.SetPosition(0, ray.origin);
            laserLineRenderer.SetPosition(1, ray.origin + (1000.0f * ray.direction));
            laserLineRenderer.enabled = true;
        }

        //VR point interaction
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            RaycastHit hit;
            Ray ray = new Ray(vrHand.transform.position, vrHand.transform.forward);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                DeleteTriangle(hit.triangleIndex, hit.transform.gameObject);
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
