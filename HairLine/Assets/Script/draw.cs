using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{
    private LineRenderer player;
    public List<Vector3> PointPos = new List<Vector3>();
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
            PointPos.Add(MousePos);
            player.numCapVertices = 2;//端點圓度
            player.numCornerVertices = 2;//拐彎圓滑度
            player.positionCount = PointPos.Count;
            player.SetPositions(PointPos.ToArray());
  
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i <= PointPos.Count-1; i++)
            {
                Debug.Log(PointPos[i]);
            }
        }
     
    }

    private void OnMouseDrag()
    {
        
    }

}
