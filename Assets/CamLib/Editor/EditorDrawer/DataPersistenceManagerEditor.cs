using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public abstract class DataPersistenceManagerEditor<T, TWindow> : UnityEditor.Editor where T : GameDataBase where TWindow : SaveDataWindow<T>
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Profile Editor"))
            {
                SaveDataWindow<T>.CreateWindow<TWindow>(target as DataPersistenceManager<T>);
            }
            EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
}