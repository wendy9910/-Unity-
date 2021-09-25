using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{

    public MeshFilter TargetMesh;               //網格
    public Transform BallGroup;                 //球體集合(關節上所有球放這)
    private List<Transform> ListBall;         //存放所有球體
    private List<MeshData> ListMeshData;      //節點資料
    GameObject HairMesh, Ball;

    public void Set(int BallCount)
    {
        HairMesh = GameObject.Find("HairModel/HairGroup/FreeHair" + BallCount);
        TargetMesh = HairMesh.GetComponent<MeshFilter>();
        Ball = GameObject.Find("HairModel/HairGroup/BallGroup" + BallCount);
        BallGroup = Ball.transform;
        ListBall = new List<Transform>();
        foreach (Transform tran in BallGroup)
        {
            ListBall.Add(tran);
        }


        ListMeshData = new List<MeshData>();
        int totleMeshPoint = TargetMesh.mesh.vertices.Length;
        for (int i = 0; i < totleMeshPoint; i++)
        {
            MeshData data = new MeshData();

            data.Index = i;
            data.target = __FindNearest(TargetMesh.mesh.vertices[i]);
            if (data.target == null) Debug.Log("有空的");
            data.offset = TargetMesh.mesh.vertices[i] - data.target.localPosition;
            ListMeshData.Add(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveMeshPoint();
    }

    private void MoveMeshPoint()
    {
        Vector3[] v3 = TargetMesh.mesh.vertices;
        for (int i = 0; i < ListMeshData.Count; i++)
        {
            MeshData curData = ListMeshData[i];
            Vector3 dir = curData.target.transform.TransformDirection(curData.offset);
            v3[i] = curData.target.localPosition + dir;
        }
        TargetMesh.mesh.vertices = v3;
    }



    private Transform __FindNearest(Vector3 v3)//
    {
        if (ListBall != null)
        {
            float MaxDis = 999999;
            Transform MaxTran = null;
            for (int i = 0; i < ListBall.Count; i++)
            {
                float curDis = Vector3.Distance(ListBall[i].localPosition, v3);
                if (curDis < MaxDis)
                {
                    MaxDis = curDis;
                    MaxTran = ListBall[i];
                }
            }
            return MaxTran;
        }
        return null;
    }

}

public class MeshData
{
    public int Index;               //索引
    public Transform target;        //目標球球
    public Vector3 offset;          //與目標球球位置差距
}
