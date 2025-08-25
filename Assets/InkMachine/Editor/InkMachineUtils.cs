using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InkMachine{
    public class InkMachineUtils{
        // Take the files and place them into corresponding folder paths
        public static void SortFiles(List<Object> files, string path){
            // Assets/Ink/Dialogues/Act 5/A5S2
            if(CreateDirectory(path)){
                Debug.Log($"Created Directory {path}");
            }
            // Copy the files into the directory
            CopyFilesTo(files, path);
            // Create linking ink file
            GenerateInkLink(files, path);
        }

        // Create linking ink file
        private static void GenerateInkLink(List<Object> files, string path){
            // Get the last 4 characters of path
            string fileName = path.Length >= 4 ? path.Substring(path.Length - 4) : path;
            string inkFilePath = $"{path}/{fileName}.ink";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INCLUDE ../../Globals/Globals.ink");
            List<string> days = new List<string>(); // Make list of days - A5_S2_D1, A5_S2_D2, etc

            // Write the include lines and make list of days
            foreach(Object file in files){
                if (IsInkFile(file)){
                    string f_Name = file.name;
                    sb.AppendLine($"INCLUDE {f_Name}.ink");
                    days.Add(f_Name);
                }
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
            File.WriteAllText(inkFilePath, sb.ToString());

            AssetDatabase.Refresh();
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
        private static bool CreateDirectory(string path){
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
}