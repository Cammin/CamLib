using CamLib.DataTypes;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor.DataTypes
{
    [CustomPropertyDrawer(typeof(Bool2))]
    public class Bool2Editor : PropertyDrawer
    {
        private const float FIELD_SPACING = 35;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty xProp = property.FindPropertyRelative("x");
            SerializedProperty yProp = property.FindPropertyRelative("y");

            bool newX = xProp.boolValue;
            bool newY = yProp.boolValue;
            
            label = EditorGUI.BeginProperty(position, label, property);
            Rect fieldRect = EditorGUI.PrefixLabel(position, label);
            
            Rect xToggleRect = new Rect(fieldRect)
            {
                width = FIELD_SPACING
            };
            Rect yToggleRect = new Rect(fieldRect)
            {
                x = fieldRect.x + FIELD_SPACING,
                width = fieldRect.width - FIELD_SPACING
            };
            
            //EditorGUI.DrawRect(fieldRect, Color.red);
            //EditorGUI.DrawRect(yToggleRect, Colors.Purple);
            //EditorGUI.DrawRect(xToggleRect, Colors.Navy);

            int oldIndentLvl = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            EditorGUI.BeginChangeCheck();
            newX = EditorGUI.ToggleLeft(xToggleRect, "X", newX);
            if (EditorGUI.EndChangeCheck())
            {
                xProp.boolValue = newX;
            }
            
            EditorGUI.BeginChangeCheck();
            newY = EditorGUI.ToggleLeft(yToggleRect, "Y", newY);
            if (EditorGUI.EndChangeCheck())
            {
                yProp.boolValue = newY;
            }
            
            EditorGUI.indentLevel = oldIndentLvl;
            
            EditorGUI.EndProperty();
        }
    }
}
