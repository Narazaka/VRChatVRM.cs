using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRM;

public class VRChatVRM : EditorWindow {
    [MenuItem("Assets/Create/VRMMetaObject")]
    public static void CreateVRMMetaObjectAsset() {
        ProjectWindowUtil.CreateAsset(CreateInstance<VRMMetaObject>(), "Meta.asset");
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
        if (Avatar.transform.Find("secondary") == null) {
            var secondary = new GameObject("secondary");
            secondary.transform.parent = Avatar.transform;
            secondary.name = "secondary";
            SetSecondary(secondary);
        }
    }

    public void Revert () {
        var vrmMeta = Avatar.GetComponent<VRMMeta>();
        if (vrmMeta != null) DestroyImmediate(vrmMeta);
        var vrmBlendShapeProxy = Avatar.GetComponent<VRMBlendShapeProxy>();
        if (vrmBlendShapeProxy != null) DestroyImmediate(vrmBlendShapeProxy);
        var secondary = Avatar.transform.Find("secondary");
        if (secondary != null) DestroyImmediate(secondary.gameObject);
    }

    void SetSecondary(GameObject gameObject) {
        var vrmSpringBone = gameObject.GetOrAddComponent<VRMSpringBone>();
        vrmSpringBone.RootBones = new List<Transform> {
            Avatar.transform.Find("body/hips/spine/chest/neck/head/pony_tail_joint"),
            Avatar.transform.Find("body/hips/spine/chest/neck/head/lower_ahoge"),
            Avatar.transform.Find("body/hips/spine/bust.L"),
            Avatar.transform.Find("body/hips/spine/bust.R"),
        };
    }
}
