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

    public void PosGenerate(Vector3 pos1, Vector3 pos2,int width,List<Vector3> PointPos,int GetSelect,float widthAdj)//計算點座標 (1)主線段點(2)右左兩個延伸點座標計算
    {
        //右左兩個延伸點座標矩陣
        thickness1 = new Vector3[width];
        thickness2 = new Vector3[width];

        //算兩點向量差
        Vector3 Vec0 = pos2 - pos1;//兩點移動方向向量
        for (int i = 0, j = thickness1.Length; i < thickness1.Length; i++, j--)//widthAdd1
        {
            thickness1[i] = pos1;
            PointPos.Add(thickness1[i]);
        }
        PointPos.Add(pos1);
        GetLenPoint.Add(pos1);
        for (int i = 0, j = 1; i < thickness2.Length; i++, j++)//widthAdd
        {
            thickness2[i] = pos1;
            PointPos.Add(thickness2[i]);
        }
        if(GetLenPoint.Count > 2 && GetSelect == 0)straightStyle(PointPos,width,widthAdj,Vec0);
        if (GetLenPoint.Count > 2 && GetSelect == 1) diamandStyle(PointPos, width,widthAdj,Vec0);
        
    }

    Vector3 Vec = new Vector3();
    public void straightStyle(List<Vector3> Point, int width,float widthAdj,Vector3 Vec00)
    {
        tempPoint.Clear();
        
        float w = 0.2f;
        int x = 0;
        for (int i = 0; i < GetLenPoint.Count; i++)
        {
            if (i != 0) Vec = GetLenPoint[i] - GetLenPoint[i - 1];//兩點移動方向向量  new - old
            else Vec = Vec00;
            for (int k = 0, j = thickness1.Length; k < thickness1.Length; k++, j--)
            {
                Vector3 Vec1 = new Vector3((Vec.y) * j * w, (-Vec.x) * j * w, Vec.z * j);
                Vector3 temp = new Vector3(Point[x].x + Vec1.x, Point[x].y + Vec1.y, Point[x].z + Vec1.z);
                tempPoint.Add(temp);
                x++;
            }
            tempPoint.Add(GetLenPoint[i]);
            x++;
            for (int k = 0, j = 1; k < thickness2.Length; k++, j++)
            {
                Vector3 Vec1 = new Vector3((-Vec.y) * j * w, (+Vec.x) * j * w, Vec.z * j);
                Vector3 temp = new Vector3(Point[x].x + Vec1.x, Point[x].y + Vec1.y, Point[x].z + Vec1.z);
                tempPoint.Add(temp);
                x++;
            }
            if (w < width) w += widthAdj;

        }
        GetUpdatePoint.Clear();
        GetUpdatePoint.AddRange(tempPoint);
    }
    
    public void diamandStyle(List<Vector3> Point, int width,float widthAdj,Vector3 Vec00)
    {
        tempPoint.Clear();
        GetUpdatePoint.Clear();

        float w = 0.1f;
        int x = 0;
        for (int i = 0; i < GetLenPoint.Count; i++)
        {
            if (i != 0) Vec = GetLenPoint[i] - GetLenPoint[i - 1];//兩點移動方向向量  new - old
            else Vec = Vec00;
            if (i == GetLenPoint.Count - 1)
            {
                for (int ii = 0; ii < thickness1.Length + thickness2.Length + 1; ii++)
                {
                    tempPoint.Add(GetLenPoint[i]);
                    x++;
                }
                w = widthAdj;

            }
            else
            {
                for (int k = 0, j = thickness1.Length; k < thickness1.Length; k++, j--)
                {
                    Vector3 Vec1 = new Vector3((Vec.y) * j * w, (-Vec.x) * j * w, Vec.z * j);
                    Vector3 temp = new Vector3(Point[x].x + Vec1.x, Point[x].y + Vec1.y, Point[x].z + Vec1.z);
                    tempPoint.Add(temp);
                    x++;
                }
                tempPoint.Add(GetLenPoint[i]);
                x++;
                for (int k = 0, j = 1; k < thickness2.Length; k++, j++)
                {
                    Vector3 Vec1 = new Vector3((-Vec.y) * j * w, (+Vec.x) * j * w, Vec.z * j);
                    Vector3 temp = new Vector3(Point[x].x + Vec1.x, Point[x].y + Vec1.y, Point[x].z + Vec1.z);
                    tempPoint.Add(temp);
                    x++;
                }
            }
            if (w < width && i < GetLenPoint.Count / 2 -1) w += widthAdj;
            else if (i >= GetLenPoint.Count / 2) w -= widthAdj;

        }
        
        GetUpdatePoint.AddRange(tempPoint);

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
