using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavehair : MonoBehaviour
{
    public List<Vector3> PointPos = new List<Vector3>();
    public List<Vector3> UpdatePointPos = new List<Vector3>();
    Vector3 OldPos, NewPos;

    int down = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*for (float angle = -Mathf.PI*5; angle < Mathf.PI*5; angle+=0.2f) 
        {
            float x = angle * 0.9f, y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(x, y + 1f , 0);
            PointPos.Add(Position);
        }*/

        /*float z1 = 1, x1 = 1, y1 = 1, angle1 = 0;
        for (int i = 0; i < 20; i++)
        {
            x1 = Mathf.Cos(angle1);
            y1 = Mathf.Sin(angle1);
            z1 = x1 + i * y1;
            Vector3 Position = new Vector3(x1, z1, y1);
            PointPos.Add(Position);

            angle1 += 0.2f;

        }*/
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
        /*float a = Mathf.PI;
        for (int i = 0,x=-10; i < 20; i++,x++) 
        {
            float y = Mathf.Sin(a);
            Vector3 Position = new Vector3(x, y + 1f, 0);
            PointPos.Add(Position);

            a -= 0.9f;
        }*/
        /*
        float t = Mathf.PI * 1.5f;
        float a = 0;
        for (float i = 0.1f; i < 10;i+=0.3f) 
        {
            float b = i;

            float r = a + b * t;
            float x = r * Mathf.Cos(t);
            float y = r * Mathf.Sin(t);

            Vector3 pos = new Vector3(x,y,0);
            PointPos.Add(pos);
            t += 0.2f;
        }
         /*
        阿基米德螺旋波
        float t = Mathf.PI * 1.5f;
        float a = 0;
        for (float i = 0.1f; i < 10;i+=0.3f) 
        {
            float b = i;

            float r = a + b * t;
            float x = r * Mathf.Cos(t);
            float y = r * Mathf.Sin(t);

            Vector3 pos = new Vector3(x,y,0);
            PointPos.Add(pos);
            t += 0.2f;
        }*/

       
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            NewPos = OldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            down = 1;
        }

        if (down == 1) 
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30.0f));
            float dist = Vector3.Distance(OldPos,NewPos);
            if (dist > 1) 
            {
                PointPos.Add(OldPos);
            }
            OldPos = NewPos;
            Wave();

        }
        if (Input.GetMouseButtonUp(0))
        {
            down = 0;
        }

    }
    List<Vector3> temp = new List<Vector3>();
    public void Wave() 
    {
        temp.Clear();
        int i = 0,n = 0;
        for (float angle = Mathf.PI * PointPos.Count; angle > -Mathf.PI * PointPos.Count; angle -= 0.5f)
        {
            float x = PointPos[i].x + angle * 0.9f, y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(x, y + 0.5f, 0);
            temp.Add(Position);
            n++;
            if (n % 10 == 0) i++;
            Debug.Log("Hi");
        }
        UpdatePointPos.AddRange(temp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.5f);
        }
    }
}
