using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{
    private Mesh mesh;
    public Material GethairColor;

    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangle;

    int OldVerticeCount = 0;
    int OldTriangleCount = 0;

    public void GenerateMesh(List<Vector3> GetPointPos,int Getwidth) 
    {
        GethairColor = GetComponent<Renderer>().material;
        GethairColor.color = Color.yellow;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshRenderer>().material = GethairColor;
        mesh.name = "HairModel";

        vertice = new Vector3[GetPointPos.Count + OldVerticeCount];
        uv = new Vector2[GetPointPos.Count + OldVerticeCount];
        tangents = new Vector4[GetPointPos.Count + OldVerticeCount];

        for (int i = OldVerticeCount,j=0;i<GetPointPos.Count;i++,j++) 
        {
            vertice[i] = GetPointPos[j];
            uv[i].x = GetPointPos[j].x;//Vector3轉Vector2
            uv[i].y = GetPointPos[j].y;
            tangents[i] = new Vector4(1f, 0f, 0f, -1f);
        }
        mesh.vertices = vertice;//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;

        int point;
        if (GetPointPos.Count < 1) point = 0;//計算網格數
        else point = ((GetPointPos.Count / (3 + (Getwidth - 1) * 2) - 1)) * 2 * Getwidth;

        triangle = new int[point * 6 + OldTriangleCount];//計算需要多少三角形點座標

        int t = OldTriangleCount;
        int k = 0;

        for (int vi = OldVerticeCount, x = 1; x <= point; x++, vi += k)
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
