using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{
    private Mesh mesh;
    public Material GetHairColor;
    MeshCollider HairCollider;

    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangle;

    public void GenerateMesh(List<Vector3> GetPointPos,int Getwidth) 
    {
       
        GetHairColor = GetComponent<Renderer>().material;
        GetHairColor.color = Color.red;
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
            uv[i].x = GetPointPos[i].x;
            uv[i].y = GetPointPos[i].y;
            tangents[i] = new Vector4(1f, 0f, 0f, -1f);
        }
        mesh.vertices = vertice;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int point = ((GetPointPos.Count / (3 + (Getwidth - 1) * 2) - 1)) * 2 * Getwidth;

        triangle = new int[point * 6];

        int t=0;//triangle index起始點
        for (int vi = 0, k = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangle, t, vi, vi + 1, vi + 3 + (2 * (Getwidth - 1)), vi + 4 + (2 * (Getwidth - 1)));
            if (x % (Getwidth * 2) != point % (Getwidth * 2)) k = 1;  //在同一行
            else k = 2;  //對vi的累加  (需換行時)
        }
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
