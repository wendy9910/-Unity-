using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChangeTexture : MonoBehaviour
{

    public Texture MyTexture;
    Material GetPicture;
    public Material GethairColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionGenerate();
    }

    List<Vector3> PointPos = new List<Vector3>();
    public void PositionGenerate() 
    {
        for (int i = 0; i < 10; i++) 
        {
            for (int j = 0; j < 20; j++) 
            {
                PointPos.Add(new Vector3(i,j,10));
            }
        }
    }

    Mesh mesh;
    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangent;
    public void MeshGenerate() 
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshRenderer>().material = GethairColor;
        mesh.name = "HairModel";

        vertice = new Vector3[PointPos.Count];
        uv = new Vector2[PointPos.Count];
        tangent = new Vector4[PointPos.Count];

        for (int i = 0;i < PointPos.Count; i++) 
        {
            vertice[i] = PointPos[i];
            uv[i] = new Vector2(PointPos[i].x,PointPos[i].y);
            tangent[i] = new Vector4(1f, 0f, 0f, -1f);
        }
        mesh.vertices = vertice;
        mesh.uv = uv;
        mesh.tangents = tangent;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < PointPos.Count; i++)
        {
            Gizmos.DrawSphere(PointPos[i], 0.1f);
        }
    }


}
