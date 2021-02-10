﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    public int xSize, ySize, zSize;

    private Mesh mesh;
    private Vector3[] vertices;
    

    private void Awake()
    {
        Generate();
    }
    private void Generate() 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Cube";
        CreateVertices();
        CreateTriangles();

    }
    private void CreateVertices() {
        int cornerVertices = 8;
        int edgeVertices = (xSize + ySize + zSize - 3) * 4;
        int faceVertices = ((xSize - 1) * (ySize - 1) +
            (xSize - 1) * (zSize - 1) +
            (ySize - 1) * (zSize - 1)) * 2;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];

        int v = 0;

        for (int y = 0; y < ySize; y++)///高
        {

            for (int x = 0; x <= xSize; x++)
            {//X軸座標設點 正
                vertices[v++] = new Vector3(x, y, 0);
            }
            for (int z = 1; z <= zSize; z++)
            {//Z軸座標設點 正
                vertices[v++] = new Vector3(xSize, y, z);
            }
            for (int x = xSize - 1; x >= 0; x--)
            {//X軸座標設點 反 
                vertices[v++] = new Vector3(x, y, zSize);
            }
            for (int z = zSize - 1; z > 0; z--)
            {//X軸座標設點 反
                vertices[v++] = new Vector3(0, y, z);
            }

        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                vertices[v++] = new Vector3(x, ySize - 1, z);
            }
        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                vertices[v++] = new Vector3(x, 0, z);
            }
        }

        mesh.vertices = vertices;


    }
    private void CreateTriangles() {
        int quads = (xSize * ySize * zSize * zSize) * 2;
        int[] triangles = new int[quads * 6];
        int ring = (xSize + zSize) * 2; //整個環
        int t = 0, v = 0;

        for (int y = 0; y < ySize; y++, v++) {
            for (int q = 0; q < ring - 1; q++, v++) {
                t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
            }
            t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
        }
        t = CreateTopFace(triangles, t, ring);
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }

    private int CreateTopFace(int[] triangles, int t, int ring) {
        int v = ring * ySize;
        for (int x = 0;x <xSize - 1; x++,v++) {
            t = SetQuad(triangles, t, v, v+1, v+ring-1, v+ ring);
        }
        int vMin = ring * (ySize + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;
       
        t = SetQuad(triangles , t, vMin, vMid , vMin-1, vMid + xSize - 1);
        for (int x = 1; x < xSize - 1; x++ , vMid++) {
            t = SetQuad(triangles, t, vMid, vMid + 1 , vMid + xSize - 1, vMid + xSize) ;
        }
        t = SetQuad(triangles, t, vMid, vMax, vMid + xSize - 1, vMax + 1);

        return t;
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11) {//創建三角形網格
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;

        return i + 6;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
        
    }
}
