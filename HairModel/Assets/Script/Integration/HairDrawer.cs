using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairDrawer : MonoBehaviour
{
    int ControllerDown = 0; //按下滑鼠
    int count = 0;//髮片數量
    int HairWidth = 1;//髮片寬度
    float length = 0.5f;//New & Old間距
    

    public static List<Vector3> PointPos = new List<Vector3>();//儲存座標
    public List<GameObject> HairModel = new List<GameObject>();//儲存髮片

    Vector3 NewPos, OldPos;

    public MeshGenerate CreateHair;
    public PositionGenerate CreatePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerDown == 0) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject Model = new GameObject();
                HairModel.Add(Model);
                HairModel[count].name = "FreeHair" + count;

                NewPos = OldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                ControllerDown = 1;
            }
        }
        if (ControllerDown == 1)
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            float Distance = Vector3.Distance(OldPos,NewPos);
            if (Distance > length) 
            {
                if (HairModel[count].GetComponent<PositionGenerate>() == null) CreatePosition = HairModel[count].AddComponent<PositionGenerate>();
                else CreatePosition = HairModel[count].GetComponent<PositionGenerate>();
                CreatePosition.GeneratePosition(OldPos,NewPos,HairWidth);

                NewPos = OldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }
            if (PointPos.Count >= (3 + (HairWidth - 1) * 2) * 2) 
            {
                if (HairModel[count].GetComponent<MeshGenerate>() == null) CreateHair = HairModel[count].AddComponent<MeshGenerate>();
                else CreateHair = HairModel[count].GetComponent<MeshGenerate>();
                CreateHair.GenerateMesh(PointPos,HairWidth);
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            PointPos.Clear();
            count++;
            ControllerDown = 0;
        }

        if (Input.GetKeyDown("up")) //消除物件的方法
        {
            if (HairModel.Count > 1) 
            {
                int leastObject = HairModel.Count - 1;
                Destroy(HairModel[leastObject]);
                HairModel.RemoveAt(leastObject);
                
            }
        }
    }

    
}
