using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCollider : MonoBehaviour
{
    private Vector3 NewPos, OldPos;//零時座標變數 New & Old
    public float length = 0.4f;
    int down = 0;

    public List<Vector3> PointPos = new List<Vector3>();
    public List<SphereCollider> SphereCollider = new List<SphereCollider>();
    public List<GameObject> SetCollider = new List<GameObject>();
    
    int c = 0;
    int n = 1; 
    //GameObject Hairmodel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (down == 0) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
                down = 1;
            }        
        }
        if (down == 1) 
        {
            NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));//new position
            float dist = Vector3.Distance(OldPos,NewPos);
            if (dist > length) 
            {
                PointPos.Add(OldPos);

                SetCol();

                OldPos = NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                n++;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //PointPos.Clear();
                c++;
                down = 0;
            
            }
        }

       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            Debug.Log(hit.transform.name);

        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Get");
    }

    void SetCol()
    {
        GameObject EmptySet = new GameObject();
        SphereCollider box = EmptySet.AddComponent<SphereCollider>();
        EmptySet.transform.position = OldPos;
        EmptySet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        box.name = "box" + c;
        box.center = OldPos;
        box.radius = 1.0f;
        box.isTrigger = true;
        SetCollider.Add(EmptySet);
    }

}
