using System;
using UnityEditor;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefInstanceBool : EditorPrefInstance<bool>
    {
        public override bool GetValue()
        {
            return EditorPrefs.GetBool(Key, false);
        }

        public override void SetValue(bool value)
        {
            EditorPrefs.SetBool(Key, value);
        }

        public override bool DrawGUIObject()
        {
            return EditorGUILayout.Toggle(Content, Value);
        }
    }
}