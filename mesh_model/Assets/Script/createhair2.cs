using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createhair2 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> MousePointPos2 = new List<Vector3>();
    public List<Vector3> LinePointPos = new List<Vector3>();


    private Vector3 MousePos, LastPos, MousePos2, LastPos2;

    private LineRenderer player;

    int down = 0;//滑鼠判定
    int childist = 0;
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

            MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//new position
            LastPos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));//last position

            player.positionCount = LinePointPos.Count;
            player.SetPositions(LinePointPos.ToArray());

            down = 1;
        }
        if (down == 1)
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
            float dist = Vector3.Distance(LastPos, MousePos);//座標間距
            if (dist > 2.4f)
            {
                WidthGenerate(MousePos, LastPos);//點座標計算函式
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                player.positionCount = LinePointPos.Count;
                player.SetPositions(LinePointPos.ToArray());
                
            }
            MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
            float dist2 = Vector3.Distance(LastPos2, MousePos2);//座標間距
            if (dist2 > 0.2f)
            {

                Generatedist();
                MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));
                LastPos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));

            }


        }
        if (Input.GetMouseButtonUp(0)) down = 0;

    }

    void WidthGenerate(Vector3 pos1, Vector3 pos2)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //Vector3 Vec0 = pos1 - pos2;

        MousePointPos.Add(MousePos);
        LinePointPos.Add(MousePos);

        

    }

    void Generatedist() {
        MousePointPos2.Add(MousePos);
        LinePointPos.Add(MousePos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < MousePointPos.Count; i++)
        {           
            Gizmos.DrawSphere(MousePointPos[i], 0.2f);
        }
        for (int n = 0; n < MousePointPos2.Count; n++)
        {
            Gizmos.DrawSphere(MousePointPos2[n], 0.1f);
        }

    }
}
