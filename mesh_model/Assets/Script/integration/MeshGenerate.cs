using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{

    private Mesh mesh;
    static List<Vector3> GetPointPos = drawer.PointPos;
    static int Getwidth = drawer.width;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void meshGenerate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Hair Grid";

        Vector2[] uv = new Vector2[GetPointPos.Count];//texture
        Vector4[] tangents = new Vector4[GetPointPos.Count];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0; i < GetPointPos.Count; i++)//Vector3轉Vector2
        {
            uv[i].x = GetPointPos[i].x;
            uv[i].y = GetPointPos[i].y;
            tangents[i] = tangent;
        }

        mesh.vertices = GetPointPos.ToArray();//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;
        int point = 0;

        point = ((GetPointPos.Count / (3 + (Getwidth - 1) * 2) - 1)) * 2 * Getwidth;//計算網格數


        int[] triangles = new int[point * 6];//計算需要多少三角形點座標

        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (Getwidth - 1)), vi + 4 + (2 * (Getwidth - 1)));
            if (x % (Getwidth * 2) != point % (Getwidth * 2)) k = 1;
            else k = 2;
        }
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = v10;
        triangles[i + 2] = v01;
        triangles[i + 3] = v01;
        triangles[i + 4] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }
}
