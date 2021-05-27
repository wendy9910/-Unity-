using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class AddManyMesh : MonoBehaviour
{

    public List<GameObject> HairModel = new List<GameObject>();
    
    int n = 0;
    int down = 0;
    int count = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (Input.GetMouseButtonDown(0))
        {  
            GameObject Model = new GameObject();
            Model.name = "Hair" + n;
            Model.AddComponent<MeshFilter>();
            Model.AddComponent<MeshRenderer>();
            Model.AddComponent<TryModel>();
            HairModel.Add(Model);
            n++;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            

            down = 0; 
        }


       
    }
}
