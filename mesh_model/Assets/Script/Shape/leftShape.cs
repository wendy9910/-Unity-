using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftShape : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> PointPos = new List<Vector3>();
    public List<Vector3> PointPosSet = new List<Vector3>();

    public float Hradius = 1.0f;
    public float Vradius = 1;

    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
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
                    
                }
                //PointPosSet.Clear();
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            LeftGenerate();
            //PointPos.Clear();
            down = 0;

        }
    }
   
    void LeftGenerate() 
    {
        PointPosSet.AddRange(PointPos);
        int mid = PointPos.Count / 2;

        int degree = 45;
        float angle = degree * Mathf.Deg2Rad;
        float curAngle = angle / 2;
   
        float cos = Mathf.Cos(curAngle);
        float sin = Mathf.Sin(curAngle);

        float w = 0f;

        for (int i = PointPos.Count, j = 1; i < (PointPos.Count - 2) * 2 ; i++, j++)
        {

            PointPosSet.Add(new Vector3(PointPos[j].x + cos * (Hradius + w), PointPos[j].y + sin * Vradius, 0));

            if (j <= mid) w += 0.5f;
            else w -= 0.5f;

            if (j == mid)
            {
                j = 0;

                degree = 315;
                angle = degree * Mathf.Deg2Rad;
                Hradius = 1;
                curAngle = angle / 2;
                cos = Mathf.Cos(curAngle);
                sin = Mathf.Sin(curAngle);
                w = 0;
            }

        }
        //Hradius = 1;




    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPosSet.Count; i++)
        {
            Gizmos.DrawSphere(PointPosSet[i], 0.1f);
        }
    }

}
