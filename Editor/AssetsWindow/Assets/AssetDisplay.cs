using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public class AssetDisplay
    {
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
            GUILayout.Label("Assets", EditorStyles.boldLabel);
            foreach (var o in Items)
            {
                o.Draw();
            }
        }
    }
}