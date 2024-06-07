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
        
        public void Initialize()
        {
            Items.Clear();
            
            //todo make this extendable
            //AddBool(EditorPrefStartBootstrap.KEY, "Start Bootstrap", "d_VideoPlayer Icon");
            //AddBool(EditorPrefBypassStartup.KEY, "Bypass to Lobby", "d_NetworkLobbyManager Icon");
            //AddInt(EditorPrefStartCount.KEY, "Players to Start", "d_NetworkManager Icon");
        }
        
        public void AddBool(string prefKey, string displayName, string icon)
        {
            EditorPrefInstanceBool prefBool = new EditorPrefInstanceBool();
            prefBool.Initialize(prefKey, displayName, icon);
            Items.Add(prefBool);
        }
        public void AddInt(string prefKey, string displayName, string icon)
        {
            EditorPrefInstanceInt prefInt = new EditorPrefInstanceInt();
            prefInt.Initialize(prefKey, displayName, icon);
            Items.Add(prefInt);
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