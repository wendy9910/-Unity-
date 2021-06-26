using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryGetRange : MonoBehaviour
{

    public List<string> Mystring = new List<string>();
    public List<string> GetSring = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Mystring.Add("A");
        Mystring.Add("B");
        Mystring.Add("C");
        Mystring.Add("D");


        GetSring.AddRange(Mystring.GetRange(1,3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
