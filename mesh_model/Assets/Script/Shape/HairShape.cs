using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HairShape : MonoBehaviour
{

    public float Hradius = 1.0f;
    public float Vradius = 1;
    int block = 15;

    public List<Vector3> PointPosSet = new List<Vector3>();

    public List<Vector3> PointPos = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        CreatPosition();
    }

    // Update is called once per frame
    void Update()
    {
        PointPos.Add(new Vector3(1, 1, 0));
        PointPos.Add(new Vector3(1, 2, 0));
        PointPos.Add(new Vector3(1, 3, 0));
        PointPos.Add(new Vector3(1, 4, 0));
        PointPos.Add(new Vector3(1, 5, 0));
        PointPos.Add(new Vector3(1, 6, 0));
        PointPos.Add(new Vector3(1, 7, 0));

        PointPos.AddRange(PointPos);
    }

    public void CreatPosition()
    {
        int degree = 45;
        float angle = degree * Mathf.Deg2Rad;
        
        float curAngle = angle / 2;
        float deltaAngle = angle / block;

        float cos = Mathf.Cos(curAngle);
        float sin = Mathf.Sin(curAngle);

        /*float cos = Mathf.Cos(curAngle);
        float sin = Mathf.Sin(curAngle);

        PointPos[7] = new Vector3(PointPos[1].x + cos * (Hradius + 0), PointPos[1].y + sin * Vradius, 0);
        PointPos[8] = new Vector3(PointPos[2].x + cos * (Hradius + 0.5f), PointPos[2].y + sin * Vradius, 0);
        PointPos[9] = new Vector3(PointPos[3].x + cos * (Hradius + 1.0f), PointPos[3].y + sin * Vradius, 0);
        PointPos[10] = new Vector3(PointPos[4].x + cos * (Hradius + 0.5f), PointPos[4].y + sin * Vradius, 0);
        PointPos[11] = new Vector3(PointPos[5].x + cos * (Hradius + 0), PointPos[5].y + sin * Vradius, 0);

        degree = 315;
        angle = degree * Mathf.Deg2Rad;

        Hradius = 1;

        curAngle = angle / 2;
        cos = Mathf.Cos(curAngle);
        sin = Mathf.Sin(curAngle);

        PointPos[12] = new Vector3(PointPos[1].x + cos * (Hradius + 0), PointPos[1].y + sin * Vradius, 0);
        PointPos[13] = new Vector3(PointPos[2].x + cos * (Hradius + 0.5f), PointPos[2].y + sin * Vradius, 0);
        PointPos[14] = new Vector3(PointPos[3].x + cos * (Hradius + 1.0f), PointPos[3].y + sin * Vradius, 0);
        PointPos[15] = new Vector3(PointPos[4].x + cos * (Hradius + 0.5f), PointPos[4].y + sin * Vradius, 0);
        PointPos[16] = new Vector3(PointPos[5].x + cos * (Hradius + 0), PointPos[5].y + sin * Vradius, 0);*/

        

        float w = 0f;

        for (int i = PointPos.Count,j=1; i < (PointPos.Count-2)*2; i++,j++)
        {

            PointPosSet.Add(new Vector3(PointPos[j].x + cos * (Hradius + w), PointPos[j].y + sin * Vradius, 0));
            //PointPos[i] = new Vector3(PointPos[j].x + cos * (Hradius + w), PointPos[j].y + sin * Vradius, 0);

            if (j <= PointPos.Count/2) w += 0.5f;
            else w -= 0.5f;

            if (j == PointPos.Count-2) {
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
