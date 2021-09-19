using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

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


    public static List<Vector3> PointPos = new List<Vector3>();//儲存座標
    public static List<Vector3> UpdatePointPos = new List<Vector3>();//變形更新點座標
    public static List<Vector3> direction = new List<Vector3>();
    public List<GameObject> HairModel = new List<GameObject>();//儲存髮片

    Vector3 NewPos, OldPos;

    public MeshGenerate CreateHair;
    public PositionGenerate CreatePosition;
    public Texture HairTexture, hairnormal;

    //player位移
    public GameObject playerMove;
    public GameObject ball;

    private string Model;

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
                ball.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
                GameObject Model = new GameObject();
                Model.transform.SetParent(gameObject.transform);
                HairModel.Add(Model);
                HairModel[count].name = "FreeHair" + count;
                NewPos = OldPos = ball.transform.position;
                CreatePosition.VectorCross(ball.transform.up, ball.transform.forward, ball.transform.right);
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
            PointPos.Clear();
            ControllerDown = 0;
        }

    }

    public void SaveModel()
    {
        Model = "Model";
        for (int i = 0; i < HairModel.Count; i++) 
        {
            Mesh mesh = HairModel[i].GetComponent<MeshFilter>().mesh;
            AssetDatabase.CreateAsset(mesh, "Assets/Model/" + Model + i + ".asset");
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

        if (Input.GetKeyDown("a") && TwistCurve > 0.8f) TwistCurve -= 0.1f;
        if (Input.GetKeyDown("d") && TwistCurve < 1.2f) TwistCurve += 0.1f;//越大越捲

        if (Input.GetKeyDown("t")) SaveModel();
    }

    public void PlayerMove()
    {
        if (gameObject.transform.position != playerMove.transform.position) 
        {
            Vector3 Move = playerMove.transform.position - gameObject.transform.position;
            gameObject.transform.position += Move;
        }

    }
    private string MeshToString(MeshFilter mf, Vector3 scale)
    {
        Mesh mesh = mf.mesh;
        Material[] haredMaterials = mf.GetComponent<Renderer>().sharedMaterials;
        Vector2 textureOffset = mf.GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
        Vector2 textureScale = mf.GetComponent<Renderer>().material.GetTextureScale("_MainTex");

        StringBuilder stringBuilder = new StringBuilder().Append("mtllib design.mtl").Append("\n").Append("g ").Append(mf.name).Append("\n");

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vector = vertices[i];
            stringBuilder.Append(string.Format("v {0} {1} {2}\n", vector.x * scale.x, vector.y * scale.y, vector.z * scale.z));
        }

        stringBuilder.Append("\n");

        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        if (mesh.subMeshCount > 1)
        {
            int[] triangles = mesh.GetTriangles(1);

            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (!dictionary.ContainsKey(triangles[j]))
                {
                    dictionary.Add(triangles[j], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 1]))
                {
                    dictionary.Add(triangles[j + 1], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 2]))
                {
                    dictionary.Add(triangles[j + 2], 1);
                }
            }
        }

        for (int num = 0; num != mesh.uv.Length; num++)
        {
            Vector2 vector2 = Vector2.Scale(mesh.uv[num], textureScale) + textureOffset;

            if (dictionary.ContainsKey(num))
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", mesh.uv[num].x, mesh.uv[num].y));
            }
            else
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", vector2.x, vector2.y));
            }
        }

        for (int k = 0; k < mesh.subMeshCount; k++)
        {
            stringBuilder.Append("\n");

            if (k == 0)
            {
                stringBuilder.Append("usemtl ").Append("Material_design").Append("\n");
            }

            if (k == 1)
            {
                stringBuilder.Append("usemtl ").Append("Material_logo").Append("\n");
            }

            int[] triangles2 = mesh.GetTriangles(k);

            for (int l = 0; l < triangles2.Length; l += 3)
            {
                stringBuilder.Append(string.Format("f {0}/{0} {1}/{1} {2}/{2}\n", triangles2[l] + 1, triangles2[l + 2] + 1, triangles2[l + 1] + 1));
            }
        }

        return stringBuilder.ToString();
    }
}
