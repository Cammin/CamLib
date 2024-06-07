using System;
using UnityEditor;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefInstanceString : EditorPrefInstance<string>
    {
        public EditorPrefInstanceString(string prefKey, string displayName, string icon) : base(prefKey, displayName, icon)
        {
        }
        
        public override string GetValue()
        {
            return EditorPrefs.GetString(Key, string.Empty);
        }

        public override void SetValue(string value)
        {
            EditorPrefs.SetString(Key, value);
        }

        public override string DrawGUIObject()
        {
            return EditorGUILayout.TextField(Content, Value);
        }
    }
}