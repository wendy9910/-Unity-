using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = HairDrawer.PointPos;
    public List<Vector3> GetUpdatePointPos = HairDrawer.UpdatePointPos;
    List<Vector3> TempPoint = new List<Vector3>();

    //float GetWidthLimit = HairDrawer.WidthLimit;

    public void GeneratePosition(Vector3 oldPos,Vector3 newPos,int width,float GetWidthLimit)
    {

        Debug.Log(GetWidthLimit);
        for (int i = 0; i < 3 + (width - 1) * 2; i++) GetPointPos.Add(oldPos);
        Vector3 Vec0 = newPos - oldPos;
        /*for (int i = 0, j = width; i < width; i++, j--)
        {
            float n = j * GetWidthLimit;
            Vector3 Vec1 = new Vector3((Vec0.y) * n, (-Vec0.x) * n, Vec0.z * n);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(oldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            float n = j * GetWidthLimit;
            Vector3 Vec1 = new Vector3((-Vec0.y) * n, (Vec0.x) * n, Vec0.z * n);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);0
            GetPointPos.Add(temp);
        }*/
        if (HairDrawer.HairStyleState == 1) StraightHairStyle(width,Vec0,GetWidthLimit);
        if (HairDrawer.HairStyleState == 2) DiamodHairStyle(width,Vec0,GetWidthLimit);

    }
    Vector3 Vec = new Vector3();
    public void StraightHairStyle(int width, Vector3 Vec0,float GetWidthLimit) 
    {
        TempPoint.Clear();
        float w = 0.2f;
        float PointLen = GetPointPos.Count/(3+(width-1)*2);
 
        Debug.Log("StraightHairStyle");


        float dist = 0;
        for (int k = 0, x = 0; k < PointLen; k++) {

            if (k != 0) Vec = GetPointPos[x + (3+(width-1)*2)/2] - GetPointPos[x + (3 + (width - 1) * 2) / 2 - (3 + (width - 1) * 2)];
            else Vec = Vec0;
            if (k == 0)
            {
                for (int i = 0; i < 3 + (width - 1) * 2; i++)
                {
                    TempPoint.Add(GetPointPos[(3+(width-1)*2)/2]);
                    x++;
                }
            }
            else
            {
                Debug.Log("dist"+dist);
                for (int i = 0, j = width; i < width; i++, j--)
                {
                    float n = j * 0.05f * w + GetWidthLimit ;
                    Debug.Log(n);
                    Vector3 Vec1 = new Vector3((Vec.y) * j * n, (-Vec.x) * j * n, Vec.z * j);
                    Vector3 temp = new Vector3(GetPointPos[x].x + Vec1.x, GetPointPos[x].y + Vec1.y, GetPointPos[x].z + Vec1.z);
                    TempPoint.Add(temp);
                    x++;
                }
                TempPoint.Add(GetPointPos[x]);
                x++;
                for (int i = 0, j = 1; i < width; i++, j++)
                {
                    float n = j * 0.05f * w + GetWidthLimit;
                    Vector3 Vec1 = new Vector3((-Vec.y) * j * n, (+Vec.x) * j * n, Vec.z * j);
                    Vector3 temp = new Vector3(GetPointPos[x].x + Vec1.x, GetPointPos[x].y + Vec1.y, GetPointPos[x].z + Vec1.z);
                    TempPoint.Add(temp);
                    x++;
                }
                dist = Vector3.Distance(TempPoint[3+(width-1)*2], TempPoint[3 + (width - 1) * 2 + width]);
            }
            if (w < dist) w += 0.05f;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
       
    }

    public void DiamodHairStyle(int width, Vector3 Vec0, float GetWidthLimit) 
    {
        TempPoint.Clear();
        float w = 0.2f;
        float PointLen = GetPointPos.Count / (3 + (width - 1) * 2);

        Debug.Log("DimandtHairStyle");

        float Limit = GetWidthLimit;

        float dist = 0;
        for (int k = 0, x = 0; k < PointLen; k++)
        {

            if (k != 0) Vec = GetPointPos[x + (3 + (width - 1) * 2) / 2] - GetPointPos[x + (3 + (width - 1) * 2) / 2 - (3 + (width - 1) * 2)];
            else Vec = Vec0;
            if (k == 0)
            {
                for (int i = 0; i < 3 + (width - 1) * 2; i++)
                {
                    TempPoint.Add(GetPointPos[(3 + (width - 1) * 2) / 2]);
                    x++;
                }
            }
            else
            {
                if (k!=PointLen-1) {
                    for (int i = 0, j = width; i < width; i++, j--)
                    {
                        float n = 0;
                        if (k < PointLen / 2 + 2) n = j * Limit * w;
                        else n = j * Limit * w;
                        Vector3 Vec1 = new Vector3((Vec.y) * j * n, (-Vec.x) * j * n, Vec.z * j);
                        Vector3 temp = new Vector3(GetPointPos[x].x + Vec1.x, GetPointPos[x].y + Vec1.y, GetPointPos[x].z + Vec1.z);
                        TempPoint.Add(temp);
                        x++;
                    }
                    TempPoint.Add(GetPointPos[x]);
                    x++;
                    for (int i = 0, j = 1; i < width; i++, j++)
                    {
                        float n = 0;
                        if (k < PointLen / 2 + 2) n = j * Limit * w;
                        else n = j * Limit * w;
                        Vector3 Vec1 = new Vector3((-Vec.y) * j * n, (+Vec.x) * j * n, Vec.z * j);
                        Vector3 temp = new Vector3(GetPointPos[x].x + Vec1.x, GetPointPos[x].y + Vec1.y, GetPointPos[x].z + Vec1.z);
                        TempPoint.Add(temp);
                        x++;
                    }
                    dist = Vector3.Distance(TempPoint[3 + (width - 1) * 2], TempPoint[3 + (width - 1) * 2 + width]);

                    if (width * w * Limit  < dist && k < PointLen / 2) w += 0.05f;
                    if (k > PointLen / 2 + 2 && w > 0.01f && width * w * Limit > 0.05f)
                    {
                        w -= 0.1f;
                        Limit -= 0.1f;
                    }
                }
                else
                {
                    for (int i = 0; i < 3 + (width - 1) * 2; i++)
                    {
                        TempPoint.Add(GetPointPos[GetPointPos.Count-1-(3+(width-1)*2)/2]);
                        x++;
                    }
                }
            }
            
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
