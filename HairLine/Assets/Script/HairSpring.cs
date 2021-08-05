using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSpring : MonoBehaviour
{
    private LineRenderer player;
    public List<Vector3> MousePointPos = new List<Vector3>();  
    public List<GameObject> SphereGroup = new List<GameObject>();
    private Vector3 LastPos, MousePos;
    private bool Down;
    int number0 = 1, number1 = 0;
    private Vector3 g = new Vector3(0.0f, 9.8f, 0.0f);
    private Vector3 n1 = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 n = new Vector3(0.0f, 0.0f, 0.0f);
    public List<Vector3> Vec = new List<Vector3>(); 

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.green, Color.gray);
        player.SetWidth(0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0))
        {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

            player.numCapVertices = 2;//端點圓度
            player.numCornerVertices = 2;//拐彎圓滑度

            player.positionCount = MousePointPos.Count;
            player.SetPositions(MousePointPos.ToArray());
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            MousePointPos.Add(LastPos);
            Vec.Add(new Vector3(0f,0f,0f));
            Down = true;
            if (number1 > number0) number0++;
            number1++;
        }
        if (Down == true)
        {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 0.05f)
            {//更新座標

                Vector3 Vec1 = MousePos - LastPos;
                Vec1 = Vector3.Normalize(Vec1);
                Vec1 = new Vector3(Vec1.x*0.05f,Vec1.y*0.05f,Vec1.z*0.05f);
                MousePos = Vec1 + LastPos;
                MousePointPos.Add(MousePos);
                LastPos = MousePos;

                player.positionCount = MousePointPos.Count;
                player.SetPositions(MousePointPos.ToArray());
                GameObject sphere;
                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                sphere.transform.position = MousePos;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Down = false;
            MousePointPos.Clear();
            player = null;

        }
        if (number1 > number0)
        {
            for (int i = 0; i <= SphereGroup.Count - 1; i++)
            {
                Destroy(SphereGroup[i]);
            }
            number0++;
        }
        
    }
    
}
