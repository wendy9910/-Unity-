using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetUpdatePointPos = HairDrawer.UpdatePointPos;
    public List<Vector3> GetPointPos = HairDrawer.PointPos;
    List<Vector3> TempPoint = new List<Vector3>();
    int width = HairDrawer.HairWidth;
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
        /*
        if (GetPointPos == null)
        {
            Vector3 Vec = OldPos - NewPos;
            float n = 1.2f * 0.1f * range;//每段長度
            Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
            GetPointPos.Add(OldPos);
            Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
            temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);

        }
        else 
        {
            Vector3 Vec = NewPos - OldPos;
            float n = 1.2f * 0.1f * range;//每段長度
            Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
            Vector3 temp = new Vector3(NewPos.x + Vec1.x, NewPos.y + Vec1.y, NewPos.z + Vec1.z);
            GetPointPos.Add(temp);
            GetPointPos.Add(NewPos);
            Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
            temp = new Vector3(NewPos.x + Vec1.x, NewPos.y + Vec1.y, NewPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }*/

    }
    public void StraightHairtyle(List<Vector3> GetPointPos, int range,int thickness) 
    {
        float w1;
        float w2 = 0.5f;
        if (GetPointPos.Count <= 6) w1 = 1.2f / GetPointPos.Count;
        else w1 = 1.2f / range;


        TempPoint.Clear();
        for (int i = 0; i < GetPointPos.Count; i++) 
        {
            if (i == 0)
            {
                Vector3 Vec = GetPointPos[i + 1] - GetPointPos[i];
                float n = w2 * 0.1f * range;//每段長度
                float t = 1.2f * 0.1f * thickness;

                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3(Vec.z * t, (-Vec.y) * t, (-Vec.x) * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.z) * t, (Vec.y) * t, Vec.x * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

            }
            else
            {
                Vector3 Vec = GetPointPos[i] - GetPointPos[i - 1];
                //Vector3 Vec = GetPointPos[i - 1] - GetPointPos[i];
                float n = w2 * 0.1f * range;//每段長度
                float t = 1.2f * 0.1f * thickness;

                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3(Vec.z * t, (-Vec.y) * t, (-Vec.x) * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.z) * t, (Vec.y) * t, Vec.x * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
            }
            if (w2 < 1.2f) w2 += w1;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void DimandHiarStyle(List<Vector3> GetPointPos, int range ,int thickness)
    {

        float w1 = 1.2f / (GetPointPos.Count / 2);
        float w2 = 0.5f;

        TempPoint.Clear();
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            if (i == 0)
            {
                Vector3 Vec = GetPointPos[i + 1] - GetPointPos[i];
                float n = w2 * 0.1f * range;//每段長度
                float t = 1.2f * 0.1f * thickness;

                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3(Vec.z * t, (-Vec.y) * t, (-Vec.x) * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.z) * t, (Vec.y) * t, Vec.x * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
            }
            else if (i == GetPointPos.Count - 1 && GetPointPos.Count > 2)
            {
                for (int j = 0; j < 4; j++) TempPoint.Add(GetPointPos[i]);
            }
            else
            {
                Vector3 Vec = GetPointPos[i] - GetPointPos[i - 1];
                float n = w2 * 0.1f * range;//每段長度
                float t = 1.2f * 0.1f * thickness;

                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3(Vec.z * t, (-Vec.y) * t, (-Vec.x) * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

                Vec1 = new Vector3((-Vec.z) * t, (Vec.y) * t, Vec.x * t);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
            }
            if (w2 < 1.2f && i < GetPointPos.Count/2) w2 += w1;
            else if (i > GetPointPos.Count / 2) w2 -= w1;
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
