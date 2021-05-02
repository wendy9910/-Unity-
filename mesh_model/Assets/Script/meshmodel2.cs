using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class meshmodel2 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    private Vector3 MousePos, LastPos;
    private Mesh mesh;
    private Vector3[] vertices;

    int down = 0;//滑鼠判定
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            down = 1;
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //MousePointPos.Add(MousePos);
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        }
        if (down == 1)
        {


            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 1.0f)
            {
                Generate(MousePos, LastPos);
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }


        }
        if (Input.GetMouseButtonUp(0)) down = 2;

        if (down == 2)
        {
            MeshGenerate();

        }

    }
   

    void MeshGenerate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Hair Grid";

        Vector3[] moscov = MousePointPos.ToArray();
        Vector2[] uv = new Vector2[MousePointPos.Count];
        Vector4[] tangents = new Vector4[MousePointPos.Count];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0; i < MousePointPos.Count; i++)
        {
            uv[i].x = MousePointPos[i].x;
            uv[i].y = MousePointPos[i].y;
            tangents[i] = tangent;
        }

        mesh.vertices = MousePointPos.ToArray();
        mesh.uv = uv;
        mesh.tangents = tangents;
        int point=0;
        point = ((MousePointPos.Count / 3) - 1) * 2;
    ;

        int[] triangles = new int[point * 6];
        
        int t = 0;
        int k = 0;
        for (int vi = 0, x = 0; x < point; x++,vi+=k)
        {
            
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3, vi + 4);
    
            if (x % 2 != 0) k = 2;
            else k = 1;

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

    void Generate(Vector3 pos1, Vector3 pos2)
    {
        Vector3 Vec0 = pos1 - pos2;
        Vector3 Vec1 = new Vector3(Vec0.y, -Vec0.x, 0.0f);
        Vector3 Vec2 = new Vector3(-Vec0.y, Vec0.x, 0.0f);
        Vector3 AddPos1 = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, 0.0f);
        Vector3 AddPos2 = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, 0.0f);
        MousePointPos.Add(AddPos1);
        MousePointPos.Add(MousePos);
        MousePointPos.Add(AddPos2);
         
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < MousePointPos.Count; i++)
        {
            Gizmos.DrawSphere(MousePointPos[i], 0.1f);
        }

    }
}
