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
    private Vector3 g = new Vector3(0.0f, 0.098f, 0.0f);
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
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                MousePointPos.Add(MousePos);
                Vec.Add(new Vector3(0f, 0f, 0f));
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                player.positionCount = MousePointPos.Count;
                player.SetPositions(MousePointPos.ToArray());
                GameObject sphere;
                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                sphere.transform.position = MousePos;
            }

            //Debug.Log(dist);

        }
        if (Input.GetMouseButtonUp(0))
        {
            Spring();
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
    void Spring() 
    {
        for (int i = 1; i < MousePointPos.Count; i++) {
            Vector3 Vec1 = Vec[i];
            Vector3 pt1 = MousePointPos[i];
            Vector3 d = new Vector3(0.0f , 0.045f, 0.0f);

            Vec1 += g;
            pt1 += Vec1;

            if (i != MousePointPos.Count - 1) {
                Vector3 pt0 = MousePointPos[i + 1];
                float d2 = Vector3.Distance(pt1, pt0);
                Vector3 dist2 = new Vector3(d2,d2,0);
                if (d2 > 0.045f) {
                    n1 = (pt0 - pt1).normalized;
                    dist2 -= d;
                    n1.y *= dist2.y;
                    n1.x *= dist2.x;
                    pt1.y += n1.y;
                    pt1.x += n1.x;
                    
                }
                Debug.Log(d2);
                MousePointPos[i] = pt1;
            }

            Vector3 pt2 = MousePointPos[i-1];
            float d1 = Vector3.Distance(pt1, pt2);
            Vector3 dist1 = new Vector3(d1, d1, 0);
            if (d1 > 0.045f) {
                n = (pt2 - pt1).normalized;
                dist1 -= d;
                n.y *= dist1.y;
                n.x *= dist1.x;
                pt1.y += n.y;
                pt1.x += n.x;   
            }
            MousePointPos[i] = pt1;

        }
    
    }
}
