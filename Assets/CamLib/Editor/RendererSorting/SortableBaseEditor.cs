using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public static class SortableBaseEditor
    {
        public static void CheckIfChanged<T>(SortableBase<T> target) where T : Component
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (!GUILayout.Button("Update editor order"))
            {
                return;
            }
            
            target.UpdateOrder();
                
            Object o = target.DirtiedEditorObject;
            if (o == null)
            {
                return;
            }
                
            EditorUtility.SetDirty(o);
            Undo.RecordObject(o, "Changed Sorting Order");
        }
    }
}
