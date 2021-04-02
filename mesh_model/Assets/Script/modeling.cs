using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling : MonoBehaviour
{
    //如何讓Cube跟著滑鼠走
    //能知道2D滑鼠座標,左下角(0,0)，右上角(width,height)
    // Start is called before the first frame update

    Vector3 mouse;
    GameObject sphere;
    void Start()
    {
        
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        float newX = (mouse.x - Screen.width / 2) / (Screen.width / 2);
        float newY = (mouse.y - Screen.height / 2) / (Screen.height / 2);
        Vector3 X = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
        float x = newX * 5.77f / X.x / X.y;
        sphere.transform.position = new Vector3(x, newY * 5.77f, 0);

    }
}
