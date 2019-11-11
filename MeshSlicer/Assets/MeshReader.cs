using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshReader : MonoBehaviour
{
    private Mesh theMesh;
    private MeshFilter theMeshFilter;
    void Start()
    {
        theMesh = GetComponent<Mesh>();
        theMeshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {

        //theMeshFilter.mesh.Optimize
    }
}
