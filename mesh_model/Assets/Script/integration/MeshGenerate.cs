using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{

    private Mesh mesh;
    public Material GethairColor;
    MeshCollider meshCollider;
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


    public void meshGenerate(int count,int Getwidth,List<Vector3> GetPointPos,GameObject Hairmodel)
    {

        if (down == 0)//讓list有值
        {
            verticeBox.Add(0);
            triangleBox.Add(0);
            down = 1;
        }
        //meshCollider = Hairmodel.AddComponent<MeshCollider>();

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

        int point;

        if (GetPointPos.Count < 1) point = 0;//計算網格數
        else point = ((GetPointPos.Count / (3 + (Getwidth - 1) * 2) - 1)) * 2 * Getwidth;

        triangles = new int[point * 6 + triangleBox[count]];//計算需要多少三角形點座標

        //備份三角形
        for (int i = 0; i < triangleBox[count]; i++) triangles[i] = oldTrianglePos[i];

        int t = triangleBox[count];//初始三角形
        int k = 0;//累加
        for (int vi = verticeBox[count], x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (Getwidth - 1)), vi + 4 + (2 * (Getwidth - 1)));
            if (x % (Getwidth * 2) != point % (Getwidth * 2)) k = 1;  //在同一行
            else k = 2;  //對vi的累加  (需換行時)
        }

        mesh.triangles = triangles;
        mesh.RecalculateBounds();
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
    //undo暫存值
    public List<Vector3> undoVerticePos = new List<Vector3>();
    public List<int> undoTrianglePos = new List<int>();
    //個別的髮片的 vertice與triangle個數
    public List<int> VerticeTotal = new List<int>();
    public List<int> TriangleTotal = new List<int>();
    //undo排序
    public List<int> undoSortVertice = new List<int>();
    public List<int> undoSortTriangle = new List<int>();

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

            if (count == 0) VerticeTotal.Add(verticeBox[count + 1]);
            else VerticeTotal.Add(verticeBox[count + 1] - verticeBox[count]);
            if (count == 0) TriangleTotal.Add(triangleBox[count + 1]);
            else TriangleTotal.Add(triangleBox[count + 1] - triangleBox[count]);

        }
    
    }

    public void RemoveMesh(Vector3 removePos)
    {

 
    }
 

    public void ClearMesh(int count) 
    {

        undoSortVertice.Add(verticeBox[count]);
        undoSortTriangle.Add(triangleBox[count]);

        undoVerticePos.AddRange(oldVerticePos);
        undoTrianglePos.AddRange(oldTrianglePos);

        oldVerticePos.Clear();
        oldTrianglePos.Clear();

        down = 0;
    }


    public void undoMesh(int count) 
    {
        undoSortVertice.Add(VerticeTotal[count-1]);
        undoSortTriangle.Add(TriangleTotal[count-1]);

        int Vindex = verticeBox[count-1];
        int Tindex = triangleBox[count-1];

        undoVerticePos.AddRange(oldVerticePos.GetRange(Vindex,VerticeTotal[count-1]));
        undoTrianglePos.AddRange(oldTrianglePos.GetRange(Tindex,TriangleTotal[count-1]));
        oldVerticePos.RemoveRange(Vindex,VerticeTotal[count-1]);
        oldTrianglePos.RemoveRange(Tindex,TriangleTotal[count-1]);

    }
    public void redoMesh()
    {
        //推回去
        int LastUndoVIndex = undoSortVertice.Count - 1;
        int LastUndoTIndex = undoSortTriangle.Count - 1;

        Debug.Log("Last" + LastUndoVIndex);
        Debug.Log("Last count" + undoSortTriangle[LastUndoTIndex]);

        int Vindex = undoVerticePos.Count - undoSortVertice[LastUndoVIndex];
        int Tindex = undoTrianglePos.Count - undoSortTriangle[LastUndoTIndex];


        oldVerticePos.AddRange(undoVerticePos.GetRange(Vindex,undoSortVertice[LastUndoVIndex]));
        oldTrianglePos.AddRange(undoTrianglePos.GetRange(Tindex, undoSortTriangle[LastUndoTIndex]));

        //移除
        undoVerticePos.RemoveRange(Vindex, undoSortVertice[LastUndoVIndex]);
        undoTrianglePos.RemoveRange(Tindex, undoSortTriangle[LastUndoTIndex]);

        undoSortVertice.RemoveAt(LastUndoVIndex);
        undoSortTriangle.RemoveAt(LastUndoTIndex);

        
    }


    public void Selectcolor(int Getcolor)
    {
        

    }

}
