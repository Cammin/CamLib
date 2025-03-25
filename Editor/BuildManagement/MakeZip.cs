using System.IO;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public static class MakeZip
    {
        [MenuItem("Tools/CamLib/Zip Build - Make Folder Locations")]
        public static void MakeBuildZipFolderLocation()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            string buildDir = Path.Combine(Application.dataPath, "..", "Build", target.ToString());
            Directory.CreateDirectory(buildDir);

            ShowExplorer(buildDir);
        }

        /// <summary>
        /// Will look for a build inside of the "Build" folder, and generates a folder called "Builds" which contains the zip for the current platform.
        /// </summary>
        [MenuItem("Tools/CamLib/Zip Build - Make Zip")]
        public static void MakeBuildZip()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            
            string buildPath = Path.Combine(Application.dataPath, "..", "Build", target.ToString(), $"{PlayerSettings.productName}.exe");
            buildPath = Path.GetFullPath(buildPath);
            string buildDir = Path.GetDirectoryName(buildPath);
            
            Directory.CreateDirectory(buildDir);

            string buildsName = $"{PlayerSettings.productName}_{PlayerSettings.bundleVersion}";
            if (target == BuildTarget.Android)
            {
                buildsName += ".apk";
            }
            else
            {
                buildsName += ".zip";
            }
            
            string buildsPath = Path.Combine(Application.dataPath, "..", "Builds", target.ToString(), buildsName);
            buildsPath = Path.GetFullPath(buildsPath);
            string buildsDir = Path.GetDirectoryName(buildsPath);
            
            Directory.CreateDirectory(buildsDir);
            

            if (File.Exists(buildsPath))
            {
                Debug.Log($"It's already the same build! {Path.GetFileName(buildsPath)}");
            }
            else
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(buildDir, buildsPath);
            }
            
            ShowExplorer(buildsPath);
        }

        [MenuItem("Tools/CamLib/PurgeAllZips")]
        public static void PurgeAllZips()
        {
            string buildsPath = Path.Combine(Application.dataPath, "..", "Builds", $"{PlayerSettings.productName}_{PlayerSettings.bundleVersion}.zip");
            buildsPath = Path.GetFullPath(buildsPath);
            
            string buildsDir = Path.GetDirectoryName(buildsPath);
            if (buildsDir == null)
            {
                return;
            }
            
            string[] zips = Directory.GetFiles(buildsDir, "*.zip");
            Debug.Log($"Found zips: {zips.Length}");
            foreach (string zip in zips)
            {
                Debug.Log($"Delete zip: {Path.GetFileName(zip)}");
                File.Delete(zip);
            }
        }
        
        public static void ShowExplorer(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", "/select,"+itemPath);
        }
    }
}