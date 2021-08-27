using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_head : MonoBehaviour
{
    int down = 0;
    Vector3 oldPos, newPos, objPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            oldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        }
        if (Input.GetMouseButtonUp(0)) 
        { 
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            objPos = newPos - oldPos;
            Debug.Log(objPos);
            Vector3 n = Vector3.Normalize(objPos);
            Debug.Log("N:" + n);

            n = n * 3f;
            
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            sphere.transform.position = newPos + n;

        }
        
    }
}
