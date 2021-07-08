using CamLib.RendererSorting;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomEditor(typeof(SortableParticleSystem))]
    public class SortableParticleSystemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SortableBaseEditor.CheckIfChanged((SortableParticleSystem)target);
            DrawDefaultInspector();
        }
    }
}

