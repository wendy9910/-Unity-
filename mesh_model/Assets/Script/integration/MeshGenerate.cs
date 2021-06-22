using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{

    private Mesh mesh;
    public Material GethairColor;


    //裝mesh基本設定的陣列
    Vector3[] vertice;
    Vector2[] uv;//texture
    Vector4[] tangents;
    int[] triangles;

    //紀錄vertice & triangle的長度值變數
    int oldVertice;
    int oldTriangle;
    int Voldlen;

    //記第一下
    int down = 0;

    public void meshGenerate(int count,int Getwidth,List<Vector3> GetPointPos)
    {

        if (down == 0)//讓list有值
        {
            verticeBox.Add(0);
            triangleBox.Add(0);
            down = 1;
        }


        GethairColor = GetComponent<Renderer>().material;
        GethairColor.color = Color.red;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshRenderer>().material = GethairColor;
        mesh.name = "Hair Grid";


        Voldlen = verticeBox[count];//目前的total vertice個數
        vertice = new Vector3[GetPointPos.Count + Voldlen];
        uv = new Vector2[GetPointPos.Count + Voldlen];//texture
        tangents = new Vector4[GetPointPos.Count + Voldlen];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        //舊座標備分
        for (int i = 0; i < Voldlen; i++)
        {
            vertice[i] = oldVerticePos[i];
            uv[i].x = oldVerticePos[i].x;
            uv[i].y = oldVerticePos[i].y;
            tangents[i] = tangent;
        }


        //新的座標
        for (int i = Voldlen,j = 0; i < GetPointPos.Count + Voldlen; i++,j++)
        {
            vertice[i] = GetPointPos[j];
            uv[i].x = GetPointPos[j].x;//Vector3轉Vector2
            uv[i].y = GetPointPos[j].y;
            tangents[i] = tangent;
        }

        mesh.vertices = vertice;//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;

        int point = ((GetPointPos.Count / (3 + (Getwidth - 1) * 2) - 1)) * 2 * Getwidth;//計算網格數
        triangles = new int[point * 6 + triangleBox[count]];//計算需要多少三角形點座標

        //備份三角形
        for (int i = 0; i < triangleBox[count]; i++)
        {
            triangles[i] = oldTrianglePos[i];
        }

        int t = triangleBox[count];//初始三角形
        int k = 0;//累加
        for (int vi = verticeBox[count], x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (Getwidth - 1)), vi + 4 + (2 * (Getwidth - 1)));
            if (x % (Getwidth * 2) != point % (Getwidth * 2)) k = 1;  //在同一行
            else k = 2;  //對vi的累加  (需換行時)
        }

        mesh.triangles = triangles;
        //mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        oldVertice = vertice.Length;
        oldTriangle = triangles.Length;

        //收集長度&舊的位置
        RecordValue(oldVertice, oldTriangle,mesh.vertices,mesh.triangles,count);

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

    //紀錄 vertice & triangle長度的矩陣
    public List<int> verticeBox = new List<int>(); 
    public List<int> triangleBox = new List<int>();
    //輩分座標
    public List<Vector3> oldVerticePos = new List<Vector3>();
    public List<int> oldTrianglePos = new List<int>();
    public List<int> VerticeTotal = new List<int>();
    public List<int> TriangleTotal = new List<int>();

    public void RecordValue(int verticeLength,int triangleLength,Vector3[] verticePos,int[] trianglePos,int count) 
    {

        if (Input.GetMouseButtonUp(0))
        {
            oldVerticePos.Clear();//需先清空原有的座標
            oldTrianglePos.Clear();

            verticeBox.Add(verticeLength);
            triangleBox.Add(triangleLength);

            oldVerticePos.AddRange(verticePos);//重新新增上去
            oldTrianglePos.AddRange(trianglePos);

            Debug.Log(count);

            
            if (count == 0) VerticeTotal.Add(verticeBox[count + 1]);
            else VerticeTotal.Add(verticeBox[count + 1] - verticeBox[count]);
            if (count == 0) TriangleTotal.Add(triangleBox[count + 1]);
            else TriangleTotal.Add(triangleBox[count + 1] - triangleBox[count]);

        }
    
    }

    public void RemoveMesh(Vector3 removePos)
    {
        Debug.Log("Hi");

        Vector3[] MeshVertice = mesh.vertices;
        for (int i=0;i< MeshVertice.Length;i++) 
        {
            Debug.Log("in");
            if (removePos == MeshVertice[i]) Debug.Log("Get");
        }
    }

    public void Selectcolor(int Getcolor)
    {


    }

}
