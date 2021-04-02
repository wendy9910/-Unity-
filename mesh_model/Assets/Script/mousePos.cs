using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePos : MonoBehaviour
{
    // Start is called before the first frame update

    
    GameObject sphere;

    void Start()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        //camera: (0,1,-10) sphere: (0,0,0) 
        Vector3 mousePos = Input.mousePosition;

        float x0 = (mousePos.x - Screen.width / 2)/(Screen.width / 2);
        float y0 = (mousePos.y - Screen.height / 2) / (Screen.height / 2); 

        float y = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * y0;// 求tan(30);
        float x = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * x0 * Screen.width/Screen.height; //加上 Screen.width/Screen.height 控制螢幕寬變動

        sphere.transform.position = new Vector3(x,y,0); 
    }
}
