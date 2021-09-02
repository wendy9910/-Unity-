using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    public List<Vector3> PointPos = new List<Vector3>();
    public List<Vector3> UpdatePointPos = new List<Vector3>();
    Vector3 oldPos, newPos;
    float length = 10f;
    Vector3 NormaizelVec;
    int down = 0;
    // Start is called before the first frame update
    void Start()
    {
        /*float angle = Mathf.PI * 2f;
        for (int i = -10; i < 10; i++)
        {
            float x = i , y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(x, y + 4f , 0);
            PointPos.Add(Position);
            angle += Mathf.PI * 0.2f;
        }*/

        /*for (float angle = -Mathf.PI*6; angle < Mathf.PI*6; angle+= 0.9f) 
        {
            float x = angle * 0.9f, y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(x, y + 1f , 0);
            PointPos.Add(Position);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldPos = newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            PointPos.Add(oldPos);
            down = 1;
        }
        if (down == 1)
        {
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            float dist = Vector3.Distance(oldPos,newPos);
            Debug.Log(dist);
            
            if (dist > length)
            {
                n = 0;
                NormaizelVec = newPos - oldPos;
                NormaizelVec = Vector3.Normalize(NormaizelVec);
                Vector3 NewVec = new Vector3(NormaizelVec.x * length, NormaizelVec.y * length, NormaizelVec.z * length);
                newPos = NewVec + oldPos;
                PointPos.Add(newPos);

                if (n == 0)
                {
                    Wave(NormaizelVec);
                    n = 1;
                }
                oldPos = newPos;
            }
            
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            down = 0;
        }
    }

    int n = 0;
    public void Wave(Vector3 NVec) 
    {
        /*float angle = Mathf.PI * 0.2f;
        Vector3 TempVec = PointPos[0];//0 1 2 3
        if (n == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                float x = i, y = Mathf.Sin(angle);
                Vector3 Position = new Vector3(TempVec.x, TempVec.y + y, 0);
                UpdatePointPos.Add(Position);
                angle += Mathf.PI * 0.2f;
                TempVec += NVec;
            }
            n = 1;
        }*/
        
        Vector3 TempVec = PointPos[PointPos.Count - 2];
        for (float angle = -Mathf.PI; angle < Mathf.PI; angle += 0.9f)
        {
            float x = angle * 0.9f, y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(TempVec.x, y + TempVec.y, 0);
            UpdatePointPos.Add(Position);
            TempVec += (NVec*length)/7;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < UpdatePointPos.Count; i++)
        {
            Gizmos.DrawSphere(UpdatePointPos[i], 0.5f);
        }
    }
}
