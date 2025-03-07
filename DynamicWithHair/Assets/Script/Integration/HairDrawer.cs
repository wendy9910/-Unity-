﻿using System.Collections;
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
    public float TwistCurve = 0.5f;
    public float WaveCurve = 0.9f;
    int n = 0;
    public int BallCount = 0;

    public static List<Vector3> PointPos = new List<Vector3>();//儲存座標
    public static List<Vector3> UpdatePointPos = new List<Vector3>();//變形更新點座標
    public List<GameObject> HairModel = new List<GameObject>();//儲存髮片
    public List<GameObject> BallGroup = new List<GameObject>();

    Vector3 NewPos, OldPos;

    public MeshGenerate CreateHair;
    public PositionGenerate CreatePosition;
    public Joint DynamicHair;
    public Texture HairTexture, hairnormal;

    //player位移
    public GameObject playerMove;
    public GameObject ball;
    GameObject HairGroup;
    public static GameObject Ball;

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
        ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        if (ControllerDown == 0) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                HairGroup = new GameObject();
                HairGroup.name = "HairGroup";
                HairGroup.transform.SetParent(gameObject.transform);
                ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
                GameObject Model = new GameObject();
                Model.transform.SetParent(HairGroup.transform);
                HairModel.Add(Model);
                HairModel[count].name = "FreeHair" + count;
                NewPos = OldPos = ball.transform.position;
                PointPos.Add(OldPos);
            
                ControllerDown = 1;
            }
        }
        if (ControllerDown == 1)
        {
            ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            NewPos = ball.transform.position;
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
                CreatePosition.VectorCross(ball.transform.up, ball.transform.forward, ball.transform.right);
                if (HairStyleState == 1) CreatePosition.StraightHairtyle(PointPos, InputRange, InputRangeThickness);
                if (HairStyleState == 2) CreatePosition.DimandHiarStyle(PointPos, InputRange, InputRangeThickness);
                if (HairStyleState == 3) CreatePosition.WaveHairStyle(PointPos, InputRange, InputRangeThickness, WaveCurve);
                if (HairStyleState == 4) CreatePosition.TwistHairStyle(PointPos, InputRange, InputRangeThickness,TwistCurve);
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
            Ball = new GameObject();
            Ball.name = "BallGroup" + BallCount;
            Ball.transform.SetParent(HairGroup.transform);
            //DynamicHair = HairGroup.AddComponent<Joint>();
            AddSphere();
            DynamicHair = HairGroup.AddComponent<Joint>();
            DynamicHair.Set(BallCount);
            PointPos.Clear();
            BallCount++;
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
        if (Input.GetKeyDown("3")) HairStyleState = 3;
        if (Input.GetKeyDown("4")) HairStyleState = 4;

        if (Input.GetKeyDown("s") && WaveCurve > 0.2f) WaveCurve -= 0.1f ;
        if (Input.GetKeyDown("w") && WaveCurve < 0.8f) WaveCurve += 0.1f;

        if (Input.GetKeyDown("a") && TwistCurve > 0.5f) TwistCurve -= 0.1f;
        if (Input.GetKeyDown("d") && TwistCurve < 0.8f) TwistCurve += 0.1f;//越大越捲
    }


    void AddSphere()
    {
        for (int i = 0; i < PointPos.Count; i += 2, n++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "sphere" + i / 2;
            sphere.transform.position = PointPos[i];
            sphere.transform.localScale = new Vector3(length, length, length);
            sphere.transform.SetParent(Ball.transform);
            sphere.GetComponent<MeshRenderer>().enabled = false;
            HingeJoint joint;
            joint = sphere.AddComponent<HingeJoint>();
            joint.anchor = new Vector3(0f,2f,0f);
            BallGroup.Add(sphere);
            if (i > 0) joint.connectedBody = BallGroup[n - 1].GetComponent<Rigidbody>();
            Rigidbody sphererig;
            sphererig = sphere.GetComponent<Rigidbody>();
            sphererig.mass *=0.5f;
        }
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
