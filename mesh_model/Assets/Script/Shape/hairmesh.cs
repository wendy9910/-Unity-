using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class hairmesh : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    private Vector3[] thickness1;
    private Vector3[] thickness2;

    private Vector3 MousePos, LastPos;
    public int width = 1;

    int down = 0;//滑鼠判定


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("按Space 設定寬度");
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
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

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//new position
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//last position

            down = 1;
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
            if (MousePointPos.Count >= (width * 2 + 1) * 2 && MousePointPos != null)
            {

                MeshGenerate();

            }
        }
        
        if (Input.GetMouseButtonUp(0) && down == 1)
        {
            
            HairN++;
            Num++;

            nextVertice = new int[HairN];
            nextTriangle = new int[HairN];

            nextVertice[Num] = oldPos[Num-1];
            nextTriangle[Num] = pointPos[Num-1];
            oldVerticePos = vertice;
            oldTrianglePos = triangles;
            GetBox(nextVertice,nextTriangle);

            MousePointPos.Clear();
            down = 0;
            
        }
        
        //if (MousePointPos.Count >= (width * 2 + 1) * 2 && MousePointPos != null) MeshGenerate();

    }
    public Vector3[] oldVerticePos; //備分之前的髮片V
    public int[] oldTrianglePos; //備分之前的髮片T
    public int[] nextVertice;//total vertice長度
    public int[] nextTriangle;//total triangle長度
    Vector3[] vertice;
    Vector2[] uv;
    int[] triangles;

    int[] oldPos;
    int[] pointPos;
    int point=0;
    int HairN = 1;//Size
    int Num = 0;//指標

    public Mesh mesh;

    void MeshGenerate()
    {
        
        mesh.name = "Hair Grid";

        nextVertice = new int[HairN];
        nextTriangle = new int[HairN];

        oldPos = new int[HairN];
        pointPos = new int[HairN];

        verticeBox = new int[HairN];
        triangleBox = new int[HairN];

        nextVertice[0] = 0;
        nextTriangle[0] = 0;

        oldVerticePos = new Vector3[nextVertice[Num]];
        oldTrianglePos = new int[nextTriangle[Num]];

        int len = MousePointPos.Count;
        vertice = new Vector3[len + nextVertice[Num]]; 
        uv = new Vector2[len + nextVertice[Num]];

        oldPos[Num] = vertice.Length;
        
        //備份Vertices

        for (int i = 0; i < verticeBox[Num]; i++)//0 ~ 之前的total長度
        {
            vertice[i] = oldVerticePos[i];
            uv[i].x = oldVerticePos[i].x;
            uv[i].y = oldVerticePos[i].y;

        }

        //新的髮片Vertice
        for (int j = verticeBox[Num]; j < len;j++) //從之前開始 ~ 新的長度
        {
            vertice[j] = MousePointPos[j];
            uv[j].x = MousePointPos[j].x;
            uv[j].y = MousePointPos[j].y;
        
        }
        mesh.vertices = vertice;
        //mesh.uv = uv;
        
        point = (len/ (3 + (width-1) * 2) - 1) * width * 2;
        triangles = new int[point*6 + triangleBox[Num]];


        for (int vi = 0, x = 1; x <= triangleBox[Num]; x++, vi++)//迴圈走網格面數
        {
            triangles[vi] = oldTrianglePos[vi];
        }

        //新髮片三角形
        int t = 0;//初始三角形
        int k = 0;
        for (int vi = 0 + triangleBox[Num], x = 1; x <= point; x++, vi += k)//迴圈走網格面數
        {
            t = SetQuad(triangles, t + triangleBox[Num], vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));
            if (x % (width * 2) != point % (width * 2)) k = 1;//判斷換沒換行
            else k = 2;
        }

        pointPos[Num] = triangles.Length;

        mesh.triangles = triangles;
        

    }
    public int[] verticeBox;
    public int[] triangleBox;
    public Vector3[] VBox;
    public int[] TBox;
    void GetBox(int[] Ve,int[] Tr)
    {
        verticeBox = new int[HairN];
        triangleBox = new int[HairN];
        VBox = new Vector3[HairN];
        TBox = new int[HairN];

        verticeBox[Num] = Ve[Num];
        triangleBox[Num] = Tr[Num];
        //VBox[Num] = VPos[Num];
        //TBox[Num] = TPos[Num];


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


    void WidthGenerate(Vector3 pos1, Vector3 pos2)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //右左兩個延伸點座標矩陣
        thickness1 = new Vector3[width];
        thickness2 = new Vector3[width];

        //算兩點向量差
        Vector3 Vec0 = pos1 - pos2;

        for (int i = 0, j = thickness1.Length; i < thickness1.Length; i++, j--)//widthAdd1
        {
            Vector3 Vec1 = new Vector3((Vec0.y) * j, (-Vec0.x) * j, 0.0f);
            thickness1[i] = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, 0.0f);
            MousePointPos.Add(thickness1[i]);
        }
        MousePointPos.Add(MousePos);
        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, 0.0f);
            thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, 0.0f);
            MousePointPos.Add(thickness2[i]);
        }

    }
    /*private void OnDrawGizmos(Vector3[] Pos)
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < Pos.Length; i++)
        {
            Gizmos.DrawSphere(Pos[i], 0.1f);
        }
    }*/
}
