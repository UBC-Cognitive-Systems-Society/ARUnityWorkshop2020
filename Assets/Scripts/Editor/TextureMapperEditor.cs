using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextureMapper))]
public class TextureMapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TextureMapper tex = target as TextureMapper;

        if(GUILayout.Button(new GUIContent("Load Renderers"))){
            Undo.RecordObject(tex, "Set renderers");
            tex.GetAllRenderers();
        }
        if (GUILayout.Button(new GUIContent("Preview Texture")))
        {
            tex.PreviewTex();
        }
        if (GUILayout.Button(new GUIContent("Clear Texture")))
        {
            tex.ClearTexture();
        }
    }

}
