using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomEditor(typeof(DataPersistenceManager))]
    public class DataPersistenceManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Profile Editor"))
            {
                SaveDataWindow.CreateWindow(target as DataPersistenceManager);
            }
            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}