using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UniGLTF;
using UnityEngine.UI;
using VRM;
using VRMShaders;

public class VRMSave : MonoBehaviour
{
    [SerializeField]
    public bool UseNormalize = true;
    GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.Find("boyFBX");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s")) OnExportClicked();

    }

    #region Export

    void OnExportClicked()
    {
        //#if UNITY_STANDALONE_WIN
#if false
        var path = FileDialogForWindows.SaveDialog("save VRM", Application.dataPath + "/export.vrm");
#else
        var path = Application.dataPath + "/../export.vrm";
#endif
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        var bytes = UseNormalize ? ExportCustom(model) : ExportSimple(model);

        File.WriteAllBytes(path, bytes);
        Debug.LogFormat("export to {0}", path);
    }

    static byte[] ExportSimple(GameObject model)
    {
        var vrm = VRMExporter.Export(new UniGLTF.GltfExportSettings(), model, new RuntimeTextureSerializer());
        var bytes = vrm.ToGlbBytes();
        return bytes;
    }

    static byte[] ExportCustom(GameObject exportRoot, bool forceTPose = false)
    {
        // normalize
        var target = VRMBoneNormalizer.Execute(exportRoot, forceTPose);

        try
        {
            return ExportSimple(target);
        }
        finally
        {
            // cleanup
            GameObject.Destroy(target);
        }
    }

    void OnExported(UniGLTF.glTF vrm)
    {
        Debug.LogFormat("exported");
    }

    #endregion

}
