using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

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
        public static void GenerateInkLink(List<Object> files, string path){
            // Get the last 4 characters of path
            string fileName = path.Length >= 4 ? path.Substring(path.Length - 4) : path;
            string inkFilePath = $"{path}/{fileName}.ink";
            File.WriteAllText(inkFilePath, "");
            
            AssetDatabase.Refresh();
        }

        // Copy each file in the file list to the target directory
        public static void CopyFilesTo(List<Object> Files, string targetDir){
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
        public static bool CreateDirectory(string path){
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