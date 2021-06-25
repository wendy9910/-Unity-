using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    public static List<Vector3> PointPos = new List<Vector3>(); //儲存路徑座標
    public static List<Vector3> UpdatePoint = new List<Vector3>();
    public static List<Vector3> LenPoint = new List<Vector3>();

    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
    public int width = 1;//調整寬度
    public int Select = 0;//選擇頭髮style
    public int colorSelect = 1;//選擇頭髮顏色
    int down = 0;//滑鼠判定
    float widthAdj = 0.15f;//寬度參數

    public MeshGenerate CreatHair;
    public PositionGenerate CreatePosition;

    GameObject Hairmodel;
    public int count = 0;
    int CopyCount = 0;
    int clearMesh = 0;
    int chickUndo = 0;
    int tempCount;
    // Start is called before the first frame update
    void Start()
    {
        Hairmodel = new GameObject();
        Hairmodel.name = "HairModel";
        CreatHair = Hairmodel.AddComponent<MeshGenerate>();
        CreatePosition= Hairmodel.AddComponent<PositionGenerate>();
        Debug.Log("按Space 設定寬度");
    }

    // Update is called once per frame
    void Update()
    {
        controlWidth();
        controlMesh();

        if (Input.GetMouseButtonDown(0)) 
        {
            if (chickUndo == 1)
            {
                CreatHair = Hairmodel.GetComponent<MeshGenerate>();
                CreatHair.undoMeshUpdate(count, CopyCount);
                chickUndo = 0;
            }

            OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position
            down = 1;
        }
        if (down == 1) 
        { 
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position
            float dist = Vector3.Distance( OldPos,NewPos);
            if (dist > 1.0f) 
            {
                CreatePosition = Hairmodel.GetComponent<PositionGenerate>();
                CreatePosition.PosGenerate(NewPos,OldPos,width,PointPos,Select,widthAdj);
                OldPos= NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position//old position

            }
            if (PointPos.Count >= (3 + (width - 1) * 2) * 3)
            {
                //if(Hairmodel.GetComponent<MeshGenerate>() == null) CreatHair = Hairmodel.AddComponent<MeshGenerate>();//判斷是否已經存在組件(MeshGenerate.cs)
                CreatHair = Hairmodel.GetComponent<MeshGenerate>();
                CreatHair.Selectcolor(colorSelect);
                CreatHair.meshGenerate(count,width,UpdatePoint);//呼叫MeshGenerate.cs中的meshGenerate函式
                
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (PointPos.Count >= (3 + (width - 1) * 2) * 3) count++;
            PointPos.Clear();
            LenPoint.Clear();
            UpdatePoint.Clear();
            CopyCount = count;
            down = 0;
        }
        
    }

    

    public void controlWidth()//寬度&髮片風格設定 
    {
        if (Input.GetKeyDown("down") && width > 1 && down==0)//設定mesh寬度
        {
            width--;
            widthAdj -= 0.05f;
            Debug.Log("Range" + width);
        }
        if (Input.GetKeyDown("up") && down == 0)
        {
            width++;
            widthAdj += 0.05f;
            Debug.Log("Range" + width);
        }

        if (Input.GetKeyDown("1")) Select = 0;
        if (Input.GetKeyDown("2")) Select = 1;

    }
    
    public void controlMesh()//髮片控制 clear undo redo color 
    {
        
        /*if (Input.GetKeyDown("c") && chickUndo ==1)//清除前Undo再復原會有問題
        {
            Debug.Log("chick "+ chickUndo);
            CreatHair = Hairmodel.GetComponent<MeshGenerate>();
            CreatHair.undoMeshUpdate(count, CopyCount);
            //1.undo - clear - undo 會壞掉
            clearMesh = 2;
            tempCount = count;
        }*/
        if (Input.GetKeyDown("c")) 
        {
            count = 0;
            CreatHair.ClearMesh();
            clearMesh = 1;
        }

        if (Input.GetKeyDown("u")) 
        { 
            CreatHair = Hairmodel.GetComponent<MeshGenerate>();
            CreatHair.undoMesh();

            if (clearMesh == 1)
            {
                count = CopyCount;
                clearMesh = 0;
            }


            else if (count > 0) count--;
            if (count < CopyCount) chickUndo = 1;

            CreatHair.meshGenerate(count, width, UpdatePoint);

        }
        if (Input.GetKeyDown("r") && chickUndo == 1)
        {
            if (count <= CopyCount) count++;
            if (count == CopyCount) chickUndo = 0;
            CreatHair = Hairmodel.GetComponent<MeshGenerate>();
            CreatHair.meshGenerate(count, width, UpdatePoint);

        }


        if (Input.GetMouseButtonDown(1))
        {
            Vector3 RemovePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            CreatHair.RemoveMesh(RemovePos);
        }

        if (Input.GetKeyDown("3")) colorSelect = 1;
        if (Input.GetKeyDown("4")) colorSelect = 2;

    }

    


}
