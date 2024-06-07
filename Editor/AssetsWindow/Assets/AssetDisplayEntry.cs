using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CamLib.Editor
{
    [Serializable]
    public class AssetDisplayEntry
    {
        public string AssetPath;
        public Object Obj;
        public GUIContent Content;
        public Texture Preview;
        public Texture Mini;
        public Texture Image;

        private GUIStyle IconStyle;
        private GUIStyle TextStyle;
        
        public AssetDisplayEntry(string path)
        {
            AssetPath = path;
            Obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            Debug.Assert(Obj, $"Obj null at {path}");
            Content = new GUIContent()
            {
                text = Obj.name,
            };
            
            TryLoad();
        }

        private void TryLoad()
        {
            if (Mini == null)
            {
                Mini = AssetPreview.GetMiniThumbnail(Obj);
            }
            
            if (Preview == null)
            {
                Preview = AssetPreview.GetAssetPreview(Obj);
            }

            Image = Preview ? Preview : Mini;
        }

        public void Draw()
        {
            TryLoad();
            
            TextStyle = new GUIStyle(GUI.skin.button);
            TextStyle.alignment = TextAnchor.MiddleLeft;
            IconStyle = new GUIStyle(GUI.skin.button);
            
            ZeroOffet(IconStyle.padding);
            void ZeroOffet(RectOffset rect)
            {
                rect.top = 1;
                rect.bottom = 1;
                rect.left = 1;
                rect.right = 1;
            }

            float height = 50;
            
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button(Image, IconStyle, GUILayout.Height(height), GUILayout.Width(height)))
                {
                    EditorGUIUtility.PingObject(Obj);
                    Selection.activeObject = Obj;
                }
            
                if (GUILayout.Button(Content, TextStyle, GUILayout.Height(height)))
                {
                    AssetDatabase.OpenAsset(Obj);
                }
            }
        }
    }
}