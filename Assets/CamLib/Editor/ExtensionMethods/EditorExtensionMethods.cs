using UnityEditor;
using UnityEngine;

namespace CamLib.Editor.ExtensionMethods
{
    public static class EditorExtensionMethods
    {
        public static SerializedProperty DrawField(this SerializedObject obj, string propName)
        {
            return DrawField(obj, EditorGUILayout.GetControlRect(), propName, null);
        }
        public static SerializedProperty DrawField(this SerializedProperty prop, string propName)
        {
            return DrawField(prop, EditorGUILayout.GetControlRect(), propName, null);
        }        
        public static SerializedProperty DrawField(this SerializedObject obj, Rect rect, string propName)
        {
            return DrawField(obj, rect, propName, null);
        }
        public static SerializedProperty DrawField(this SerializedProperty prop, Rect rect, string propName)
        {
            return DrawField(prop, rect, propName, null);
        }
        public static SerializedProperty DrawField(this SerializedObject obj, Rect rect, string propName, GUIContent content)
        {
            SerializedProperty prop = obj.FindProperty(propName);
            EditorGUI.PropertyField(rect, prop, content);
            return prop;
        }
        public static SerializedProperty DrawField(this SerializedProperty prop, Rect rect, string propName, GUIContent content)
        {
            SerializedProperty relProp = prop.FindPropertyRelative(propName);
            EditorGUI.PropertyField(rect, relProp, content);
            return prop;
        }
    }
}