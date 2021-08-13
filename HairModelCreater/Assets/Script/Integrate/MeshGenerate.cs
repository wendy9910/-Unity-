using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{
    private Mesh mesh;
    public static Material GetHairColor;
    MeshCollider HairCollider;

    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangle;

    public void GenerateMesh(List<Vector3> GetPointPos,int Getwidth) 
    {
       
        GetHairColor = GetComponent<Renderer>().material;
        GetHairColor.color = Color.blue;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();//指定Mesh到MeshFilter
        GetComponent<MeshRenderer>().material = GetHairColor;
        mesh.name = "HairMesh";
        if(gameObject.GetComponent<MeshCollider>() == null)HairCollider = gameObject.AddComponent<MeshCollider>();
        else HairCollider = gameObject.GetComponent<MeshCollider>();
        HairCollider.sharedMesh = mesh;

        vertice = new Vector3[GetPointPos.Count];
        uv = new Vector2[GetPointPos.Count];
        tangents = new Vector4[GetPointPos.Count];

        for (int i=0;i<GetPointPos.Count;i++) 
        {
            vertice[i] = GetPointPos[i];
            tangents[i] = new Vector4(1f, 0f, 0f, -1f);
        }
        int len = GetPointPos.Count / (3+(Getwidth - 1)*2);
        Debug.Log(len);
        for (int i = 0, x = 0; i < len; i++)
        {
            for (int j = 1; j <= (3 + (Getwidth - 1)*2); j++)
            {
                uv[x] = new Vector2(1.0f / (3 + (Getwidth - 1)*2) * j, 1.0f / len * i);//Vector3轉Vector2
                x++;
            }
        }

        mesh.vertices = vertice;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int point = GetPointPos.Count-4;
        triangle = new int[point*6];

        int t = 0;
        for (int i = 1,vi = 0; i <= point;i++,vi++)
        {
            if (i % 4 != 0)
            {
                t = SetQuad(triangle, t, vi, vi + 1, vi + 4, vi + 5);
            }
            else 
            {
                t = SetQuad(triangle, t, vi, vi - 3, vi + 4, vi + 1);
            }
        }
        /*int vii = 0;
        t = SetQuad(triangle, t, vii + 1, vii + 2, vii, vii + 3);
        vii = GetPointPos.Count - 1;
        t = SetQuad(triangle, t, vii -2, vii - 1, vii - 3, vii);*/

        mesh.triangles = triangle;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    private static int SetQuad(int[] triangles, int i, int v0, int v1, int v2, int v3)
    {
        triangles[i] = v0;
        triangles[i + 1] = v1;
        triangles[i + 2] = v2;
        triangles[i + 3] = v2;
        triangles[i + 4] = v1;
        triangles[i + 5] = v3;
        return i + 6;
    }
}

/*
  int Pointlen = GetPointPos.Count / (((3 + (Getwidth - 1) * 2) - 1) * 2 + 2);
        int totalPoint = (((3 + (Getwidth - 1) * 2) - 1) * 2 + 2) * (Pointlen - 1) + ((3 + (Getwidth - 1) * 2) - 1) * 2;

        triangle = new int[totalPoint * 6];

        int t = 0;//初始三角形
        int Block1 = ((3 + (Getwidth - 1) * 2) - 1) * (Pointlen - 1); // 前or後兩面區塊

        //A1 K
        for (int vi = 0, x = 1, k = 0; x <= Block1; x++, vi += k)
        {
            t = SetQuad(triangle, t, vi, vi + 1, vi + 6 + (Getwidth - 1) * 4, vi + 7 + (Getwidth - 1) * 4);
            if (x % ((3 + (Getwidth - 1) * 2) - 1) != 0) k = 1;
            else k = 5 + (Getwidth - 1) * 2;
        }
        //A2 K
        for (int vi = vertice.Length - 1, x = 1, k = 0; x <= Block1; x++, vi -= k)
        {
            t = SetQuad(triangle, t, vi, vi - 1, vi - (6 + (Getwidth - 1) * 4), vi - (7 + (Getwidth - 1) * 4));
            if (x % ((3 + (Getwidth - 1) * 2) - 1) != 0) k = 1;
            else k = 5 + (Getwidth - 1) * 2;
        }
        //B1 K
        for (int vi = 5 + (Getwidth - 1) * 4, vii = 0, x = 1; x <= (3 + (Getwidth - 1) * 2) - 1; x++, vi--, vii++)
        {
            t = SetQuad(triangle, t, vi, vi - 1, vii, vii + 1);
        }

        //Top K
        for (int vi = 3 + (Getwidth - 1) * 2, x = 1; x <= Pointlen - 1; x++, vi += (6 + (Getwidth - 1) * 4))
        {
            t = SetQuad(triangle, t, vi, vi + 6 + (Getwidth - 1) * 4, vi - 1, vi + 5 + (Getwidth - 1) * 4);
        }

        //B2
        for (int vi = (vertice.Length - 1) - (3 + (Getwidth - 1) * 2) * 2 + 1, vii = vertice.Length - 1, x = 1; x <= (3 + (Getwidth - 1) * 2) - 1; x++, vi++, vii--)
        {
            t = SetQuad(triangle, t, vi, vi + 1, vii, vii - 1);
        }
        //Bottom k
        for (int vi = 0, vii = 5 + (Getwidth - 1) * 4, x = 1; x <= Pointlen - 1; x++, vi += (6 + (Getwidth - 1) * 4), vii += (6 + (Getwidth - 1) * 4))
        {
            t = SetQuad(triangle, t, vi, vi + 6 + (Getwidth - 1) * 4, vii, vii + 6 + (Getwidth - 1) * 4);
        }
 
 */