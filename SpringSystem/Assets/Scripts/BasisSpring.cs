using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasisSpring : MonoBehaviour
{
    public Vector3[] SpherePos = new Vector3[2];
    public GameObject[] Sphere = new GameObject[2];
    private LineRenderer player;
    public Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        //Set Sphere
        SpherePos[0] = new Vector3(100, 0, 0);
        SpherePos[1] = new Vector3(-100, 0, 0);

        Sphere[0] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere[1] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere[0].transform.localScale = new Vector3(20f, 20f, 20f);
        Sphere[0].transform.position = SpherePos[0];
        Sphere[1].transform.localScale = new Vector3(20f, 20f, 20f);
        Sphere[1].transform.position = SpherePos[1];

        SetRenderer();

        

    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponent<LineRenderer>();
        player.positionCount = SpherePos.Length;
        player.SetPositions(SpherePos);
        Sphere[0].transform.position = SpherePos[0];
        Sphere[1].transform.position = SpherePos[1];

        spring();
    }

    void SetRenderer() 
    {
        //LineRenderer
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.gray, Color.gray);
        player.SetWidth(3f, 3f);
        
    }

    void spring() 
    { 
        float dist = Vector3.Distance(SpherePos[0],SpherePos[1]);
        v = SpherePos[0] - SpherePos[1];
        v.Normalize();
        v = v * ((dist-100)/100);
        SpherePos[1] += v;
        SpherePos[0] -= v;

    }
}
