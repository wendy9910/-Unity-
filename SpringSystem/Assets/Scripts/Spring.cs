using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint), typeof(Rigidbody), typeof(BoxCollider))]
public class Spring : MonoBehaviour
{
    public Vector3[] SpherePos = new Vector3[3];
    public Vector3[] LinePos = new Vector3[3];
    public GameObject[] Sphere = new GameObject[3];

    private LineRenderer player;

    public Vector3 p1= new Vector3(0.5f,4.0f,0.0f);
    public Vector3 p2 = new Vector3(1.0f,6.0f, 0.0f);
    public Vector3 p3 = new Vector3(1.5f, 8.0f, 0.0f);
    public float mass1 = 3;

    // Start is called before the first frame update
    void Start()
    {
        SpherePos[0] = new Vector3(0.5f, 4.0f, 0.0f);
        SpherePos[1] = new Vector3(1.0f, 6.0f, 0.0f);
        SpherePos[2] = new Vector3(1.5f, 8.0f, 0.0f);

        Sphere[0] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere[0].name = "sphere1";
        Sphere[0].transform.position = SpherePos[0];
        Sphere[1] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere[1].name = "sphere2";
        Sphere[1].transform.position = SpherePos[1];
        Sphere[1].AddComponent<Rigidbody>();
        Sphere[2] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere[2].name = "sphere3";
        Sphere[2].transform.position = SpherePos[2];
        Sphere[2].AddComponent<Rigidbody>();

        Rigidbody fristRG = Sphere[0].AddComponent<Rigidbody>();
        SpringJoint fristcube = Sphere[0].AddComponent<SpringJoint>();
        fristRG.isKinematic = true;
        fristRG.mass = mass1;

        fristcube.connectedBody = Sphere[1].GetComponent<Rigidbody>();
        fristcube.damper = 0.5f;

        SpringJoint secondcube = Sphere[1].AddComponent<SpringJoint>();
        secondcube.connectedBody = Sphere[2].GetComponent<Rigidbody>();

        SetRenderer();
    }

    // Update is called once per frame
    void Update()
    {
       
        LinePos[0] = Sphere[0].transform.position;
        LinePos[1] = Sphere[1].transform.position;
        LinePos[2] = Sphere[2].transform.position;

        player.positionCount = LinePos.Length;
        player.SetPositions(LinePos);

    }

    void SetRenderer()
    {
        //LineRenderer
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.yellow, Color.blue);
        player.SetWidth(0.2f, 0.2f);
        player.numCapVertices = 2;//端點圓度
        player.numCornerVertices = 2;//拐彎圓滑度


    }
}
