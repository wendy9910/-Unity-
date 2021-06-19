using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class MeshShape : MonoBehaviour
{
    //分区
    private int segments = 30;
    //半径
    private int Hradius = 5;
    private int Vradius = 12;
    private MeshRenderer m_MeshRender;
    private MeshFilter m_MeshFilter;

    public Material hairColor;

    // Use this for initialization
    void Start()
    {
        hairColor = GetComponent<Renderer>().material;
        hairColor.color = Color.red;

        m_MeshRender = GetComponent<MeshRenderer>();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRender.material = hairColor;
        m_MeshFilter.mesh = CreateMesh();
    }
    Vector3[] vertex;
    private Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        int vertexLen = segments + 1;
        //把这个度数改成其他度数，就可以绘制成扇面了
        int degree = 360;
        float angle = degree * Mathf.Deg2Rad;
        float curAngle = angle / 2;
        float deltaAngle = angle / segments;

        vertex = new Vector3[vertexLen];
        //第一个顶点，保存圆心
        vertex[0] = Vector3.zero;
        //其余顶点保存圆上的点
        for (int i = 1; i < vertexLen; i++)
        {
            float cos = Mathf.Cos(curAngle);
            float sin = Mathf.Sin(curAngle);
            vertex[i] = new Vector3(cos * Hradius, sin * Vradius, 0);
            //为了绘制圆形，当前的角度不断递减
            curAngle -= deltaAngle;
        }
        //填充三角形
        int triangleCount = segments * 3;
        int[] tri = new int[triangleCount];
        for (int i = 0, j = 1; i < triangleCount - 3; i += 3, j++)
        {
            //以圆心为三角形的第一个顶点
            tri[i] = 0;
            tri[i + 1] = j;
            tri[i + 2] = j + 1;
        }
        //最后一个三角形形成闭环
        tri[triangleCount - 3] = 0;
        tri[triangleCount - 2] = vertexLen - 1;
        tri[triangleCount - 1] = 1;

        //根据顶点位置，设置UV纹理坐标
        Vector2[] uv = new Vector2[vertexLen];
        for (int i = 0; i < vertexLen; i++)
        {
            uv[i] = new Vector2(vertex[i].x / Hradius / 2 + 0.5f, vertex[i].z / Vradius / 2 + 0.5f);
        }

        mesh.vertices = vertex;
        mesh.triangles = tri;
        mesh.uv = uv;
        mesh.name = "Sphere";
        return mesh;
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < vertex.Length; i++)
        {
            Gizmos.DrawSphere(vertex[i], 0.1f);
        }
    }
    */

}
