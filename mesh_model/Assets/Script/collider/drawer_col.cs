﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer_col : MonoBehaviour
{
    public static List<Vector3> PointPos = new List<Vector3>(); //儲存路徑座標
    public static List<Vector3> UpdatePoint = new List<Vector3>();
    public static List<Vector3> LenPoint = new List<Vector3>();
    public List<GameObject> SetCollider = new List<GameObject>();
    public List<string> ColliderName = new List<string>();

    //For Position
    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
    public float length = 0.4f;
    public int width = 1;//調整寬度
    public int Select = 0;//選擇頭髮style
    public float widthAdj = 0;//寬度參數

    public MeshG2 CreatHair;
    public PositionG2 CreatePosition;
    public Controler Control;
    GameObject Hairmodel;

    public int count = 0;//髮片片數
    //For undo redo
    int CopyCount = 0;
    int chickUndo = 0;
    public static int colorSelect = 1;//選擇頭髮顏色
    int down = 0;//滑鼠判定

    //碰撞偵測變數
    int c = 1;
    SphereCollider box;
    GameObject EmptySet;


    // Start is called before the first frame update
    void Start()
    {
        Hairmodel = new GameObject();
        Hairmodel.name = "HairModel";
        CreatHair = Hairmodel.AddComponent<MeshG2>();
        CreatePosition = Hairmodel.AddComponent<PositionG2>();
        ColliderName.Add("none");
        Debug.Log("按Space 設定寬度");
    }

    // Update is called once per frame
    void Update()
    {
        controlWidth();
        controlMesh();

        if (down == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
                down = 1;
            }
        }
        if (down == 1)
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
            float dist = Vector3.Distance(OldPos, NewPos);
            if (dist > length)
            {
                CreatePosition = Hairmodel.GetComponent<PositionG2>();
                CreatePosition.PosGenerate(OldPos, NewPos, width, PointPos, Select, widthAdj);//NewPos & OldPos倒過來解決隊不到點問題
                if (PointPos.Count >= (3 + (width - 1) * 2) * 2) SetCol();
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position//old position
            }
            if (PointPos.Count >= (3 + (width - 1) * 2) * 2)
            {
                //if(Hairmodel.GetComponent<MeshGenerate>() == null) CreatHair = Hairmodel.AddComponent<MeshGenerate>();//判斷是否已經存在組件(MeshGenerate.cs)
                CreatHair = Hairmodel.GetComponent<MeshG2>();
                CreatHair.meshGenerate(count, width, UpdatePoint, Hairmodel);//呼叫MeshGenerate.cs中的meshGenerate函式

            }
            if (Input.GetMouseButtonUp(0))
            {

                if (PointPos.Count >= (3 + (width - 1) * 2) * 2)
                {
                    SetCollider.Add(EmptySet);
                    ColliderName.Add(box.name);
                    count++;
                    c++;
                }
                PointPos.Clear();
                LenPoint.Clear();
                UpdatePoint.Clear();
                CopyCount = count;
                down = 0;
            }
        }
        RayCollider();

    }

    public void controlWidth()//寬度&髮片風格設定 
    {
        if (Input.GetKeyDown("down") && down == 0)//設定mesh寬度
        {
            length -= 0.1f;
            //width--;
            Debug.Log("Range" + width);
        }
        if (Input.GetKeyDown("up") && down == 0)
        {
            length += 0.1f;
            //width++;
            Debug.Log("Range" + width);
        }
        widthAdj = length / 4;

        if (Input.GetKeyDown("1")) Select = 0;
        if (Input.GetKeyDown("2")) Select = 1;
    }


    public void controlMesh()//髮片控制 clear undo redo color 
    {
        CreatHair = Hairmodel.GetComponent<MeshG2>();
        if (Input.GetKeyDown("c"))
        {
            CreatHair.ClearMesh(count);
            CopyCount = count;
            count = 0;
            CreatHair.meshGenerate(count, width, UpdatePoint, Hairmodel);
        }
        if (Input.GetKeyDown("u"))
        {
            CreatHair.undoMesh(count);
            if (count == 0) count = CopyCount;
            else count--;
            CreatHair.meshGenerate(count, width, UpdatePoint, Hairmodel);
        }
        if (CopyCount > count) chickUndo = 1;
        else chickUndo = 0;

        if (Input.GetKeyDown("r") && chickUndo == 1)
        {
            CreatHair.redoMesh();
            count++;
            CreatHair.meshGenerate(count, width, UpdatePoint, Hairmodel);

        }

        if (Input.GetKeyDown("3")) colorSelect = 1;
        if (Input.GetKeyDown("4")) colorSelect = 2;
        if (Input.GetKeyDown("5")) colorSelect = 3;
    }
    public void SetCol()
    {
        EmptySet = new GameObject();
        box = EmptySet.AddComponent<SphereCollider>();
        EmptySet.transform.position = OldPos;
        EmptySet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        box.name = "box" + c;
        box.center = OldPos;
        box.radius = 1.0f;
        box.isTrigger = true;

    }

    public void RayCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            string name = hit.transform.name;

            Debug.Log(name);

        }

    }
    public void RecordCollider()
    {


    }

}



