using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = CreateHair.PointPos;
    float thickness = 0f;
    public void GetPosition(Vector3 oldPos,Vector3 newPos,int width) 
    {
        Vector3 Vec = newPos - oldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((Vec.y) * j, (-Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3(Vec.z * j * 0.5f, (-Vec.y) * j * 0.5f, (-Vec.x) * j * 0.5f);
            //Vector3 Vec1 = new Vector3((Vec.x) * j, Vec.z * j, (-Vec.y) * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            
            Vector3 Vec1 = new Vector3((-Vec.y) * j, (Vec.x) * j, Vec.z * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z );
            GetPointPos.Add(temp);
        }
        for (int i = 0, j = width; i < width; i++, j--)
        {
            Vector3 Vec1 = new Vector3((-Vec.z) * j, (Vec.y) * j, Vec.x * j);
            //Vector3 Vec1 = new Vector3((Vec.x) * j, (-Vec.z) * j, Vec.y * j);
            Vector3 temp = new Vector3(oldPos.x + Vec1.x, oldPos.y + Vec1.y, oldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            Gizmos.DrawSphere(GetPointPos[i], 0.1f);
        }
    }
}
