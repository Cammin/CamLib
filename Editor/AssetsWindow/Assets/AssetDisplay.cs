using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public class AssetDisplay
    {
        public List<AssetDisplayEntry> Items = new List<AssetDisplayEntry>();
        
        public void Initialize()
        {
            Items.Clear();
            
            //todo make this extendable
            //Add("Assets/_Game/Prefabs/Player.prefab");
            //Add("Assets/_Game/Prefabs/Ride.prefab");
            
            //Add("Assets/_Game/Prefabs/GameManager.prefab");
            //Add("Assets/_Game/Prefabs/NetworkManager.prefab");
            //Add("Assets/_Game/Scripts/Game.asmdef");
        }

        public void Add(string path)
        {
            Items.Add(new AssetDisplayEntry(path));
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