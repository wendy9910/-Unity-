using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = CreateHair.PointPos;
    public void GetPosition(Vector3 OldPos,Vector3 NewPos,int width) 
    {
        Vector3 Vec = NewPos - OldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(OldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }


    }
}
