using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomEditor(typeof(EditableBoundsTool))]
    public class EditableBoundsManagerEditor : UnityEditor.Editor
    {
        private static ISettableBounds _inspectedObject = null;
        private static BoxBoundsHandle _handle;

        public override void OnInspectorGUI()
        {
            GUILayout.Label(_inspectedObject == null
                ? $"Make sure that this component in the same object as a component inheriting '{nameof(ISettableBounds)}'."
                : $"Captured: {_inspectedObject}");

            SerializedProperty drawBounds = serializedObject.FindProperty("_drawBounds");
            SerializedProperty drawBoundsColor = serializedObject.FindProperty("_drawBoundsColor");
            
            EditorGUI.BeginChangeCheck();
            drawBounds.boolValue = EditorGUILayout.Toggle("Draw Bounds", drawBounds.boolValue);
            
            if (drawBounds.boolValue)
            {
                drawBoundsColor.colorValue = EditorGUILayout.ColorField("Bounds Color", drawBoundsColor.colorValue);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    
        private void OnSceneGUI()
        {
            _inspectedObject = ((EditableBoundsTool)target).GetComponent<ISettableBounds>();

            if (_inspectedObject == null)
            {
                return;
            }

            if (_handle == null)
            {
                _handle = new BoxBoundsHandle();
            }

            _handle.center = _inspectedObject.Bounds.center;
            _handle.size = _inspectedObject.Bounds.size;
            
            EditorGUI.BeginChangeCheck();
            {
                _handle.DrawHandle();
            }
            if (EditorGUI.EndChangeCheck())
            {
                Bounds newBounds = new Bounds(_handle.center, _handle.size);
                _inspectedObject.SetBounds(newBounds);
                RecordChange();
            }
            
        }

        private static void RecordChange()
        {
            Undo.RecordObject(_inspectedObject.DirtiedObject, "Change Bounds");
            EditorUtility.SetDirty(_inspectedObject.DirtiedObject);
        }
        
        
        [DrawGizmo(GizmoType.NonSelected)]
        private static void RenderBounds(EditableBoundsTool boundsTool, GizmoType gizmoType)
        {
            if (_inspectedObject == null)
            {
                _inspectedObject = boundsTool.GetComponent<ISettableBounds>();
            }
            if (_inspectedObject == null) return;
            
            
            if (!boundsTool.DrawBounds) return;
            
            Gizmos.color = boundsTool.DrawBoundsColor;
            Gizmos.DrawWireCube(_inspectedObject.Bounds.center, _inspectedObject.Bounds.size);
        }
    }
    

}
