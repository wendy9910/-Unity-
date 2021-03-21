using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring2 : MonoBehaviour
{
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<GameObject> SphereGroup = new List<GameObject>();
    private Vector3 MousePos,LastPos;
    GameObject sphere;
    private bool Down;
    public float mass1 = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            MousePointPos.Add(LastPos);

            SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
            sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            sphere.transform.position = MousePos;

            Rigidbody RG = sphere.AddComponent<Rigidbody>();
            RG.isKinematic = true;

            Down = true;
        }
        if (Down == true) 
        {

            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 0.05f)
            {//更新座標
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
                MousePointPos.Add(MousePos);
                LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

               
                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                sphere.transform.position = MousePos;
                Rigidbody RG = sphere.AddComponent<Rigidbody>();
                RG.isKinematic = true;



            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Down = false;            
            if (Down==false) {
                spring();
            }
            MousePointPos.Clear();
        }

        void spring() 
        {
            Rigidbody fristRG = SphereGroup[0].GetComponent<Rigidbody>();
            fristRG.isKinematic = true;
            fristRG.mass = mass1;


            for (int i = 0; i < SphereGroup.Count - 1; i++)
            {

                SpringJoint MainSpring = SphereGroup[i].AddComponent<SpringJoint>();
                //MainSpring.maxDistance = 0.05f;
                //MainSpring.spring = 10;
                // MainSpring.damper = 1f;

                SphereGroup[i].transform.position = MousePointPos[i];
                SphereGroup[i+1].transform.position = MousePointPos[i+1];

                
                Rigidbody otherRG = SphereGroup[i+1].GetComponent<Rigidbody>();
                
                otherRG.isKinematic = false;
                otherRG.mass = 0.001f;
                
                MainSpring.connectedBody = otherRG;
            }
            

        }


    }

}
