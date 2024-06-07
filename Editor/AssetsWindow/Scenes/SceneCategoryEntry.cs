using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CamLib.Editor
{
    public class SceneCategoryEntry
    {
        public string Path;
        public string Name;
        public int BuildIndex;
        public bool Addressable;
                
        public SceneCategoryEntry(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            BuildIndex = SceneUtility.GetBuildIndexByScenePath(Path);
        }

        public void Draw()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.alignment = TextAnchor.MiddleLeft;
            
            if (BuildIndex != -1)
            {
                GUILayout.Label(BuildIndex.ToString(), GUILayout.Width(20));
            }
            else
            {
                GUILayout.Label(EditorGUIUtility.IconContent("Warning"), GUILayout.Width(20));
            }

            if (GUILayout.Button(EditorGUIUtility.IconContent("Scene"), GUILayout.Width(30)))
            {
                GoToScene(Path);
            }
            
            if (GUILayout.Button(Name, style))
            {
                EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(Path));
            }

            void GoToScene(string path)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(path);
                }
            }
        }
    }
}