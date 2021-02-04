using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    //Step1 找出頂點(vertices)位置
    //Step2 找到頂點座標後開始建立mesh

    public int xSize, ySize;
    private Mesh mesh;

    private void Awake()
    {
       Generate();
    }

    private Vector3[] vertices;

    private void Generate() //IEnumerator作用是麼?定義字串?
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);//顯示過程

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();//生成mesh
        mesh.name = "Procedural Grid";//網格(mesh)名稱

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);//法向量切線
        for (int i = 0, y = 0; y <= ySize; y++) {
            for (int x = 0; x <= xSize; x++, i++) {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize,(float)y / ySize);//texture
                tangents[i] = tangent;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++){ 
            for (int x = 0; x < xSize; x++, ti += 6, vi++){
                triangles[ti] = vi;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 1] = triangles[ti + 4] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;//抓點以三角形生成網格
        mesh.RecalculateNormals();//三角形計算法線
    }
    private void OnDrawGizmos () 
    {
       if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i=0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i] ,0.1f);
            Debug.Log("*");
        }
    }
    
}
