using CamLib.RendererSorting;
using UnityEditor;

namespace CamLib.Editor
{
    [CustomEditor(typeof(SortableSortingGroup))]
    public class SortableSortingGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SortableBaseEditor.CheckIfChanged((SortableSortingGroup)target);
            DrawDefaultInspector();
        }
    }
}

