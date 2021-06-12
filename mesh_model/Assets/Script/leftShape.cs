using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftShape : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> PointPos = new List<Vector3>();


    private Vector3 NewPos, OldPos,FristPos;//零時座標變數 New & Old
    int down = 0;//滑鼠判定

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            NewPos = OldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position //old position
            FristPos = NewPos;
            down = 1;
        }
        if (down == 1)
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));//new position
            float dist = Vector3.Distance(OldPos, NewPos);
            if (dist > 1.0f)
            { 
                PointPos.Add(NewPos);
                if (PointPos.Count > 2 && PointPos.Count % 2 != 0)
                {
                    LeftGenerate();
                }
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            PointPos.Clear();
            down = 0;

        }
    }
   
    void LeftGenerate() 
    {
        int mid = PointPos.Count / 2;
        float Len = Vector3.Distance(FristPos, PointPos[mid]);
        Debug.Log(Len);

        float Hradius = 1;

        int degree = 360;
        int block = PointPos.Count * 3;
        float angle = degree * Mathf.Deg2Rad;
        float curAngle = angle / 2;
        float deltaAngle = angle / block;

        for (int i = 0; i < block; i++)
        {
            float cos = Mathf.Cos(curAngle);
            float sin = Mathf.Sin(curAngle);

            PointPos.Add(new Vector3(PointPos[mid].x + cos * Hradius, PointPos[0].y + sin * Len, 0));
            curAngle -= deltaAngle;
        }
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
    }

}
