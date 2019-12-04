using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;
    private float modifier = 1;

    /* Basic player movement for PC/VR */
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            modifier = 3.0f;
        }
        else
        {
            modifier = 1.0f;
        }

        if (OVRInput.Get(OVRInput.Button.Up) || Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * modifier;
        }
        if (OVRInput.Get(OVRInput.Button.Down) || Input.GetKey(KeyCode.S))
        {
            this.transform.position -= this.transform.forward * Time.deltaTime * modifier;
        }
        if (OVRInput.Get(OVRInput.Button.Left) || Input.GetKey(KeyCode.A))
        {
            this.transform.position -= this.transform.right * Time.deltaTime * modifier;
        }
        if (OVRInput.Get(OVRInput.Button.Right) || Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * Time.deltaTime * modifier;
        }

        //If no controllers are connected, we're probs not in VR, so look around with mouse
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.All))
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * speed;
        }
    }
}
