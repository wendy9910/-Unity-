using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{

    public Vector3[] thickness1;//計算寬度增加座標
    public Vector3[] thickness2;
    public static List<Vector3> GetPoint = drawer.PointPos;

    public static List<Vector3> tempPoint = new List<Vector3>();//變形時暫存用
    public List<Vector3> GetUpdatePoint = drawer.UpdatePoint;

    public List<Vector3> GetLenPoint = drawer.LenPoint;

    public void PosGenerate(Vector3 pos1, Vector3 pos2,int width,List<Vector3> PointPos,int GetSelect,int count)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //右左兩個延伸點座標矩陣
        thickness1 = new Vector3[width];
        thickness2 = new Vector3[width];

        //算兩點向量差
        Vector3 Vec0 = pos1 - pos2;//兩點移動方向向量

        for (int i = 0, j = thickness1.Length; i < thickness1.Length; i++, j--)//widthAdd1
        {
            Vector3 Vec1 = new Vector3((Vec0.y) * j, (-Vec0.x) * j, Vec0.z * j);
            thickness1[i] = new Vector3(pos1.x + Vec1.x, pos1.y + Vec1.y, pos1.z + Vec1.z);
            //thickness1[i] = pos1;
            PointPos.Add(thickness1[i]);
            //PointPos.Add(pos1);
        }

        PointPos.Add(pos1);
        GetLenPoint.Add(pos1);

        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            Vector3 Vec2 = new Vector3((-Vec0.y) * j, (Vec0.x) * j, (-Vec0.z) * j);
            thickness2[i] = new Vector3(pos1.x + Vec2.x, pos1.y + Vec2.y, pos1.z + Vec2.z);
            //thickness2[i] = pos1;
            PointPos.Add(thickness2[i]);
            //PointPos.Add(pos1);
        }


        if(GetLenPoint.Count > 2 && GetSelect == 0)straightStyle(PointPos,width);
        if (GetLenPoint.Count > 2 && GetSelect == 1) diamandStyle(PointPos, width);
        
    }

    public void straightStyle(List<Vector3> Point,int width)
    {
        GetUpdatePoint.Clear();
        tempPoint.Clear();

        for (int i = 0, x = 1; i < Point.Count; i++, x++)
        {
            if (i < 3 + (width - 1))
            {
                tempPoint.Add(GetLenPoint[0]);
            }
            /*else if (i >= (3 + (width - 1) * 2) * (LenPoint.Count - 1))
            {
                tempPoint.Add(LenPoint[LenPoint.Count - 1]);
            }*/
            else
            {
                tempPoint.Add(Point[i]);
            }
        }
        GetUpdatePoint.AddRange(tempPoint);

        if (Input.GetMouseButtonUp(0)) GetLenPoint.Clear();

    }

    public void diamandStyle(List<Vector3> Point, int width)
    {
        GetUpdatePoint.Clear();
        tempPoint.Clear();

        for (int i = 0, x = 1; i < Point.Count; i++, x++)
        {
            if (i < 3 + (width - 1))
            {
                tempPoint.Add(GetLenPoint[0]);
            }
            else if (i >= (3 + (width - 1) * 2) * (GetLenPoint.Count - 1))
            {
                tempPoint.Add(GetLenPoint[GetLenPoint.Count - 1]);
            }
            else
            {
                tempPoint.Add(Point[i]);
            }
        }
        GetUpdatePoint.AddRange(tempPoint);
        if (Input.GetMouseButtonUp(0)) GetLenPoint.Clear();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < GetUpdatePoint.Count; i++)
        {
            Gizmos.DrawSphere(GetUpdatePoint[i], 0.1f);
        }
    }

}
