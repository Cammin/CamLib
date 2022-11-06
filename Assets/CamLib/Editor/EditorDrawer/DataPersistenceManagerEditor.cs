using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public abstract class DataPersistenceManagerEditor<T, TWindow> : UnityEditor.Editor where T : GameData where TWindow : DataPersistenceWindow<T>
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Profile Editor"))
            {
                DataPersistenceWindow<T>.CreateWindow<TWindow>(target as DataPersistenceManager<T>);
            }
            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}