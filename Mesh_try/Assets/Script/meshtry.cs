using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class meshtry : MonoBehaviour
{
    public int xSize, ySize;

    private void Awake()
    {
       StartCoroutine(Generate());
    }

    private Vector3[] vertices;
    private Mesh mesh;
    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "P_Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }

        mesh.vertices = vertices;
        
        int[] triangles = new int[xSize * ySize * 6];

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, vi++, ti+=6) {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
               // mesh.RecalculateNormals();
                yield return wait;
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (vertices == null) {
            return;
        }
        Gizmos.color = Color.black;
        for (int i=0; i<vertices.Length;i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
