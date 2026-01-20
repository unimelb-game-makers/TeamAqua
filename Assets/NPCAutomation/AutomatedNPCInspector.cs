using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


namespace AutomateNPC {
    public class AutomatedNPCInspector : EditorWindow
    {

        SerializedProperty filesProperty;
        SerializedObject so;
        [MenuItem("Tools/Automate NPC Collider")]

        public static void ShowWindow(){
            GetWindow(typeof(AutomatedNPCInspector));
        }

        private void OnEnable()
        {
            ScriptableObject target = this;
            so = new SerializedObject(target);
            filesProperty = so.FindProperty("NPCmodels");
        }

        // Show the inspector GUI
        private void OnGUI() {
            // Button for setting collider layer mask in every npc model inside Assets/Prefabs/Models/NPCs folder
            if (GUILayout.Button("Fix NPC")){
                AutomatedNpc.HandleColliderLayer();
                Debug.Log("triggering npc collider automation...");
            }
        }

        private bool IsObjectInList(Object obj)
        {
            for (int i = 0; i < filesProperty.arraySize; i++)
            {
                if (filesProperty.GetArrayElementAtIndex(i).objectReferenceValue == obj)
                    return true;
            }
            return false;
        }
    }
}