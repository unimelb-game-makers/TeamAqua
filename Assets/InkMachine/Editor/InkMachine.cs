using UnityEditor;
using UnityEngine;

public class InkMachine : EditorWindow
{
    public string[] Strings = {"testing 1", "testing 2", "testing 3"};
    SerializedObject so;

    [MenuItem("Tools/Ink Machine")]
    public static void ShowWindow(){
        GetWindow(typeof(InkMachine));
    }

    private void OnEnable() {
        ScriptableObject target = this;
        so = new SerializedObject(target);
    }

    private void OnGUI(){
        
        SerializedProperty stringsProperty = so.FindProperty("Strings");

        EditorGUILayout.PropertyField(stringsProperty, true); // true to show children
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Generate Ink")){
            GenerateInk();
        }
    }

    private void GenerateInk(){

    }
}
