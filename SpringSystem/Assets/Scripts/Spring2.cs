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
    public float mass1 = 1f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Input.mousePosition;

            float x0 = (mousePos.x - Screen.width / 2) / (Screen.width / 2);
            float y0 = (mousePos.y - Screen.height / 2) / (Screen.height / 2);

            float y = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * y0;// 求tan(30);
            float x = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * x0 * Screen.width / Screen.height; //加上 Screen.width/Screen.height 控制螢幕寬變動

            MousePos = new Vector3(x, y, 0.0f);
            LastPos = new Vector3(x, y, 0.0f);
            MousePointPos.Add(LastPos);

            SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sphere.transform.position = MousePos;

            Rigidbody RG = sphere.AddComponent<Rigidbody>();
            RG.isKinematic = true;
            RG.mass = mass1;

            Down = true;
        }
        if (Down == true) 
        {
            Vector3 mousePos = Input.mousePosition;

            float x0 = (mousePos.x - Screen.width / 2) / (Screen.width / 2);
            float y0 = (mousePos.y - Screen.height / 2) / (Screen.height / 2);
            float y = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * y0;// 求tan(30);
            float x = 10 * Mathf.Tan(Mathf.Deg2Rad * 30) * x0 * Screen.width / Screen.height; //加上 Screen.width/Screen.height 控制螢幕寬變動

            MousePos = new Vector3(x, y, 0.0f);
            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 1f)
            {//更新座標
                MousePos = new Vector3(x, y, 0.0f);
                MousePointPos.Add(MousePos);
                LastPos = new Vector3(x, y, 0.0f);


                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                sphere.transform.position = MousePos;
                Rigidbody RG = sphere.AddComponent<Rigidbody>();
                RG.isKinematic = true;
                RG.mass = mass1;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Down = false;            
            if (Down==false) {
                spring();
            }
            //MousePointPos.Clear();
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
                MainSpring.spring = 20.0f;
                MainSpring.damper = 5.0f;

                SphereGroup[i].transform.position = MousePointPos[i];
                SphereGroup[i+1].transform.position = MousePointPos[i+1];

                
                Rigidbody otherRG = SphereGroup[i+1].GetComponent<Rigidbody>();
                
                otherRG.isKinematic = false;
                otherRG.mass = mass1;
                MainSpring.connectedBody = otherRG;
            }
            

        }


    }

}
