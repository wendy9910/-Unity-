using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class hairstyle : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] hairPos;
    public int width = 2;
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        MeshGenerate();
    }

    void SetPosition()
    {
        hairPos = new Vector3[25];

        hairPos[0] = new Vector3(0, 0, 0);
        hairPos[1] = new Vector3(0, 0, 0);
        hairPos[2] = new Vector3(0, 0, 0);
        hairPos[3] = new Vector3(0, 0, 0);
        hairPos[4] = new Vector3(0, 0, 0);

        hairPos[5] = new Vector3(-1, -1, 0);
        hairPos[6] = new Vector3(-1, -1, 0);
        hairPos[7] = new Vector3(0, -1, 0);
        hairPos[8] = new Vector3(1, -1, 0);
        hairPos[9] = new Vector3(1, -1, 0);

        hairPos[10] = new Vector3(-2, -2, 0);
        hairPos[11] = new Vector3(-2, -2, 0);
        hairPos[12] = new Vector3(0, -2, 0);
        hairPos[13] = new Vector3(2, -2, 0);
        hairPos[14] = new Vector3(2, -2, 0);

        hairPos[15] = new Vector3(-1, -3, 0);
        hairPos[16] = new Vector3(-1, -3, 0);
        hairPos[17] = new Vector3(0, -3, 0);
        hairPos[18] = new Vector3(1, -3, 0);
        hairPos[19] = new Vector3(1, -3, 0);

        hairPos[20] = new Vector3(0, -4, 0);
        hairPos[21] = new Vector3(0, -4, 0);
        hairPos[22] = new Vector3(0, -4, 0);
        hairPos[23] = new Vector3(0, -4, 0);
        hairPos[24] = new Vector3(0, -4, 0);

    }

    void MeshGenerate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Hair Grid";

        mesh.vertices = hairPos;//mesh網格點生成

        int point = 0;

        point = ((hairPos.Length / (3 + (width - 1) * 2) - 1)) * 2 * width;//計算網格數


        int[] triangles = new int[point * 6];//計算需要多少三角形點座標

        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (width - 1)), vi + 4 + (2 * (width - 1)));
            if (x % (width * 2) != point % (width * 2)) k = 1;
            else k = 2;
        }
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = v10;
        triangles[i + 2] = v01;
        triangles[i + 3] = v01;
        triangles[i + 4] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

}
