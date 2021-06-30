using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public static class EditorGUIUtil
    {
        public static Texture GetUnityIcon(string name, string ending = " Icon")
        {
            string tilemapIcon = EditorGUIUtility.isProSkin ? $"d_{name}{ending}" : $"{name}{ending}";
            return EditorGUIUtility.IconContent(tilemapIcon).image;
        }
        
        public static float LabelWidth(float controlRectWidth)
        {
            const float divisor = 2.24f;
            const float offset = -33;
            float totalWidth = controlRectWidth + EditorGUIUtility.singleLineHeight;
            return Mathf.Max(totalWidth / divisor + offset, EditorGUIUtility.labelWidth);
        }
        
        public static void DrawDivider()
        {
            const float space = 2;
            const float height = 2f;
            
            GUILayout.Space(space);
            
            Rect area = GUILayoutUtility.GetRect(0, height);
            area.xMin -= 15;
            
            float colorIntensity = EditorGUIUtility.isProSkin ? 0.1f : 0.5f;
            Color areaColor = new Color(colorIntensity, colorIntensity, colorIntensity, 1);
            EditorGUI.DrawRect(area, areaColor);
            
            GUILayout.Space(space);
        }
    }
}