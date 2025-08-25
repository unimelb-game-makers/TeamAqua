using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InkMachine : EditorWindow
{
    public List<Object> InkFiles = new List<Object>();

    private string filePath = "Assets/Ink/Dialogues/";
    private HashSet<string> fileNames;

    SerializedProperty filesProperty;
    SerializedObject so;

    [MenuItem("Tools/Ink Machine")]
    public static void ShowWindow(){
        GetWindow(typeof(InkMachine));
    }

    private void OnEnable() {
        ScriptableObject target = this;
        so = new SerializedObject(target);
        filesProperty = so.FindProperty("InkFiles");
    }

    private void OnGUI(){
        filePath = EditorGUILayout.TextField("File Path", filePath);

        EditorGUILayout.PropertyField(filesProperty, true); // true to show children

        DrawSimpleDragDropArea();
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Sort Files")){
            SortFiles();
        }
    }

    /// <summary>
    /// Draws a drag-drop area and handles all functionality in one method
    /// </summary>
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

    // Take the files and place them into corresponding folder paths
    private void SortFiles(){
        // Assets/Ink/Dialogues/Act 5/A5S2
        if(CreateDirectory(filePath)){
            Debug.Log($"Created Directory {filePath}");
        }
        // Copy the files into the directory
        CopyFilesTo(InkFiles, filePath);
        // Create linking ink file

    }

    private void GenerateInk(){

    }

    // Copy each file in the file list to the target directory
    private void CopyFilesTo(List<Object> Files, string targetDir){
        foreach(Object file in Files){
            if (file == null) continue;
            string sourcePath = AssetDatabase.GetAssetPath(file);
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(targetDir, fileName);

            FileUtil.CopyFileOrDirectory(sourcePath, targetPath);
        }
        AssetDatabase.Refresh();
    }

    // Create a directory and return true and false whether it already exists
    private bool CreateDirectory(string path){
        if (!Directory.Exists(path)){
            Directory.CreateDirectory(path);
            AssetDatabase.Refresh();
            return true;
        }
        else{
            Debug.LogWarning($"Directory already exists: {path}");
            return false;
        }
    }

    
}
