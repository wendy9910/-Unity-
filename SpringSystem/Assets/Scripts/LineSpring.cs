using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpring : MonoBehaviour
{
    private LineRenderer player;
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> SpherePos = new List<Vector3>();
    public List<GameObject> SphereGroup = new List<GameObject>();
    private Vector3 MousePos, LastPos;
    GameObject sphere;
    Rigidbody RG;
    public float mass1 = 1f;
    int count = 0;
    int v = 5;
    int d = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        RendererSet();
    }

    void Update()
    {
        player = GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0))
        {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            MousePointPos.Add(MousePos);
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

            Draw();

            SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sphere.transform.position = MousePos;
            var sphereRenderer = sphere.GetComponent<Renderer>();
            sphereRenderer.material.SetColor("_Color", Color.red);
            SpherePos.Add(sphere.transform.position);

            RG = sphere.AddComponent<Rigidbody>();
            RG.isKinematic = true;
            RG.mass = mass1;

            d=1;
        }
        if (d==1)
        {
           
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 1f)
            {//更新座標
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                MousePointPos.Add(MousePos);
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

                Draw();

                
                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                sphere.transform.position = MousePos;
                var sphereRenderer = sphere.GetComponent<Renderer>();
                sphereRenderer.material.SetColor("_Color", Color.red);
                SpherePos.Add(sphere.transform.position);

                
                RG = sphere.AddComponent<Rigidbody>();
                RG.isKinematic = true;
                RG.mass = mass1;
            }
        }

        if (Input.GetMouseButtonUp(0)) d = 2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player = null;
            SphereGroup.Clear();
            MousePointPos.Clear();
            SpherePos.Clear();
            
        }

        if (d==2)
        {
           
            Spring();
            Draw();
        }  
    }

    void Spring()
    {
        RG = SphereGroup[0].GetComponent<Rigidbody>();
        RG.isKinematic = true;
        RG.mass = mass1;

        for (int i = 0; i < SphereGroup.Count - 1; i++)
        {
            count = SphereGroup.Count;
            SpringJoint MainSpring = SphereGroup[i].AddComponent<SpringJoint>();

            MainSpring.spring = v * count;
            count--;
            MainSpring.damper = 10.0f;
            Rigidbody otherRG = SphereGroup[i + 1].GetComponent<Rigidbody>();

            otherRG.isKinematic = false;
            otherRG.mass = mass1;
            MainSpring.connectedBody = otherRG;

            SpherePos[i + 1] = SphereGroup[i + 1].transform.position;

        }

    }
    void RendererSet() {
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.black, Color.gray);
        player.SetWidth(0.1f, 0.1f);
        player.numCapVertices = 2;//端點圓度
        player.numCornerVertices = 2;//拐彎圓滑度
    }
    void Draw()
    {
        player.positionCount = SpherePos.Count;
        player.SetPositions(SpherePos.ToArray());
    }
}
