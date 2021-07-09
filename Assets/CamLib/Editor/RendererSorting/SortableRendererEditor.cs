using UnityEditor;

namespace CamLib.Editor
{
    [CustomEditor(typeof(SortableRenderer))]
    public class SortableRendererEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SortableBaseEditor.CheckIfChanged((SortableRenderer)target);
            DrawDefaultInspector();
        }
    }
}

