﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;

    private void Awake()
    {
       Generate();
    }

    private Vector3[] vertices;

    private void Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++){
            for (int x = 0; x <= xSize; x++ ,i++) {
                vertices[i] = new Vector3(x , y);
            }
        }
    }
    private void OnDrawGizmos() 
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i=0; i<vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i],0.1f);
        }
    }
}
