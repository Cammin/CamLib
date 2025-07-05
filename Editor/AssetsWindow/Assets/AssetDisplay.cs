using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public class AssetDisplay
    {
        internal const string PrefSize = "CamLib.AssetDisplayEntrySize";
        
        public List<AssetDisplayEntry> Items = new List<AssetDisplayEntry>();
        
        public void Initialize(CentralizedAssetWindowImplementation impl)
        {
            Items.Clear();

            foreach (string path in impl.AssetDisplayPaths)
            {
                Items.Add(new AssetDisplayEntry(path));
            }
        }
        
        public void OnGUI()
        {
            if (Items.Count <= 0) return;
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Assets", EditorStyles.boldLabel);
            GUILayout.Space(15);
            
            float prefValue = EditorPrefs.GetFloat(PrefSize, 50);
            float newPrefValue = GUILayout.HorizontalSlider(prefValue, 18, 100, GUILayout.MaxWidth(100));
            GUILayout.FlexibleSpace();
            
            if (prefValue != newPrefValue)
            {
                EditorPrefs.SetFloat(PrefSize, newPrefValue);
            }
            
            GUILayout.EndHorizontal();
            
            foreach (var o in Items)
            {
                o.Draw(prefValue);
            }
        }
    }
}