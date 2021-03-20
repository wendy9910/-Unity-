using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint), typeof(Rigidbody), typeof(BoxCollider))]
public class Spring : MonoBehaviour
{
    public Vector3 p1= new Vector3(0.5f,0.0f,0.0f);
    public Vector3 p2 = new Vector3(1.0f,2.0f, 0.0f);
    public Vector3 p3 = new Vector3(1.5f, 4.0f, 0.0f);
    public float mass1 = 3;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere1.name = "sphere1";
        sphere1.transform.position = p1;
        GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2.name = "sphere2";
        sphere2.transform.position = p2;
        sphere2.AddComponent<Rigidbody>();
        GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere3.name = "sphere3";
        sphere3.transform.position = p3;
        sphere3.AddComponent<Rigidbody>();

        Rigidbody fristRG = sphere1.AddComponent<Rigidbody>();
        SpringJoint fristcube = sphere1.AddComponent<SpringJoint>();
        fristRG.isKinematic = true;
        fristRG.mass = mass1;

        fristcube.connectedBody = sphere2.GetComponent<Rigidbody>();
        fristcube.damper = 0.5f;

        SpringJoint secondcube = sphere2.AddComponent<SpringJoint>();
        secondcube.connectedBody = sphere3.GetComponent<Rigidbody>();        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
