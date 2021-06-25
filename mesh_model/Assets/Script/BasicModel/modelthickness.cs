using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelthickness : MonoBehaviour
{
    public List<Vector3> PointPos = new List<Vector3>();
    int xSize = 5;
    int ySize = 8;
    int thickness = 2;

    // Start is called before the first frame update
    void Start()
    {
        PositionGenerate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PositionGenerate() 
    {
       
            for (int x = 0; x <= xSize; x++)
            {
                for (int y = 0; y <= ySize; y++)
                {

                    Vector3 newPos = new Vector3(x, y, 0);
                    PointPos.Add(newPos);

                }
                for (int y = ySize; y >= 0; y--)
                {

                    Vector3 newPos = new Vector3(x, y, 1);
                    PointPos.Add(newPos);

                }
            }
        



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
    }
}
