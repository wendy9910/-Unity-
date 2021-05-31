using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Example();
    }

    public Transform meeple;
    public GameObject grandChild;

    public void Example()
    {
        //Assigns the transform of the first child of the Game Object this script is attached to.
        meeple = this.gameObject.transform.GetChild(0);

        //Assigns the first child of the first child of the Game Object this script is attached to.
        grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }
}
