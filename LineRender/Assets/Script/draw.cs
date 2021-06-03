using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 playerInput, lastpos;
    float UD = 0f;
    float LR = 0f;
    float FB = 0f;


    private LineRenderer drawer;
    private List<Vector3> paintPos = new List<Vector3>();
    private bool P;


    void Start()
    {
        //LineRenderer drawer = gameObject.AddComponent<LineRenderer>();
        drawer = GetComponent<LineRenderer>();
        drawer.material = new Material(Shader.Find("Sprites/Default"));
        drawer.SetWidth(0.1f, 0.1f);
        drawer.SetColors(Color.black, Color.yellow);
        drawer.numCapVertices = 2;
        drawer.numCornerVertices = 2;


    }

    // Update is called once per frame
    void Update()
    {
        moveControl();

    }
    private void moveControl()
    {
        if (Input.GetKey(KeyCode.W)) FB = 0.1f;
        else if (Input.GetKey(KeyCode.A)) LR = -0.1f;
        else if (Input.GetKey(KeyCode.D)) LR = 0.1f;
        else if (Input.GetKey(KeyCode.S)) FB = -0.1f;
        else if (Input.GetKey(KeyCode.UpArrow)) UD = 0.1f;
        else if (Input.GetKey(KeyCode.DownArrow)) UD = -0.1f;
        else FB = LR = UD = 0;

        playerInput.x += LR;
        playerInput.z += FB;
        playerInput.y += UD;

        transform.localPosition = new Vector3(playerInput.x, playerInput.y, playerInput.z);


        if (Input.anyKey)
        {

            lastpos = transform.localPosition;
            paintPos.Add(lastpos);
            Debug.Log(lastpos);
            drawer.positionCount = paintPos.Count;
            drawer.SetPositions(paintPos.ToArray());
            float dist = Vector3.Distance(lastpos, transform.localPosition);

            if (dist >= 0.1f)
            { 
                paintPos.Add(transform.localPosition);
                lastpos = transform.localPosition;
            }
            drawer.positionCount = paintPos.Count;//paintPos();
            drawer.SetPositions(paintPos.ToArray());
            
        }
        else
        {
 
            paintPos.Clear();
            drawer = null;
        }


    }

    private void Go(){

       

    }


        // drawline(playerInput,points);
        //Vector3 displacement = new Vector3(playerInput.x, playerInput.y, playerInput.z);
        //transform.localPosition += displacement; 


}
