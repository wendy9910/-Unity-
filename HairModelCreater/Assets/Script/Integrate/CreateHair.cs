using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHair : MonoBehaviour
{
    int Controllerdown = 0;//判定手把動作
    int count = 0;//紀錄髮片片數
    int HairWidth = 1;//髮片寬度
    float length = 0.5f;//算點距

    Vector3 NewPos, OldPos;
    public static List<Vector3> PointPos = new List<Vector3>();//儲存座標點
    public List<GameObject> HairModel = new List<GameObject>();//儲存髮片GameObject

    public MeshGenerate HairCreater;
    public PositionGenerate PositionCreater;

    // Start is called before the first frame update
    void Start()
    {
        PositionCreater = gameObject.AddComponent<PositionGenerate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controllerdown == 0)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                GameObject Model = new GameObject();
                HairModel.Add(Model);
                HairModel[count].name = "HairModel" + count;

                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
                Controllerdown = 1;
            }
        }
        if (Controllerdown == 1) 
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
            float dist = Vector3.Distance(OldPos,NewPos);
            if (dist > length) 
            {
                PositionCreater = gameObject.GetComponent<PositionGenerate>();
                PositionCreater.GetPosition(OldPos,NewPos,HairWidth);

                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
            }
            if (PointPos.Count >= (3+(HairWidth-1)*2)*2) 
            {
                if (HairModel[count].GetComponent<MeshGenerate>() == null) HairCreater = HairModel[count].AddComponent<MeshGenerate>();
                else HairCreater = HairModel[count].GetComponent<MeshGenerate>();
                HairCreater.GenerateMesh(PointPos,HairWidth);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (PointPos.Count >= (3 + (HairWidth - 1) * 2) * 2) count++;
                else 
                {//清除建立失敗的髮片GameObject
                    int least = HairModel.Count - 1;
                    Destroy(HairModel[least]);
                    HairModel.RemoveAt(least);
                }
                PointPos.Clear();//清除座標 重新開始
                Controllerdown = 0;
            }       
        }

    }
}
