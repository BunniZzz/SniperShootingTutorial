using UnityEditor;
using UnityEngine;

using UnityEditorInternal;
using System.Collections.Generic;


public static class FindPrefabAsset
{
    [MenuItem("Tools/Find Prefab Asset %b")]
    static void FindAsset()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj == null)
            return;

        string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);

        if (!string.IsNullOrEmpty(path))
        {
            Object prefab = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = prefab;
            EditorGUIUtility.PingObject(prefab);
        }
    }
}



public static class CopyAllComponents 
{
    // Internal buffer to store the copied components
    private static List<Component> copiedComponents = new List<Component>();

    [MenuItem("Tools/Copy All Components %#&C")]
    static void CopyAll() 
    {
        var source = Selection.activeGameObject;
        if (source == null) return;

        copiedComponents.Clear();
        Component[] components = source.GetComponents<Component>();

        foreach (var c in components) 
        {
            if (c is Transform) continue;
            copiedComponents.Add(c);
        }

        Debug.Log($"Successfully cached {copiedComponents.Count} components.");
    }

    [MenuItem("Tools/Paste All Components %#&V")]
    static void PasteAll() 
    {
        var target = Selection.activeGameObject;
        if (target == null || copiedComponents.Count == 0) return;

        Undo.RegisterCompleteObjectUndo(target, "Paste All Components");

        foreach (var c in copiedComponents) 
        {
            if (c == null) continue;
            ComponentUtility.CopyComponent(c);
            ComponentUtility.PasteComponentAsNew(target);
        }
        
        Debug.Log($"Pasted {copiedComponents.Count} components onto {target.name}.");
    }
}
