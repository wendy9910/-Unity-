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
        }
        
    }
    public void StraightHairtyle(List<Vector3> GetPointPos, int range) 
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
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
                TempPoint.Add(GetPointPos[i]);
                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

            }
            else
            {
                Vector3 Vec = GetPointPos[i] - GetPointPos[i - 1];
                //Vector3 Vec = GetPointPos[i - 1] - GetPointPos[i];
                float n = w2 * 0.1f * range;//每段長度
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
                TempPoint.Add(GetPointPos[i]);
                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
            }
            if (w2 < 1.2f) w2 += w1;
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void DimandHiarStyle(List<Vector3> GetPointPos, int range)
    {
        Debug.Log("DimandHiarStyle");
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
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
                TempPoint.Add(GetPointPos[i]);
                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);

            }
            else if (i == GetPointPos.Count - 1 && GetPointPos.Count > 2)
            {
                for (int j = 0; j < 3; j++) TempPoint.Add(GetPointPos[i]);
            }
            else
            {
                Vector3 Vec = GetPointPos[i] - GetPointPos[i - 1];
                //Vector3 Vec = GetPointPos[i - 1] - GetPointPos[i];
                float n = w2 * 0.1f * range;//每段長度
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
                TempPoint.Add(GetPointPos[i]);
                Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                temp = new Vector3(GetPointPos[i].x + Vec1.x, GetPointPos[i].y + Vec1.y, GetPointPos[i].z + Vec1.z);
                TempPoint.Add(temp);
            }
            if (w2 < 1.2f) w2 += w1;
            else if (i > GetPointPos.Count / 2 + 2 ) w2 -= w1;
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

    /*
     float w = (float)width;
        if (GetPointPos == null)
        {
            Vector3 Vec = OldPos - NewPos;
            for (int i = 0, j = width; i < width; i++, j--)
            {
                float n = (j / w) * 1.8f * 0.1f * range;//每段長度
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
                GetPointPos.Add(temp);
            }
            GetPointPos.Add(OldPos);
            for (int i = 0, j = 1; i < width; i++, j++)
            {
                float n = (j / w) * 1.8f * 0.1f * range;
                Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
                GetPointPos.Add(temp);
            }

        }
        else 
        {
            Vector3 Vec = NewPos - OldPos;
            for (int i = 0, j = width; i < width; i++, j--)
            {
                float n = (j / w) * 1.8f * 0.1f * range;
                Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
                Vector3 temp = new Vector3(NewPos.x + Vec1.x, NewPos.y + Vec1.y, NewPos.z + Vec1.z);
                GetPointPos.Add(temp);
            }
            GetPointPos.Add(NewPos);
            for (int i = 0, j = 1; i < width; i++, j++)
            {
                float n = (j / w) * 1.8f * 0.1f * range;
                Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
                Vector3 temp = new Vector3(NewPos.x + Vec1.x, NewPos.y + Vec1.y, NewPos.z + Vec1.z);
                GetPointPos.Add(temp);
            }
        }
    */
}
