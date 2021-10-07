using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SaveModel : MonoBehaviour
{
    public Mesh ComMesh,OldMesh;
    public MeshFilter ComMf;
    Vector3[] vertice;
    Vector2[] uv;
    Vector4[] tangents;
    int[] triangle;

    public void CombineMesh(List<GameObject> HairModel, int count,GameObject Finalobject) 
    {

        ComMf = Finalobject.GetComponent<MeshFilter>();
        ComMf.mesh = ComMesh = new Mesh();
        ComMesh.name = "HairModel";

        int VCount=0, TCount=0;

        for (int i = 0; i < HairModel.Count; i++) 
        {
            OldMesh = HairModel[i].GetComponent<MeshFilter>().mesh;
            VCount += OldMesh.vertices.Length;
            TCount += OldMesh.triangles.Length;
            Debug.Log("VC" + VCount);
        }
        vertice = new Vector3[VCount];
        uv = new Vector2[VCount];
        tangents = new Vector4[VCount];
        triangle = new int[TCount];

        for (int i = 0, x1 = 0, x2 = 0; i < HairModel.Count; i++)
        {
            OldMesh = HairModel[i].GetComponent<MeshFilter>().mesh;
            for (int j = 0; j < OldMesh.vertices.Length; j++)
            {
                vertice[x1] = OldMesh.vertices[j];
                uv[x1] = OldMesh.uv[j];
                tangents[x1] = OldMesh.tangents[j];
                x1++;
            }

            for (int k = 0; k < OldMesh.triangles.Length;k++) 
            {
                triangle[x2] = OldMesh.triangles[k];
                x2++;
            }       
        }

        Debug.Log(vertice.Length);

        ComMesh.vertices = vertice;
        ComMesh.uv = uv;
        ComMesh.tangents = tangents;
        ComMesh.triangles = triangle;

        using (StreamWriter streamWriter = new StreamWriter(string.Format("Assets/Model/{0}{1}.obj", ComMf.mesh.name, count)))
        {

            streamWriter.Write(MeshToString(ComMf, new Vector3(-1f, 1f, 1f)));
            streamWriter.Close();
        }
        AssetDatabase.Refresh();


    }

    /*public void BulidModel(GameObject HairModel,int count) 
    {
        MeshFilter mf2 = HairModel.GetComponent<MeshFilter>();
        Material ma2 = HairModel.GetComponent<Material>();
        using (StreamWriter streamWriter = new StreamWriter(string.Format("Assets/Model/{0}{1}.obj", mf2.mesh.name, count)))
        {

            streamWriter.Write(MeshToString(mf2, new Vector3(-1f, 1f, 1f), HairModel));
            streamWriter.Close();
        }
        AssetDatabase.Refresh();
    }*/
    private string MeshToString(MeshFilter mf, Vector3 scale)
    {
        Mesh mesh = mf.mesh;
        Material[] haredMaterials = mf.GetComponent<MeshRenderer>().materials;
        Vector2 textureOffset = mf.GetComponent<MeshRenderer>().material.GetTextureOffset("_MainTex");
        Vector2 textureScale = mf.GetComponent<MeshRenderer>().material.GetTextureScale("_MainTex");

        StringBuilder stringBuilder = new StringBuilder().Append("mtllib design.mtl").Append("\n").Append("g ").Append(mf.name).Append("\n");

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vector = vertices[i];
            stringBuilder.Append(string.Format("v {0} {1} {2}\n", vector.x * scale.x, vector.y * scale.y, vector.z * scale.z));
        }

        stringBuilder.Append("\n");

        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        if (mesh.subMeshCount > 1)
        {
            int[] triangles = mesh.GetTriangles(1);

            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (!dictionary.ContainsKey(triangles[j]))
                {
                    dictionary.Add(triangles[j], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 1]))
                {
                    dictionary.Add(triangles[j + 1], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 2]))
                {
                    dictionary.Add(triangles[j + 2], 1);
                }
            }
        }

        for (int num = 0; num != mesh.uv.Length; num++)
        {
            Vector2 vector2 = Vector2.Scale(mesh.uv[num], textureScale) + textureOffset;

            if (dictionary.ContainsKey(num))
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", mesh.uv[num].x, mesh.uv[num].y));
            }
            else
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", vector2.x, vector2.y));
            }
        }

        for (int k = 0; k < mesh.subMeshCount; k++)
        {
            stringBuilder.Append("\n");

            if (k == 0)
            {
                stringBuilder.Append("usemtl ").Append("Material_design").Append("\n");
            }

            if (k == 1)
            {
                stringBuilder.Append("usemtl ").Append("Material_logo").Append("\n");
            }

            int[] triangles2 = mesh.GetTriangles(k);

            for (int l = 0; l < triangles2.Length; l += 3)
            {
                stringBuilder.Append(string.Format("f {0}/{0} {1}/{1} {2}/{2}\n", triangles2[l] + 1, triangles2[l + 2] + 1, triangles2[l + 1] + 1));
            }
        }

        return stringBuilder.ToString();
    }
}
