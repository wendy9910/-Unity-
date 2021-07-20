using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = CreateHair.PointPos;
    float thickness = 0.5f;
    public void GetPosition(Vector3 oldPos,Vector3 newPos,int width) 
    {
        Vector3 Vec = newPos - oldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(oldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }

        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z + thickness);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(new Vector3(oldPos.x, oldPos.y, oldPos.z + thickness));

        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z + thickness);
            GetPointPos.Add(temp);
        }


    }
}
