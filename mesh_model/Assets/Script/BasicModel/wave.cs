using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class wave : MonoBehaviour
{
    public List<Vector3> PointPos = new List<Vector3>();
    public List<Vector3> UpdatePointPos = new List<Vector3>();
    Vector3 oldPos, newPos;
    float length = 1f;
    Vector3 NormaizelVec;
    int down = 0;
    public GameObject ball;
    Vector3 cross1,cross2;
    public Texture HairTexture, HairNormal;

    Mesh mesh;
    public Material GethairColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
        if (Input.GetMouseButtonDown(0))
        {
            //oldPos = newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            oldPos = newPos = ball.transform.position;
            PointPos.Add(oldPos);
            down = 1;
        }
        if (down == 1)
        {
            //newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            newPos = ball.transform.position;
            float dist = Vector3.Distance(oldPos,newPos);
            Debug.Log(dist);
            
            if (dist > length)
            {
                n = 0;
                NormaizelVec = newPos - oldPos;
                NormaizelVec = Vector3.Normalize(NormaizelVec);
                Vector3 NewVec = new Vector3(NormaizelVec.x * length, NormaizelVec.y * length, NormaizelVec.z * length);
                newPos = NewVec + oldPos;
                PointPos.Add(newPos);
                VectorCross(ball.transform.up,ball.transform.forward,ball.transform.right);
                Wave(NormaizelVec);
                oldPos = newPos;
            }
            if (PointPos.Count > 1) 
            {
                MeshGenerate(UpdatePointPos);
            }
            
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            PointPos.Clear();
            down = 0;
        }
    }

    int n = 0;
    List<Vector3> TempPos = new List<Vector3>();

    public void Wave(Vector3 NVec) 
    {
        TempPos.Clear();

        float w = 3f;
        float waveSize = 0.5f;
        float angle = Mathf.PI;
        for (int i = 0; i < PointPos.Count; i++)
        {
            float y = Mathf.Sin(angle);

            /*
            Vector3 temp2 = new Vector3( PointPos[i].x + waveSize * y, PointPos[i].y + waveSize * y , PointPos[i].z);
            TempPos.Add(temp2);
            Vector3 temp = new Vector3(PointPos[i].x + waveSize * y + 1, PointPos[i].y + waveSize * y + 1, PointPos[i].z);
            TempPos.Add(temp);
            /*TempPos.Add(temp - cross2 * w);
            TempPos.Add(temp + cross1 * 0.1f);
            TempPos.Add(temp + cross2 * w);
            TempPos.Add(temp - cross1 * 0.1f);*/
            
            Vector3 temp = new Vector3(PointPos[i].x , PointPos[i].y , PointPos[i].z + waveSize * y);
            TempPos.Add(temp);
            Vector3 temp2 = new Vector3(PointPos[i].x + 1, PointPos[i].y, PointPos[i].z + waveSize * y);
            TempPos.Add(temp2);
            angle -= 0.9f;
        }
        UpdatePointPos.Clear();
        UpdatePointPos.AddRange(TempPos);
    }

    public void VectorCross(Vector3 up,Vector3 forward, Vector3 right) 
    {
        cross1 = Vector3.Cross(up, forward);
        cross2 = Vector3.Cross(up, right);

    }

    public void MeshGenerate(List<Vector3> Pos) 
    {
        GethairColor = GetComponent<Renderer>().material;
        GethairColor.color = new Color(222f / 255, 184f / 255, 135f / 255);

        GethairColor.SetTexture("_MainTex", HairTexture);
        GethairColor.SetTexture("_BumpMap", HairNormal);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshRenderer>().material = GethairColor;
        mesh.name = "HairModel";
        
        Vector3[] vertice = new Vector3[Pos.Count];
        Vector4[] tangent = new Vector4[Pos.Count];
        Vector2[] uv = new Vector2[Pos.Count];

        for (int i = 0; i < Pos.Count; i++)
        {
            vertice[i] = Pos[i];
            tangent[i] = new Vector4(1f, 0f, 0f, -1f);
        }

        int len = Pos.Count / 2;
        float TexWidth = 0.8f;
        for (int i = 0, x = 0; i < len; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                uv[x] = new Vector2(TexWidth / 2 * j, 1.0f / len * i);
                x++;
            }
        }

        mesh.vertices = vertice;//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangent;

        int point = Pos.Count/2 - 1;
        int[]triangle = new int[point * 6];
        /*
        int t = 0;
        for (int i = 1, vi = 0; i <= point - 2; i++, vi++)
        {
            if (i % 4 != 0) t = SetQuad(triangle, t, vi, vi + 1, vi + 4, vi + 5);
            else t = SetQuad(triangle, t, vi, vi - 3, vi + 4, vi + 1);
        }
        int vii = 0;
        t = SetQuad(triangle, t, vii + 2, vii + 1, vii + 3, vii);
        vii = Pos.Count - 1;
        t = SetQuad(triangle, t, vii - 1, vii, vii - 2, vii - 3);
        */
        int t = 0;
        for (int i = 1, vi = 0; i <= point; i++, vi+=2)
        {
            t = SetQuad(triangle, t, vi, vi + 1, vi + 2, vi + 3);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < UpdatePointPos.Count; i++)
        {
            Gizmos.DrawSphere(UpdatePointPos[i], 0.2f);
        }
    }
}
