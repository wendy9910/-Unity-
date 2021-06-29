using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kdtree : MonoBehaviour
{

    Vector3[] pointCloud = new Vector3[10000];

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < pointCloud.Length; i++) pointCloud[i] = Random.insideUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
