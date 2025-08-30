using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InkMachine{
    public class InkMachineUtils{
        private static string curDialogueScriptPath;
        private static string compiledJSONFilePath;

        // Take the files and place them into corresponding folder paths
        public static void SortFiles(List<Object> files){
            // Read days
            List<string> days = GetDays(files);
            // Read days and derive Act_Scene and Act
            ActSceneData actSceneData = new ActSceneData(days); // Contains 

            if (actSceneData.ActScene == "Error"){
                Debug.LogError("Ink file days are not matching or valid");
                return;
            }
            
            // Derive paths
            string inkScript_FP = $"Assets/Ink/Dialogues/{actSceneData.Act}/{actSceneData.ActScene}/";
            string dialogueNode_FP = $"Assets/ScriptableObjects/Dialogue/{actSceneData.Act}/Dialogue/{actSceneData.ActScene}/";
            string dialogueScript_FP = $"Assets/ScriptableObjects/Dialogue/{actSceneData.Act}/Script/";

            // --> Organise Ink Files <--

            // Assets/Ink/Dialogues/Act 5/A5S2
            CreateDirectory(inkScript_FP);
            // Copy the files into the directory
            CopyFilesTo(files, inkScript_FP);
            // Create linking ink file
            string inkLinkFilePath = GenerateInkLink(days, inkScript_FP, actSceneData.ActScene);
            Debug.Log($"inkLinkFile = {inkLinkFilePath}");
            // --> Create the Dialogue Nodes for each day <--
            List<DialogueNode> dialogueNodes = new List<DialogueNode>();

            CreateDirectory(dialogueNode_FP);
            foreach(string day in days){
                DialogueNode dialogueNode = ScriptableObject.CreateInstance<DialogueNode>();
                AssetDatabase.CreateAsset(dialogueNode, $"{dialogueNode_FP}{day}.asset");
                dialogueNodes.Add(dialogueNode);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // --> Create Dialogue Script for act <--
            DialogueScript dialogueScript = ScriptableObject.CreateInstance<DialogueScript>();
            dialogueScript.dialogues = dialogueNodes;
            
            // Try and link the compiled json
            compiledJSONFilePath = $"{inkScript_FP}{actSceneData.ActScene}.json";
            EditorApplication.delayCall += () =>{
                for (int i = 0; i < 10; i++){
                    if(File.Exists(compiledJSONFilePath))
                        dialogueScript.inkFile = AssetDatabase.LoadAssetAtPath<TextAsset>(compiledJSONFilePath);
                    System.Threading.Thread.Sleep(100);
                }
            };
            curDialogueScriptPath = $"{dialogueScript_FP}{actSceneData.Act_Scene}.asset";
            AssetDatabase.CreateAsset(dialogueScript, curDialogueScriptPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"dialogues = {dialogueScript.dialogues}, inkFile = {dialogueScript.inkFile}");
        }

        public static void HandleLog(string logString, string stackTrace, LogType type){
            Debug.Log($"logString = {logString}");
            if(logString.Contains("Ink compilation completed") && curDialogueScriptPath != null && compiledJSONFilePath != null){
                DialogueScript dialogueScriptSO = AssetDatabase.LoadAssetAtPath<DialogueScript>(curDialogueScriptPath);
                dialogueScriptSO.inkFile = AssetDatabase.LoadAssetAtPath<TextAsset>(compiledJSONFilePath);

                EditorUtility.SetDirty(dialogueScriptSO);
                AssetDatabase.SaveAssets();
                                
                curDialogueScriptPath = null;
                compiledJSONFilePath = null;
                
                Debug.Log("Hit here");
            }
        }

        // Read in and collect the list of days - A5_S2_D1, A5_S2_D2, etc
        private static List<string> GetDays(List<Object> files){
            List<string> days = new List<string>();

            // Write the include lines and make list of days
            foreach(Object file in files){
                if (IsInkFile(file)){
                    string f_Name = file.name;
                    days.Add(f_Name);
                }
            }
            return days;
        }

        // Create linking ink file and write ink script
        private static string GenerateInkLink(List<string> days, string path, string f_name){
            // Get the last 4 characters of path
            string inkFilePath = $"{path}{f_name}.ink";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INCLUDE ../../Globals/Globals.ink");
            // Write the include lines and make list of days
            foreach(string day in days){
                sb.AppendLine($"INCLUDE {day}.ink");
            }
            sb.AppendLine("// Variable Setup");
            for(int i = 0; i < days.Count; i++){
                sb.AppendLine($"CONST DIALOGUE_{i + 1} = \"{days[i]}\"");
            }
            sb.AppendLine("{");
            for(int i = 0; i < days.Count; i++){
                sb.AppendLine($"\t- dialogue_id == DIALOGUE_{i+1}:");
                sb.AppendLine($"\t\t-> {days[i]}");
            }
            sb.AppendLine("}");
            
            // Ink Script Done and write to file
            File.WriteAllText(inkFilePath, sb.ToString());
            AssetDatabase.Refresh();
            Debug.Log($"Generated {f_name}.ink at {inkFilePath}");

            return inkFilePath;
        }

        public static bool IsInkFile(Object obj)
        {
            if (obj == null) return false;
            
            string path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path)) return false;
            
            return System.IO.Path.GetExtension(path).ToLower() == ".ink";
        }

        // Copy each file in the file list to the target directory
        private static void CopyFilesTo(List<Object> Files, string targetDir){
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
        private static void CreateDirectory(string path){
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
                Debug.Log($"Created Directory {path}");
            }
            else{
                Debug.LogWarning($"Directory already exists: {path}");
            }
        }
    }
}