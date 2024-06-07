using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public class SceneCategory
    {
        public string Title;
        public List<SceneCategoryEntry> MetaData = new List<SceneCategoryEntry>();
            
        public void Sort()
        {
            MetaData = MetaData.OrderBy(data =>
            {
                if (data.Name == "Bootstrap") return "_a";
                if (data.Name == "MainMenu") return "_b";
                if (data.Name == "Lobby") return "_c";
                if (data.Name == "Gameplay") return "_d";
                return data.Name;
            }).ToList();
        }

        public void DrawCollection()
        {
            GUILayout.Label(Title, EditorStyles.largeLabel);

            foreach (SceneCategoryEntry meta in MetaData)
            {
                using (new GUILayout.HorizontalScope())
                {
                    meta.Draw();
                }
            }
        }

        public void AddPath(string assetPath)
        {
            MetaData.Add(new SceneCategoryEntry(assetPath));
        }
    }
}