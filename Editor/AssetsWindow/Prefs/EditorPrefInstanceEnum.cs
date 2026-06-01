using System;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [Serializable]
    public class EditorPrefInstanceEnum<T> : EditorPrefInstance<T> where T : Enum
    {
        public EditorPrefInstanceEnum(string prefKey, string displayName, string icon) : base(prefKey, displayName, icon)
        {
        }

        public override T GetValue()
        {
            return (T)(object)EditorPrefs.GetInt(Key, 0);
        }

        public override void SetValue(T value)
        {
            EditorPrefs.SetInt(Key, Convert.ToInt32(value));
        }

        public override T DrawGUIObject()
        {
            return (T)EditorGUILayout.EnumPopup(Content, Value);
        }
    }
}