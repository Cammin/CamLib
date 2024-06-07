using System;
using UnityEditor;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefInstanceFloat : EditorPrefInstance<float>
    {
        public EditorPrefInstanceFloat(string prefKey, string displayName, string icon) : base(prefKey, displayName, icon)
        {
        }

        public override float GetValue()
        {
            return EditorPrefs.GetFloat(Key, 0);
        }

        public override void SetValue(float value)
        {
            EditorPrefs.SetFloat(Key, value);
        }

        public override float DrawGUIObject()
        {
            return EditorGUILayout.FloatField(Content, Value);
        }
    }
}