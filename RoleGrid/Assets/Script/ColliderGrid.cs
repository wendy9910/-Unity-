using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGrid : MonoBehaviour
{

    void OnTriggerEnter(Collider other) //aaa為自定義碰撞事件
    {
        if (other.gameObject.tag == "Grid") //如果aaa碰撞事件的物件標籤名稱是test
        {
            Debug.Log("ok");
        }
    }
}
