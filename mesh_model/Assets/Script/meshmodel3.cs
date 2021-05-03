using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class meshmodel3 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    private Vector3[] thickness1;
    private Vector3[] thickness2;
 
    private Vector3 MousePos, LastPos;
    private Mesh mesh;
    public int width = 1;


    int down = 0;//滑鼠判定
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("按Space 設定寬度");
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown("space"))//設定mesh寬度
        {
            width++;
            Debug.Log("Range" + width);
        }

        if (Input.GetMouseButtonDown(0))//劃出髮片路徑抓座標
        {
            down = 1;
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//new position
            
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//last position

        }
        if (down == 1)
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));

            float dist = Vector3.Distance(LastPos, MousePos);//座標間距
            if (dist > 1.0f)
            {
                WidthGenerate(MousePos, LastPos);//點座標計算函式
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
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
        Vector2[] uv = new Vector2[MousePointPos.Count];//texture
        Vector4[] tangents = new Vector4[MousePointPos.Count];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0; i < MousePointPos.Count; i++)//Vector3轉Vector2
        {
            uv[i].x = MousePointPos[i].x;
            uv[i].y = MousePointPos[i].y;
            tangents[i] = tangent;
        }

        mesh.vertices = MousePointPos.ToArray();//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;
        int point = 0;
         
        point = ((MousePointPos.Count / (3 + (width-1)*2 ) - 1)) * 2 * width;//計算網格數
        

        int[] triangles = new int[point * 6];//計算需要多少三角形點座標

        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));
            if (x % (width * 2) != point % (width * 2)) k = 1;
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

    void WidthGenerate(Vector3 pos1, Vector3 pos2)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //右左兩個延伸點座標矩陣
        thickness1 = new Vector3[width];
        thickness2 = new Vector3[width];
       
        //算兩點向量差
        Vector3 Vec0 = pos1 - pos2;
        Vector3 pos = pos1;

        WidthAdd1(Vec0,pos);
        MousePointPos.Add(MousePos);
        WidthAdd2(Vec0,pos);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < MousePointPos.Count; i++)
        {
            Gizmos.DrawSphere(MousePointPos[i], 0.1f);
        }
    }

    void WidthAdd1(Vector3 Vec, Vector3 pos)//方便計算寬度改變
    {
        for (int i = 0;i < thickness1.Length;i++) {
            Vector3 Vec1 = new Vector3((Vec.y)*(i+1), (-Vec.x)*(i+1), 0.0f);
            thickness1[i] = new Vector3(pos.x + Vec1.x, pos.y + Vec1.y, 0.0f);
            MousePointPos.Add(thickness1[i]);
        }
    }


    void WidthAdd2(Vector3 Vec, Vector3 pos)
    {
   
        for (int i = 0; i < thickness2.Length; i++) {
            Vector3 Vec2 = new Vector3((-Vec.y)*(i+1), (Vec.x)*(i+1), 0.0f);
            thickness2[i] = new  Vector3(pos.x + Vec2.x, pos.y + Vec2.y, 0.0f);
            MousePointPos.Add(thickness2[i]);
        }

    }

}
