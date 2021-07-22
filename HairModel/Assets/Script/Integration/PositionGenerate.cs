using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = HairDrawer.PointPos;
    public List<Vector3> GetUpdatePointPos = HairDrawer.UpdatePointPos;
    List<Vector3> TempPoint = new List<Vector3>();

    float GetWidthLimit = HairDrawer.WidthLimit;

    public void GeneratePosition(Vector3 oldPos,Vector3 newPos,int width)
    {

        Debug.Log(GetWidthLimit);
        Vector3 Vec = newPos - oldPos;//兩點移動方向向量
        for (int i = 0; i < 3 + (width - 1) * 2; i++) GetPointPos.Add(Vec);

        /*
        Vector3 Vec = newPos - oldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            float n = j * GetWidthLimit;
            Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, Vec.z * n);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(oldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            float n = j * GetWidthLimit;
            Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, Vec.z * n);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }*/
        if (HairDrawer.HairStyleState == 1) StraightHairStyle(width,Vec);

    }

    public void StraightHairStyle(int width, Vector3 Vec0) 
    {
        TempPoint.Clear();
        float w = 0.02f;
        float PointLen = GetPointPos.Count/(3+(width-1)*2);
 
        Debug.Log("StraightHairStyle");

        float dist = 0;

        for (int k = 0, x = 0; k < PointLen; k++) {
            if (k == 0)
            {
                dist = Vector3.Distance(GetPointPos[0], GetPointPos[(3 + (width - 1) * 2)/2]);
                for (int i = 0; i < 3 + (width - 1) * 2; i++)
                {
                    TempPoint.Add(GetPointPos[(3+(width-1)*2)/2]);
                    x++;
                }
                Debug.Log(x);
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    TempPoint.Add(new Vector3(GetPointPos[x].x * w, GetPointPos[x].y * w, GetPointPos[x].z));
                    x++;
                }
                Debug.Log(x);
                TempPoint.Add(GetPointPos[x]);
                x++;
                for (int i = 0; i < width; i++)
                {
                    TempPoint.Add(new Vector3(GetPointPos[x].x * w, GetPointPos[x].y * w, GetPointPos[x].z));
                    x++;
                }
                Debug.Log(x);
            }
            if (w < dist) w += 0.01f;
            
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
       

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < GetUpdatePointPos.Count; i++)
        {
            Gizmos.DrawSphere(GetUpdatePointPos[i], 0.1f);
        }
    }
}
