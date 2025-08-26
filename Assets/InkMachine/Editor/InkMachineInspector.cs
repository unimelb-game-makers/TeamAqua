using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace InkMachine{
    public class InkMachineInspector : EditorWindow
    {
        public List<Object> InkFiles = new List<Object>();

        private string filePath = "Assets/Ink/Dialogues/";

        SerializedProperty filesProperty;
        SerializedObject so;

        [MenuItem("Tools/Ink Machine")]
        public static void ShowWindow(){
            GetWindow(typeof(InkMachineInspector));
        }

        private void OnEnable() {
            ScriptableObject target = this;
            so = new SerializedObject(target);
            filesProperty = so.FindProperty("InkFiles");
        }

        // Show the inspector GUI
        private void OnGUI() {
            // File Path specification
            filePath = EditorGUILayout.TextField("File Path", filePath);
            // List of file objects
            EditorGUILayout.PropertyField(filesProperty, true); // true to show children
            // Drag files box
            DrawSimpleDragDropArea();

            so.ApplyModifiedProperties();
            
            // Sort files button
            if (GUILayout.Button("Sort Files")){
                InkMachineUtils.SortFiles(InkFiles, filePath);
            }
        }

        // Draws a drag-drop area and handles all functionality in one method
        private void DrawSimpleDragDropArea()
        {
            // Create and draw the drag-drop area
            Rect dropArea = GUILayoutUtility.GetRect(0.0f, 60.0f, GUILayout.ExpandWidth(true));
            GUI.Box(dropArea, "Drag files here\n(Supports multiple selection)", EditorStyles.helpBox);
            
            // Center text
            GUIStyle centeredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
            GUI.Label(dropArea, "Drop any files here", centeredStyle);
            
            // Handle events
            Event currentEvent = Event.current;
            
            if (currentEvent.type == EventType.DragUpdated || currentEvent.type == EventType.DragPerform)
            {
                if (dropArea.Contains(currentEvent.mousePosition))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    
                    if (currentEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        
                        foreach (Object draggedObject in DragAndDrop.objectReferences)
                        {
                            if (!IsObjectInList(draggedObject))
                            {
                                filesProperty.arraySize++;
                                filesProperty.GetArrayElementAtIndex(filesProperty.arraySize - 1).objectReferenceValue = draggedObject;
                            }
                        }
                        
                        currentEvent.Use();
                    }
                }
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

