using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetUpdatePointPos = HairDrawer.UpdatePointPos;
    public List<Vector3> GetPointPos = HairDrawer.PointPos;
    List<Vector3> TempPoint = new List<Vector3>();
    int width = HairDrawer.HairWidth;
    float w = 0;

    public void PosGenerate(Vector3 OldPos, Vector3 NewPos, int range)
    {
        Vector3 Vec = NewPos - OldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            float n = (j / 3.0f) * 0.7f * 0.1f * range;
            Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) * n);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(OldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            float n = (j / 3.0f) * 0.7f * 0.1f * range;
            Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z) * n);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }

    }
    public void StraightHairtyle(List<Vector3> GetPointPos, float GetWidthLimit, int add) 
    {
        TempPoint.Clear();
        //w = (0.015f - add * 0.0001f) * add;

        for (int k = 0, x = 0; k < GetPointPos.Count; k++)
        {
            if (k == 0)
            {
                for (int i = 0; i < 3 + (width - 1) * 2; i++)
                {
                    TempPoint.Add(GetPointPos[0]);
                    x++;
                }
            }
            else
            {
                Vector3 Vec = GetPointPos[k]- GetPointPos[k-1];
                for (int i = 0, j = width; i < width; i++, j--)
                {
                    float n = j * 0.1f; //* w + GetWidthLimit;
                    Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x)*n, (Vec.y) * n);
                    Vector3 temp = new Vector3(GetPointPos[k].x + Vec1.x, GetPointPos[k].y + Vec1.y, GetPointPos[k].z + Vec1.z);
                    TempPoint.Add(temp);
                    x++;
                }
                TempPoint.Add(GetPointPos[k]);
                x++;
                for (int i = 0, j = 1; i < width; i++, j++)
                {
                    float n = j * 0.1f;//* w + GetWidthLimit;
                    Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x)*n, (-Vec.y) * n);
                    //Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, Vec.z);
                    Vector3 temp = new Vector3(GetPointPos[k].x + Vec1.x, GetPointPos[k].y + Vec1.y, GetPointPos[k].z + Vec1.z);
                    TempPoint.Add(temp);
                    x++;
                }
                float dist  = Vector3.Distance(TempPoint[x - 1], TempPoint[x - width * 2 - 1]);
                //if (dist < GetWidthLimit * 2) w += w;
            }
        }
        GetUpdatePointPos.Clear();
        GetUpdatePointPos.AddRange(TempPoint);
    }

    public void DimandHiarStyle(List<Vector3> GetPointPos, float GetWidthLimit, int add)
    { 
    
    
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
