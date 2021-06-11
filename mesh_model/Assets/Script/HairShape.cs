using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HairShape : MonoBehaviour
{

    public float Hradius = 5;
    public float Vradius = 10;
    int block = 12;


    public Vector3[] PointPos;
    // Start is called before the first frame update
    void Start()
    {
        PointPos = new Vector3[block + 1];
        CreatPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatPosition()
    {
        int degree = 360;
        float angle = degree * Mathf.Deg2Rad;
        float curAngle = angle / 2;
        float deltaAngle = angle / block;

        PointPos[0] = new Vector3(1, 1, 0);

        for (int i = 1; i < PointPos.Length; i++)
        {
            float cos = Mathf.Cos(curAngle);
            float sin = Mathf.Sin(curAngle);
            Debug.Log(cos);
            Debug.Log(sin);
            PointPos[i] = new Vector3(PointPos[0].x + cos * Hradius, PointPos[0].y + sin * Vradius, 0);
            curAngle -= deltaAngle;

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Length; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.5f);
        }

    }

}
