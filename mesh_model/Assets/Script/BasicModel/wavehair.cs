using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavehair : MonoBehaviour
{
    List<Vector3> PointPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for (float angle = -Mathf.PI*3; angle < Mathf.PI*3; angle+=0.2f) 
        {
            float x = angle * 0.9f, y = Mathf.Sin(angle);
            Vector3 Position = new Vector3(x, y + 1f , 0);
            PointPos.Add(Position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
