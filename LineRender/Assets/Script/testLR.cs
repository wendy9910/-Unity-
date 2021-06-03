using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLR : MonoBehaviour
{
    private LineRenderer lr;
    Color c1 = new Color(0, 1, 1, 1);
    Color c2 = new Color(0, 1, 0, 1);

    void Start()
    {

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();


        // Create a material with transparent diffuse shader
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = Color.green;

        // assign the material to the renderer
        GetComponent<Renderer>().material = material;

        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Transparent/Diffuse"));
       
        // Set some positions

        int size = 5;
        Vector3[] positions = new Vector3[(size+1)*(size+1)];
        
        for (int y=0,n=0;y <size;y++,n++) {
            for (int x = 0; x < size; x++,n++)
            {
                positions[n] = new Vector3(x, y, 0);
            }
        }
        /*
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        positions[2] = new Vector3(2.0f, -2.0f, 0.0f);
        */

        lineRenderer.SetPositions(positions);
   
    }

}
