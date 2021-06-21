using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondShape : MonoBehaviour
{
    public static List<Vector3> PointPos = new List<Vector3>(); //儲存路徑座標
    public List<Vector3> LenPoint = new List<Vector3>();

    private Vector3[] thickness1;//計算寬度增加座標
    private Vector3[] thickness2;

    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
    public int width = 1;//調整寬度
    float widthForPoint = 1;

    public MeshGenerate CreatHair;

    int down = 0;//滑鼠判定

    GameObject Hairmodel;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Hairmodel = new GameObject();
        Hairmodel.name = "HairModel";
        Debug.Log("按Space 設定寬度");

     
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
            if (UpdatePoint.Count >= (3+(width-1)*2)*3)
            {
                if (Hairmodel.GetComponent<MeshGenerate>() == null) CreatHair = Hairmodel.AddComponent<MeshGenerate>();//判斷是否已經存在組件(MeshGenerate.cs)
                else CreatHair = Hairmodel.GetComponent<MeshGenerate>();
                CreatHair.meshGenerate(count, width, UpdatePoint);//呼叫MeshGenerate.cs中的meshGenerate函式
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            count++;
            PointPos.Clear();
            LenPoint.Clear();
            UpdatePoint.Clear();
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
        LenPoint.Add(NewPos);

        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, (-Vec0.z) * j);
            thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, pos1.z + Vec2.z);
            PointPos.Add(thickness2[i]);
        }

        if(LenPoint.Count > 3)UpdatPoint(PointPos);
    }

    public List<Vector3> tempPoint = new List<Vector3>();
    public List<Vector3> UpdatePoint = new List<Vector3>();

    
    void UpdatPoint(List<Vector3> Point)
    {
        UpdatePoint.Clear();
        tempPoint.Clear();
        float w;
        float mid = LenPoint.Count / 2.0f;
        if (LenPoint.Count % 2 != 0) w = widthForPoint / mid;
        else  w = widthForPoint / (mid-1);

        for (int i = 0,x=1; i < Point.Count; i++,x++)
        {
            if (i < 3 + (width - 1))
            {
                tempPoint.Add(LenPoint[0]);
            }
            else if (i >= (3 + (width - 1) * 2) * (LenPoint.Count - 1))
            {
                tempPoint.Add(LenPoint[LenPoint.Count - 1]);
            }
            else 
            {
                if (LenPoint.Count % 2 != 0) {
                    
                    
                    if (i < mid - 1) {
                        if (x % 3 + (width - 1) * 2 == 0) w += w;
                        Vector3 temp = new Vector3(Point[i].x + w, Point[i].y + w,Point[i].z);
                        tempPoint.Add(temp);
                    }
                    else if (i > mid + 1) {
                        if (x % 3 + (width - 1) * 2 == 0) w -= w;
                        Vector3 temp = new Vector3(Point[i].x + w , Point[i].y + w, Point[i].z);
                        tempPoint.Add(temp);
                    }
                    else tempPoint.Add(Point[i]);
                    
                }
                else {
                    //float w = widthForPoint / (mid-1);
                    if (i < mid - 1)
                    {
                        if (x % 3 + (width - 1) * 2 == 0) w += w;
                        Vector3 temp = new Vector3(Point[i].x + w, Point[i].y + w,Point[i].z);
                        tempPoint.Add(temp);
                    }
                    else if (i > mid)
                    {
                        if (x % 3 + (width - 1) * 2 == 0) w -= w;
                        Vector3 temp = new Vector3(Point[i].x + w, Point[i].y + w, Point[i].z);
                        tempPoint.Add(temp);
                    }
                    else tempPoint.Add(Point[i]);

                }
                Debug.Log(w);
            }
        }

        UpdatePoint.AddRange(tempPoint);

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < UpdatePoint.Count; i++)
        {
            Gizmos.DrawSphere(UpdatePoint[i], 0.1f);
        }
    }

    public void controlWidth()
    {
        if (Input.GetKeyDown("down") && width > 1 && down == 0)//設定mesh寬度
        {
            width--;
            widthForPoint--;
            Debug.Log("Range" + width);
        }
        if (Input.GetKeyDown("up") && down == 0)
        {
            width++;
            widthForPoint++;
            Debug.Log("Range" + width);
        }

    }
}
