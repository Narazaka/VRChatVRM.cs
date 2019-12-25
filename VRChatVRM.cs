using System.IO;
using UnityEngine;
using UnityEditor;
using VRM;

public class VRChatVRM : EditorWindow {
    [MenuItem("Assets/Create/VRM/VRMMetaObject")]
    public static void CreateVRMMetaObjectAsset() {
        ProjectWindowUtil.CreateAsset(CreateInstance<VRMMetaObject>(), "VRMMetaObject.asset");
    }

    [MenuItem("CONTEXT/BlendShapeAvatar/CreateDefaultPresets")]
    public static void CreateBlendShapeAvatar(MenuCommand menuCommand) {
        var blendShapeAvatar = menuCommand.context as BlendShapeAvatar;
        blendShapeAvatar.CreateDefaultPreset();
        var basePath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(blendShapeAvatar));
        foreach (var clip in blendShapeAvatar.Clips) {
            AssetDatabase.CreateAsset(clip, Path.Combine(basePath, clip.name + ".asset"));
        }
    }
 
    [MenuItem("Window/VRMとの切り替え")]
    static void Open() {
        GetWindow<VRChatVRM>("VRMとの切り替え");
    }

    [SerializeField]
    GameObject Avatar;

    [SerializeField]
    VRMMetaObject VRMMetaObject;

    [SerializeField]
    BlendShapeAvatar BlendShapeAvatar;

    void OnGUI() {
        EditorGUILayout.PrefixLabel("アバター");
        Avatar = EditorGUILayout.ObjectField(Avatar, typeof(GameObject), true) as GameObject;
        EditorGUILayout.PrefixLabel("VRMMetaObject");
        VRMMetaObject = EditorGUILayout.ObjectField(VRMMetaObject, typeof(VRMMetaObject), true) as VRMMetaObject;
        EditorGUILayout.PrefixLabel("BlendShapeAvatar");
        BlendShapeAvatar = EditorGUILayout.ObjectField(BlendShapeAvatar, typeof(BlendShapeAvatar), true) as BlendShapeAvatar;
        if (GUILayout.Button("VRM適用")) Apply();
        if (GUILayout.Button("VRMはずす")) Revert();
    }

    public void Apply () {
        if (VRMMetaObject != null) {
            var vrmMeta = Avatar.GetOrAddComponent<VRMMeta>();
            vrmMeta.Meta = VRMMetaObject;
        }
        if (BlendShapeAvatar != null) {
            var vrmBlendShapeProxy = Avatar.GetOrAddComponent<VRMBlendShapeProxy>();
            vrmBlendShapeProxy.BlendShapeAvatar = BlendShapeAvatar;
        }
    }

    public void Revert () {
        var vrmMeta = Avatar.GetComponent<VRMMeta>();
        if (vrmMeta != null) DestroyImmediate(vrmMeta);
        var vrmBlendShapeProxy = Avatar.GetComponent<VRMBlendShapeProxy>();
        if (vrmBlendShapeProxy != null) DestroyImmediate(vrmBlendShapeProxy);
    }
}
