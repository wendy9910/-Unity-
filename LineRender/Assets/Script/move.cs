using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 playerInput;
    float UD = 0f;
    float LR = 0f;
    float FB = 0f;

    private Vector3 paintPos;
    private int index = 0;
    private int LengthOfPos = 0;

    private LineRenderer drawer;

    void Start()
    {
        //LineRenderer drawer = gameObject.AddComponent<LineRenderer>();
        drawer = gameObject.AddComponent<LineRenderer>();
        drawer.material = new Material(Shader.Find("Sprites/Default"));
        drawer.SetColors(Color.blue, Color.green);
        drawer.SetWidth(0.1f, 0.1f);
        drawer.numCapVertices = 2;
        drawer.numCornerVertices = 2;

    }

    // Update is called once per frame
    void Update()
    {
        moveControl();
        if (Input.anyKeyDown) {
            paintPos = transform.localPosition;
            LengthOfPos++;
            drawer.SetVertexCount(LengthOfPos);
        }
        while (index < LengthOfPos)
        {

            drawer.SetPosition(index, paintPos);
            index++;
        }
    }

    private void moveControl()
    {

   
        if (Input.GetKeyDown(KeyCode.W)) FB = 1f;
        else if (Input.GetKeyDown(KeyCode.A)) LR = -1f;
        else if (Input.GetKeyDown(KeyCode.D)) LR = 1f;
        else if (Input.GetKeyDown(KeyCode.S)) FB = -1f;
        else if (Input.GetKeyDown(KeyCode.UpArrow)) UD = 1f;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) UD = -1f;
        else FB = LR = UD = 0;
        
        playerInput.x += LR;
        playerInput.z += FB;
        playerInput.y += UD;

        transform.localPosition = new Vector3(playerInput.x, playerInput.y, playerInput.z);
        //Debug.Log(transform.localPosition);

        /*if (Input.anyKeyDown)
        {
            key = 1;
            points += 1;
        }
        else key = 0;

        if (key==1) {
            Vector3[] positions = new Vector3[1000];
            positions[points] = new Vector3(playerInput.x, playerInput.y, playerInput.z);
            drawer.positionCount = positions.Length;
            drawer.SetPositions(positions);
        }

        Debug.Log(points);
        // drawline(playerInput,points);
        //Vector3 displacement = new Vector3(playerInput.x, playerInput.y, playerInput.z);
        //transform.localPosition += displacement; */

    }
}
