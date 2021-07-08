using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class modelthickness : MonoBehaviour
{
    public List<Vector3> PointPos = new List<Vector3>();
    Vector3 newPos, oldPos;

    float length = 0.5f;
    int width = 1;
    float thickness = 0.5f;
    int down = 0;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        //PositionGenerate();

    }

    // Update is called once per frame
    void Update()
    {

        if (down == 0) 
        {
            if (Input.GetMouseButtonDown(0)) 
            { 
                newPos = oldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                down = 1;
            }
        
        }
        if (down == 1) 
        { 
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            float dist = Vector3.Distance(oldPos,newPos);
            if (dist > length) 
            {
                PositionGet(oldPos,newPos);
                newPos = oldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }
            if (PointPos.Count >= ((3 + (width - 1) * 2) * 2)*2) 
            {
                GetMesh();
            }

            if (Input.GetMouseButtonUp(0)) 
            {
               
                down = 0;
            }

        }
    }

    public void PositionGet(Vector3 oldPos,Vector3 newPos)
    {
        Vector3 Vec = newPos - oldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x,oldPos.y + Vec1.y,oldPos.z + Vec1.z);
            PointPos.Add(temp);
        }
        PointPos.Add(oldPos);
        for (int i = 0 ,j = 1;i < width; i++,j++) 
        {
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            PointPos.Add(temp);
        }
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z + thickness);
            PointPos.Add(temp);
        }
        PointPos.Add(new Vector3(oldPos.x, oldPos.y, oldPos.z + thickness)); ;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z + thickness);
            PointPos.Add(temp);
        }

    }

    Vector3[] vertice;
    Vector2[] uv;//texture
    Vector4[] tangents;
    int[] triangles;
    
    public void GetMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Hair Grid";

        vertice = new Vector3[PointPos.Count];
        uv = new Vector2[PointPos.Count];
        tangents = new Vector4[PointPos.Count];

        for (int i = 0; i < PointPos.Count; i++) 
        {
            vertice[i] = PointPos[i];
            uv[i].x = PointPos[i].x;
            uv[i].y = PointPos[i].y;
            tangents[i] = new Vector4(1f, 0f, 0f, -1f);
        }
        mesh.vertices = vertice;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int Pointlen = PointPos.Count / (((3 + (width - 1) * 2) - 1) * 2 + 2);
        int totalPoint = (((3 + (width - 1) * 2) - 1) * 2 + 2) * (Pointlen - 1) + ((3 + (width - 1) * 2) - 1) * 2;

        Debug.Log(totalPoint);
        //if (PointPos.Count < 1) point = 0;//計算網格數
        //else point = ((PointPos.Count / (3 + (width - 1) * 2) - 1)) * 2 * width;
        //int totalPoint = 2;
        triangles = new int[totalPoint * 6];

        int t = 0;//初始三角形


        int Block1 = ((3+(width-1)*2)-1)*(Pointlen-1); // 前or後兩面區塊

        for (int vi = 0, x = 1, k = 0; x <= Block1; x++, vi += k) 
        {
            t = SetQuad(triangles,t,vi,vi+1, vi + 6 + (2 * (width - 1)), vi + 7 + (2 * (width - 1)));
            if (x % (width * 2) != totalPoint % (width * 2)) k = 1;
            else k = 5 + (width-1)*2;
        }
        for (int vi = vertice.Length-1, x = 1, k = 0; x <= Block1; x++, vi -=k ) 
        {
            t = SetQuad(triangles, t, vi, vi - 1, vi - ( 6 + (2 * (width - 1))), vi - ( 7 + (2 * (width - 1))));
            if (x % (width * 2) != totalPoint % (width * 2)) k = 1;
            else k = 5 + (width - 1) * 2;
        }
        for (int vi = (3+(width-1)*2)*2-1,vii = 0, x =1; x<= (3 + (width - 1) * 2) - 1; x++, vi--, vii++) 
        {
            t = SetQuad(triangles, t, vi, vi - 5, vii , vii + 1);
        }
        for (int vi = 3 + (width - 1) * 2, x = 1; x <= Pointlen - 1; x++, vi+= 6 + (2 * (width - 1))) 
        {
            t = SetQuad(triangles, t, vi, vi + 6 + +(width - 1) * 4, vi -1, vi + 5 + (width - 1) * 4);
        }
        for (int vi = (vertice.Length-1)-(3+(width-1)*2)*2-1, vii = vertice.Length-1, x = 1; x <= (3 + (width - 1) * 2) - 1; x++ ,vi++, vii--) 
        {
            t = SetQuad(triangles, t, vi,vi - 5, vii, vii + 1);
        }
        for (int vi = 0, x = 1; x <= Pointlen - 1; x++, vi += 6 + (2 * (width - 1))) 
        {
            t = SetQuad(triangles, t, vi, vi + 6 + +(width - 1) * 4, vi - 1, vi + 5 + (width - 1) * 4);
        }

        mesh.triangles = triangles;

       
        /*for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));

            if (x % (width * 2) != point % (width * 2)) k = 1;
            else k = 2;

        }*/

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
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
    }
}
