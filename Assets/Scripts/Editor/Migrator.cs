using System;
using System.Collections.Generic;
using Tilemap3D;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Migrator : EditorWindow
{
    private string targetName = "Target Object";
    private GameObject replacementPrefab;
    private MonoScript selectedScript;

    [MenuItem("Tools/Migrator")]
    public static void ShowWindow()
    {
        GetWindow<Migrator>("Migrator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Replace Objects in Scene", EditorStyles.boldLabel);
        targetName = EditorGUILayout.TextField("Target Object Name", targetName);
        replacementPrefab = (GameObject)EditorGUILayout.ObjectField("Replacement Prefab", replacementPrefab, typeof(GameObject), false);
        selectedScript = (MonoScript)EditorGUILayout.ObjectField("Script Filter (Optional)", selectedScript, typeof(MonoScript), false);


        if (GUILayout.Button("Replace"))
        {
            if (replacementPrefab == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a prefab to replace with.", "OK");
                return;
            }

            ReplaceInPrefab();
        }
    }

    private void ReplaceInPrefab()
    {
        PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage == null)
        {
            EditorUtility.DisplayDialog("Not in Prefab Mode", "You must be in Prefab Mode to use this tool.", "OK");
            return;
        }

        GameObject root = prefabStage.prefabContentsRoot;
        Transform[] allChildren = root.GetComponentsInChildren<Transform>(true);
        int count = 0;

        Dictionary<GameObject, GameObject> replaceDictionary = new();

        Type scriptType = null;
        if (selectedScript)
            scriptType = selectedScript.GetClass();
        
        foreach (Transform t in allChildren)
        {
            if (t.name == targetName)
            {
                if (scriptType != null && !t.GetComponent(scriptType))
                    continue;
                
                GameObject original = t.gameObject;
                Transform parent = original.transform.parent;
                Vector3 originalPosition = original.transform.localPosition;
                Quaternion rotation = original.transform.localRotation;
                Vector3 scale = original.transform.localScale;

                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(replacementPrefab, prefabStage.scene);
                newObj.transform.SetParent(parent);
                newObj.transform.localPosition = originalPosition;
                newObj.transform.localRotation = rotation;
                newObj.transform.localScale = scale;
                
                replaceDictionary.Add(original, newObj);
                count++;
            }
        }

        foreach (KeyValuePair<GameObject, GameObject> toReplace in replaceDictionary)
        {
            Undo.DestroyObjectImmediate(toReplace.Key);
            Undo.RegisterCreatedObjectUndo(toReplace.Value, "Replace Object in Prefab");
        }

        Debug.Log($"Replaced {count} object(s) named '{targetName}' in prefab with '{replacementPrefab.name}'.");
    }
}
