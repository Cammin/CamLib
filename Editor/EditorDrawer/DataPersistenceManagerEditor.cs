using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    /// <summary>
    /// A wonderful window that will let you tweak your save data manually so testing is easier.
    /// </summary>
    public abstract class DataPersistenceManagerEditor<T, TWindow> : UnityEditor.Editor where T : GameData where TWindow : DataPersistenceWindow<T>
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty prop = serializedObject.FindProperty("_dataPersistenceObjects");

            if (GUILayout.Button("Open Profile Editor"))
            {
                DataPersistenceWindow<T>.CreateWindow<TWindow>(target as DataPersistenceManager<T>);
            }
            EditorGUILayout.Space();
            
            //todo add so that the override bool for id is only shown when true
            
            base.OnInspectorGUI();
        }
    }
}