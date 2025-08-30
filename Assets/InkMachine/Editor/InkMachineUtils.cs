using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InkMachine{
    public class InkMachineUtils{
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
            GenerateInkLink(days, inkScript_FP, actSceneData.ActScene);

            // --> Create the Dialogue Nodes for each day <--
            
            CreateDirectory(dialogueNode_FP);
            foreach(string day in days){
                DialogueNode dialogueNode = ScriptableObject.CreateInstance<DialogueNode>();
                AssetDatabase.CreateAsset(dialogueNode, $"{dialogueNode_FP}{day}.asset");
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            // --> Create Dialogue Script for act <--

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
        private static void GenerateInkLink(List<string> days, string path, string f_name){
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