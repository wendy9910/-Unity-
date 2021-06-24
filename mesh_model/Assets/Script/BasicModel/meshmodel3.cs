using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class meshmodel3 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> LinePointPos = new List<Vector3>();
    private Vector3[] thickness1;
    private Vector3[] thickness2;
 
    private Vector3 MousePos, LastPos;
    private Mesh mesh;
    public int width = 1;

    private LineRenderer player;
    
    int down = 0;//滑鼠判定

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.red, Color.red);
        player.SetWidth(0.1f, 0.1f);
        player.numCapVertices = 2;//端點圓度
        player.numCornerVertices = 2;//拐彎圓滑度

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
            
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position
            
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//last position
            player.positionCount = LinePointPos.Count;
            player.SetPositions(LinePointPos.ToArray());

            down = 1;
        }
        if (down == 1)
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));

            float dist = Vector3.Distance(LastPos, MousePos);//座標間距
            if (dist > 1.0f)
            {
                WidthGenerate(MousePos, LastPos);//點座標計算函式
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));

                player.positionCount = LinePointPos.Count;
                player.SetPositions(LinePointPos.ToArray());

                
            }
            if (MousePointPos.Count >= (width * 2 + 1) * 2 && MousePointPos != null)
            {
                MeshGenerate();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            ///MousePointPos.Clear();
            LinePointPos.Clear();
            down = 0;
        }

}


    void MeshGenerate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Hair Grid";

        Vector3[] vertice = new Vector3[MousePointPos.Count *2];
        Vector2[] uv = new Vector2[MousePointPos.Count * 2];//texture
        Vector4[] tangents = new Vector4[MousePointPos.Count * 2];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0,j=0; i < MousePointPos.Count*2; i++,j++)//Vector3轉Vector2
        {
            if (i == MousePointPos.Count) j = 0;
            vertice[j] = MousePointPos[j];
            uv[i].x = MousePointPos[j].x;
            uv[i].y = MousePointPos[j].y;
            tangents[i] = tangent;
        }

        mesh.vertices = vertice;//mesh網格點生成
        mesh.uv = uv;
        mesh.tangents = tangents;
        int point = 0;
         
        point = ((MousePointPos.Count / (3 + (width-1)*2 ) - 1)) * 2 * width;//計算網格數
        

        int[] triangles = new int[point * 6 * 2];//計算需要多少三角形點座標

        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));
            t = SetQuad2(triangles, t, vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));
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
    private static int SetQuad2(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {

        triangles[i] = v00;
        triangles[i + 1] = v01;
        triangles[i + 2] = v10;
        triangles[i + 3] = v10;
        triangles[i + 4] = v01;
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

         for (int i = 0, j = thickness1.Length; i < thickness1.Length; i++, j--)//widthAdd1
        {
            Vector3 Vec1 = new Vector3((Vec0.y) * j, (-Vec0.x) * j, (Vec0.y)*j);
            thickness1[i] = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, pos1.z + Vec1.z);
            MousePointPos.Add(thickness1[i]);
        }
        MousePointPos.Add(MousePos);
        LinePointPos.Add(MousePos);

        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, (-Vec0.y) * j);
            thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, pos1.z + Vec2.z);
            MousePointPos.Add(thickness2[i]);
        }

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
