using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCutInteraction : MonoBehaviour
{
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
                if (Input.GetKey(KeyCode.C))
                {
                    MeshCutter.Instance.Cut(hit.collider.gameObject, hit.point, Camera.main.transform.up);
                }
                else
                {
                    MeshCutterManager.Instance.DamageMesh(hit.collider.gameObject);
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
        }

        //Mouse interaction lock
        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
