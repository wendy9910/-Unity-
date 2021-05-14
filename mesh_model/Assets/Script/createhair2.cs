using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createhair2 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> MousePointPos2 = new List<Vector3>();
    public List<Vector3> LinePointPos = new List<Vector3>();

    private Vector3[] thickness1;
    private Vector3[] thickness2;

    private Vector3 MousePos, LastPos;

    public int width = 2;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//劃出髮片路徑抓座標
        {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//new position
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//last position
            player.positionCount = LinePointPos.Count;
            player.SetPositions(LinePointPos.ToArray());

            down = 1;
        }
        if (down == 1)
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
            float dist = Vector3.Distance(LastPos, MousePos);//座標間距
            if (dist > 0.5f)
            {
                WidthGenerate(MousePos, LastPos);//點座標計算函式
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                player.positionCount = LinePointPos.Count;
                player.SetPositions(LinePointPos.ToArray()); 
            }

        }
        if (Input.GetMouseButtonUp(0)) down = 0;

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
            if (MousePointPos.Count <= (3 + (width - 1) * 2) - 1) { thickness1[i] = new Vector3(MousePos.x, MousePos.y, 0.0f); }
            else
            {
                Vector3 Vec1 = new Vector3((Vec0.y) * j, (-Vec0.x) * j, 0.0f);
                thickness1[i] = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, 0.0f);
            }
            MousePointPos.Add(thickness1[i]);
        }
        MousePointPos.Add(MousePos);
        LinePointPos.Add(MousePos);

        for (int n = 1; n <= MousePointPos.Count; n++)
        {
            for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
            {
                if (MousePointPos.Count <= ((3 + (width - 1) * 2) - 1) * j) { thickness2[i] = new Vector3(MousePos.x, MousePos.y, 0.0f); }


                Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, 0.0f);
                thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, 0.0f);


                MousePointPos.Add(thickness2[i]);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < MousePointPos.Count; i++)
        {           
            Gizmos.DrawSphere(MousePointPos[i], 0.05f);
        }

    }
}
