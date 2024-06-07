using System;
using UnityEditor;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefInstanceInt : EditorPrefInstance<int>
    {
        public EditorPrefInstanceInt(string prefKey, string displayName, string icon) : base(prefKey, displayName, icon)
        {
        }
        
        public override int GetValue()
        {
            return EditorPrefs.GetInt(Key, 0);
        }

        public override void SetValue(int value)
        {
            EditorPrefs.SetInt(Key, value);
        }

        public override int DrawGUIObject()
        {
            return EditorGUILayout.IntField(Content, Value);
        }
    }
}