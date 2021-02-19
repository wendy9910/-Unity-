using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLine : MonoBehaviour
{
    private LineRenderer drawer;
    private Vector3 mousePos;
    private int index = 0;
    private int LengthmousePos = 0;
    // Start is called before the first frame update
    void Start()
    {
        drawer = gameObject.AddComponent<LineRenderer>();
        drawer.material = new Material(Shader.Find("Sprites/Default"));
        drawer.SetColors(Color.blue,Color.green);
        drawer.SetWidth(0.1f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        drawer = GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0)) {
            
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            LengthmousePos++;
            drawer.SetVertexCount(LengthmousePos);
        }
        if (Input.GetMouseButtonUp(0)){
            drawer.Clear();

        }
        while (index < LengthmousePos)
        {
            //两点确定一条直线，所以我们依次绘制点就可以形成线段了
            drawer.SetPosition(index, mousePos);
            index++;
        }

    }

}
