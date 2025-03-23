using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    /// <summary>
    /// A wonderful window that will let you tweak your save data manually so testing is easier.
    /// </summary>
    public abstract class DataPersistenceManagerEditor<T, TWindow> : UnityEditor.Editor 
        where T : GameData 
        where TWindow : DataPersistenceWindow<T>
    {
        public override void OnInspectorGUI()
        {
            DataPersistenceManager<T> manager = target as DataPersistenceManager<T>;
            
            if (GUILayout.Button("Open Profile Editor"))
            {
                DataPersistenceWindow<T>.GetWindow<TWindow>(manager, DataPersistenceEditorPrefs.TestSelectedProfileId);
            }
            
            EditorGUILayout.Space();
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (Application.isEditor)
            {
                foreach (var obj in manager.DataPersistenceGameObjects)
                {
                    EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                }
            }
            
        }
    }
}