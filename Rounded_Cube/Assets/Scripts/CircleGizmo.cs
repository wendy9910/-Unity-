using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGizmo : MonoBehaviour
{
    public int resolution = 10;

    private void OnDrawGizmosSelected()
    {
        float step = 2f / resolution;//上下點
        for (int i=0; i<=resolution; i++) {
            ShowPoint(i*step-1f, -1f);
            ShowPoint(i*step-1f, 1f);
        }
        for (int i = 1; i < resolution; i++) {//左右點
            ShowPoint(-1f, i * step - 1f);
            ShowPoint(1f, i * step - 1f);
        }
    }
    private void ShowPoint(float x,float y)
    {
        Vector2 square = new Vector2(x,y);
        Vector2 circle = square.normalized;
        circle.x = square.x * Mathf.Sqrt(1f - square.y * square.y * 0.5f);
        circle.y = square.y * Mathf.Sqrt(1f - square.x * square.x * 0.5f);


        Gizmos.color = Color.black;//外部黑球 圍成正方形框
        Gizmos.DrawSphere(square, 0.025f);

        Gizmos.color = Color.white;//內部白球 圍成圓形框
        Gizmos.DrawSphere(square, 0.025f);

        Gizmos.color = Color.yellow;//黑球連白球
        Gizmos.DrawLine(square, circle);

        Gizmos.color = Color.gray;//白球連原點(0,0)
        Gizmos.DrawLine(circle, Vector2.zero);
    }

}
