using System.Collections.Generic;
using UnityEngine;

/* A simple representation of a triangle */
public class Triangle
{
    public List<Vector3> vertices;
    public List<Vector3> normals;
    public List<Vector2> uvs;
    public int submeshIndex;

    public Triangle(Vector3[] v, Vector3[] n, Vector2[] u, int s)
    {
        vertices = new List<Vector3>(v);
        normals = new List<Vector3>(n);
        uvs = new List<Vector2>(u);

        submeshIndex = s;
    }
}
