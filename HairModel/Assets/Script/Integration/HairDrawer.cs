using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairDrawer : MonoBehaviour
{
    int ControllerDown = 0; //按下滑鼠
    int count = 0;//髮片數量
    public static int HairWidth = 1;//髮片寬度
    public static int HairStyleState = 1;//髮片風格選擇
    float length = 0.04f;//New & Old間距 0.05f --- 0.7f  0.02f --- 2.5f 0.04f --- 1.8f
    public float WidthLimit = 0.05f;//最小0.05,最大0.5
    public int InputRange = 10;//(1~12)
    public int InputRangeThickness = 10;

    public static List<Vector3> PointPos = new List<Vector3>();//儲存座標
    public static List<Vector3> UpdatePointPos = new List<Vector3>();//變形更新點座標
    public List<GameObject> HairModel = new List<GameObject>();//儲存髮片

    Vector3 NewPos, OldPos;

    public MeshGenerate CreateHair;
    public PositionGenerate CreatePosition;
    public Texture HairTexture, hairnormal;

    //player位移
    public GameObject playerMove;

  
    // Start is called before the first frame update
    private void Start()
    {
        CreatePosition = gameObject.AddComponent<PositionGenerate>();
        gameObject.transform.position = playerMove.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        WidthControl();
        if (ControllerDown == 0) 
        {
            if (Input.GetMouseButtonDown(0))
            {

                GameObject Model = new GameObject();
                Model.transform.SetParent(gameObject.transform);
                HairModel.Add(Model);
                HairModel[count].name = "FreeHair" + count;
                NewPos = OldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
                PointPos.Add(OldPos);
                ControllerDown = 1;
            }
        }
        if (ControllerDown == 1)
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            float Distance = Vector3.Distance(OldPos,NewPos);
            if (Distance > length) 
            {
                Vector3 NormaizelVec = NewPos - OldPos;
                NormaizelVec = Vector3.Normalize(NormaizelVec);
                NormaizelVec = new Vector3(NormaizelVec.x * length, NormaizelVec.y * length, NormaizelVec.z * length);
                NewPos = NormaizelVec + OldPos;
                PointPos.Add(NewPos);
                CreatePosition = gameObject.GetComponent<PositionGenerate>();
                //CreatePosition.PosGenerate(OldPos,NewPos, InputRange);
                if (HairStyleState==1) CreatePosition.StraightHairtyle(PointPos, InputRange, InputRangeThickness);
                if(HairStyleState==2) CreatePosition.DimandHiarStyle(PointPos, InputRange, InputRangeThickness);
                OldPos = NewPos;
                
            }
            if (PointPos.Count >= 2) 
            {
                if (HairModel[count].GetComponent<MeshGenerate>() == null) CreateHair = HairModel[count].AddComponent<MeshGenerate>();
                else CreateHair = HairModel[count].GetComponent<MeshGenerate>();
                CreateHair.GenerateMesh(UpdatePointPos,HairWidth);
                MeshGenerate.GethairColor.SetTexture("_MainTex", HairTexture);
                MeshGenerate.GethairColor.SetTexture("_BumpMap", hairnormal);
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (PointPos.Count >= 2) count++;
            else
            {//清除建立失敗的髮片GameObject
                int least = HairModel.Count - 1;
                Destroy(HairModel[least]);
                HairModel.RemoveAt(least);
            }
            PointPos.Clear();
            ControllerDown = 0;
        }

    }

    void WidthControl()
    {
        if (Input.GetKeyDown("down") && InputRange > 1) InputRange--;
        if (Input.GetKeyDown("up") && InputRange < 10) InputRange++;

        if (Input.GetKeyDown("right") && InputRangeThickness > 1) InputRangeThickness--;
        if (Input.GetKeyDown("left") && InputRangeThickness < 10) InputRangeThickness++;

        if (Input.GetKeyDown("1")) HairStyleState = 1;
        if (Input.GetKeyDown("2")) HairStyleState = 2;
    }

    public void PlayerMove()
    {
        if (gameObject.transform.position != playerMove.transform.position) 
        {
            Vector3 Move = playerMove.transform.position - gameObject.transform.position;
            gameObject.transform.position += Move;
        }

    }
}
