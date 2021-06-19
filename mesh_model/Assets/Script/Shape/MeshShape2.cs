using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshShape2 : MonoBehaviour
{
    public static List<Vector3> PointPos = new List<Vector3>(); //儲存路徑座標
    private Vector3[] thickness1;//計算寬度增加座標
    private Vector3[] thickness2;

    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
    public int width = 1;//調整寬度

    int down = 0;//滑鼠判定
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlWidth();
        if (Input.GetMouseButtonDown(0))
        {

            OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position

            down = 1;
        }
        if (down == 1)
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position
            float dist = Vector3.Distance(OldPos, NewPos);
            if (dist > 1.0f)
            {
                PosGenerate(NewPos, OldPos);
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position//old position

            }
            if (PointPos.Count >= (width * 2 + 1) * 2)
            {
               

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            count++;
            CreateShape();
            PointPos.Clear();
            down = 0;

           

        }
    }

    void PosGenerate(Vector3 pos1, Vector3 pos2)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //右左兩個延伸點座標矩陣
        thickness1 = new Vector3[width];
        thickness2 = new Vector3[width];

        //算兩點向量差
        Vector3 Vec0 = pos1 - pos2;//兩點移動方向向量

        for (int i = 0, j = thickness1.Length; i < thickness1.Length; i++, j--)//widthAdd1
        {
            Vector3 Vec1 = new Vector3((Vec0.y) * j, (-Vec0.x) * j, Vec0.z * j);
            thickness1[i] = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, pos1.z + Vec1.z);
            PointPos.Add(thickness1[i]);
        }

        PointPos.Add(NewPos);

        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, (-Vec0.z) * j);
            thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, pos1.z + Vec2.z);
            PointPos.Add(thickness2[i]);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
        for (int i = 0; i < NewPointPos.Count; i++) 
        {
            Gizmos.DrawSphere(NewPointPos[i], 0.1f);
        }
    }


    public List<Vector3> NewPointPos = new List<Vector3>();

    void CreateShape() 
    {
        int len = PointPos.Count / (3+(width-1)*2);
        float w = 0.1f;
        for (int i = 0, j = 0,x = 1, n = 1; i < PointPos.Count; i++,x++) 
        {
            
            
            if (i == ((3 + (width - 1) * 2)/2 + j * 5)) NewPointPos.Add(PointPos[i]);
            else
            {

                NewPointPos.Add(new Vector3(PointPos[i].x * w * n, PointPos[i].y * w * n, PointPos[i].z ));
            }

            if (j == len / 2) n--;
            else n++;

            if (x % (3 + (width - 1) * 2) == 0) j++;
        
        }
        
    
    
    }


    public void controlWidth()
    {
        if (Input.GetKeyDown("down") && width > 1 && down == 0)//設定mesh寬度
        {
            width--;
            Debug.Log("Range" + width);
        }
        if (Input.GetKeyDown("up") && down == 0)
        {
            width++;
            Debug.Log("Range" + width);
        }

    }
}
