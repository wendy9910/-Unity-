using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{
    private LineRenderer player;
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> PointPos = new List<Vector3>();
    private Vector3 LastPos;
    private bool Down;
    Vector3 MousePos;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.blue, Color.green);
        player.SetWidth(0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0)){

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

            player.numCapVertices = 2;//端點圓度
            player.numCornerVertices = 2;//拐彎圓滑度

            player.positionCount = MousePointPos.Count;
            player.SetPositions(MousePointPos.ToArray());
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            MousePointPos.Add(LastPos);
            Down = true;
        }
        if (Down == true) {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            float dist = Vector3.Distance(LastPos,MousePos);
            if (dist > 0.01f) {//更新座標
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                MousePointPos.Add(MousePos);
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                player.positionCount = MousePointPos.Count;
                player.SetPositions(MousePointPos.ToArray());
            }
            Debug.Log(dist);

        }
        if (Input.GetMouseButtonUp(0)) {
            Down = false;
            MousePointPos.Clear();
            player = null;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i <= MousePointPos.Count - 1; i++)
            {
                Debug.Log(MousePointPos[i]);
            }
        }

    }


   

}
