using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public abstract class EditorPrefInstance<T> : IEditorPrefInstance
    {
        public string Key;
        protected T Value;
        protected GUIContent Content;
        
        public void Initialize(string prefKey, string displayName, string icon)
        {
            Key = prefKey;

            Texture image = EditorGUIUtility.IconContent(icon, displayName).image;
            Content = new GUIContent(displayName, image, prefKey);
            Value = GetValue();
        }

        public void DrawField()
        {
            if (Content == null)
            {
                return;
            }
            
            using EditorGUIUtility.IconSizeScope iconSizeScope = new(Vector2.one * 16);

            T value = DrawGUIObject();
            if (!Value.Equals(value))
            {
                Value = value;
                SetValue(value);
            }
        }

        public abstract T GetValue();
        public abstract void SetValue(T value);
        public abstract T DrawGUIObject();
        
    }
}