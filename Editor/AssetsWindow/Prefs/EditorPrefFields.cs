using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefFields
    {
        public List<IEditorPrefInstance> Items = new List<IEditorPrefInstance>();
        
        public void Initialize(CentralizedAssetWindowImplementation impl)
        {
            Items.Clear();
            Items.AddRange(impl.Prefs);
        }
        
        public void OnGUI()
        {
            GUILayout.Label("Prefs", EditorStyles.boldLabel);
            foreach (IEditorPrefInstance o in Items)
            {
                o.DrawField();
            }
        }
    }
}