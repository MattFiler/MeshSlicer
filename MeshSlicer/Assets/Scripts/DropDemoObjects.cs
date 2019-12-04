using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDemoObjects : MonoBehaviour
{
    [SerializeField] private Rigidbody ObjectOne;
    [SerializeField] private Rigidbody ObjectTwo;
    [SerializeField] private KeyCode TriggerKey;

    /* Disable gravity as default */
    void Start()
    {
        ObjectOne.useGravity = false;
        ObjectTwo.useGravity = false;
    }
    
    /* Enable gravity on button press */
    void Update()
    {
        if (Input.GetKeyDown(TriggerKey) && !ObjectOne.useGravity)
        {
            ObjectOne.useGravity = true;
            ObjectTwo.useGravity = true;
        }
    }
}
