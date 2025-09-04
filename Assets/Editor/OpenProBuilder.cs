using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

public class OpenProBuilder
{
    [MenuItem("Tools/Open ProBuilder Window (Direct)")]
    public static void ShowProBuilder()
    {
        var type = Type.GetType("UnityEditor.ProBuilder.ProBuilderEditor, Unity.ProBuilder.Editor");
        if (type != null)
        {
            EditorWindow.GetWindow(type);
        }
        else
        {
            Debug.LogError("❌ ProBuilderEditor class not found. Coba downgrade ProBuilder di Package Manager ke versi 5.x");
        }
    }
}
