using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetUpdatePointPos = HairDrawer.UpdatePointPos;
    public List<Vector3> GetPointPos = HairDrawer.PointPos;
    List<Vector3> TempPoint = new List<Vector3>();
    int width = HairDrawer.HairWidth;
    Vector3 cross1, cross2;
    
    //float w = 0;

    public void PosGenerate(Vector3 OldPos, Vector3 NewPos, int range)
    {

        Vector3 Vec = NewPos - OldPos;
        float n = 1.2f * 0.1f * range;//每段長度
        Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, Vec.z * n);
        Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
        GetPointPos.Add(temp);
        Vec1 = new Vector3(Vec.z * n , (-Vec.y) * n, (-Vec.x) * n);
        temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
        GetPointPos.Add(temp);
        Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, Vec.z * n);
        temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
        GetPointPos.Add(temp);
        Vec1 = new Vector3((-Vec.z) * n, (Vec.y) * n, Vec.x * n);
        temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
        GetPointPos.Add(temp);
        
    }
    public void VectorCross(Vector3 up, Vector3 forward, Vector3 right)
    {
        cross1 = Vector3.Cross(up, forward);//x
        cross1.Normalize();
        cross2 = Vector3.Cross(up, right);//z
        cross2.Normalize();
    }

    public void StraightHairtyle(List<Vector3> GetPointPos, int range,int thickness) 
    {
        float w1;
        float w = range * 0.005f * 0.2f;
        float t = thickness * 0.002f;
        if (GetPointPos.Count <= 6) w1 = (range * 0.005f) / GetPointPos.Count;
        else w1 = (range * 0.005f) / range;

        TempPoint.Clear();
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            if (i == 0)
            {
                TempPoint.Add(GetPointPos[i] - cross1 * w);
                TempPoint.Add(GetPointPos[i] + cross2 * t);
                TempPoint.Add(GetPointPos[i] + cross1 * w);
                TempPoint.Add(GetPointPos[i] - cross2 * t);
            }
            else
            {
                TempPoint.Add(GetPointPos[i] - cross1 * w);
                TempPoint.Add(GetPointPos[i] + cross2 * t);
                TempPoint.Add(GetPointPos[i] + cross1 * w);
                TempPoint.Add(GetPointPos[i] - cross2 * t);
            }
            if (w < range * 0.005f) w += w1;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void DimandHiarStyle(List<Vector3> GetPointPos, int range ,int thickness)
    {

        float w1 = range * 0.005f / (GetPointPos.Count / 2);
        float w = range * 0.005f * 0.2f;
        float t = thickness * 0.002f;

        TempPoint.Clear();
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            if (i == GetPointPos.Count - 1 && GetPointPos.Count > 2)
            {
                for (int j = 0; j < 4; j++) TempPoint.Add(GetPointPos[i]);
            }
            else
            {
                TempPoint.Add(GetPointPos[i] - cross1 * w);
                TempPoint.Add(GetPointPos[i] + cross2 * t);
                TempPoint.Add(GetPointPos[i] + cross1 * w);
                TempPoint.Add(GetPointPos[i] - cross2 * t);
            }
            if (w < range * 0.005f && i < GetPointPos.Count / 2) w += w1;
            else if (i > GetPointPos.Count / 2) w -= w1;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);

    }

    public void WaveHairStyle(List<Vector3> GetPointPos, int range, int thickness) 
    {
        TempPoint.Clear();
        float w1 = range * 0.005f / (GetPointPos.Count / 2);
        float w = range * 0.005f * 0.2f;
        float t = thickness * 0.002f;
        float waveSize = 0.001f;
        
        float angle = -Mathf.PI;

        for (int i = 0; i < GetPointPos.Count; i++)
        {
            float y = Mathf.Sin(angle);
            if (i == 0)
            {
                Vector3 temp = new Vector3(GetPointPos[i].x, GetPointPos[i].y, GetPointPos[i].z + waveSize * y);
                for (int j = 0; j < 4; j++) TempPoint.Add(temp);
            }
            else
            {
                Vector3 temp = new Vector3(GetPointPos[i].x, GetPointPos[i].y, GetPointPos[i].z + waveSize * y);
                TempPoint.Add(temp - cross1 * w);
                TempPoint.Add(temp + cross2 * t);
                TempPoint.Add(temp + cross1 * w);
                TempPoint.Add(temp - cross2 * t);
                
            }
            //if (w < range * 0.005f) w += w1;
            if (w < range * 0.005f && i < GetPointPos.Count / 2) w += w1;
            else if (i > GetPointPos.Count / 2) w -= w1;
            if (waveSize < 0.03f && i%7==0) waveSize += 0.01f;
            //if (i > GetPointPos.Count - 5) waveSize = 0.01f;
            angle += 0.9f;
        }

        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            Gizmos.DrawSphere(GetPointPos[i], 0.005f);
        }
    }
}
