using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRL : MonoBehaviour
{
    Vector3[] newVertices = new Vector3[100];
    Vector2[] newUV = new Vector2 [100];
    int[] newTriangles = new int[3*100];

    void Start()
    {
        for (int i = 0; i < 100; i++) {
            newVertices[i] = new Vector3(0,0,0); 
        }
        for (int i = 0; i < 100; i++)
        {
            newUV[i] = new Vector2( 0, 0);
        }
        for (int i = 0; i < 100; i++)
        {
            newTriangles[i * 3 + 0] = 0;
            newTriangles[i * 3 + 1] = 1;
            newTriangles[i * 3 + 2] = 1;
        }

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
  
    }
    void Update()
    {


    }

}
