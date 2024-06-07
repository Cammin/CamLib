using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace CamLib.Editor
{
    public class SceneSwitcher
    {
        public List<SceneCategory> Categories = new List<SceneCategory>();

        public void GetScenes(CentralizedAssetWindowImplementation impl)
        {
            string[] folderPaths = impl.SceneFolders;
            foreach (string folderPath in folderPaths)
            {
                string[] guids = AssetDatabase.FindAssets("t:Scene", new string[] { folderPath });

                Categories.Clear();
                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    string categoryName = Path.GetDirectoryName(assetPath);
                    SceneCategory category = GetOrCreateCategory(categoryName);
                    category.AddPath(assetPath);
                }

                foreach (SceneCategory category in Categories)
                {
                    category.Sort();
                }
            }
        }

        private SceneCategory GetOrCreateCategory(string categoryTitle)
        {
            int i = Categories.FindIndex(category => category.Title == categoryTitle);
            if (i != -1)
            {
                return Categories[i];
            }
            
            SceneCategory category = new()
            {
                Title = categoryTitle,
            };
            Categories.Add(category);
            return category;
        }

        public void OnGUI()
        {
            EditorGUILayout.Space();
            foreach (SceneCategory category in Categories)
            {
                category.DrawCollection();
                EditorGUILayout.Space();
            }
        }
    }
}