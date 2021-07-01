using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadOnlyAttribute att = (ReadOnlyAttribute)attribute;

            bool readOnly = att.Status == ReadOnlyStatus.Both || 
                            att.Status == ReadOnlyStatus.PlayMode && EditorApplication.isPlayingOrWillChangePlaymode ||
                            att.Status == ReadOnlyStatus.Editor && !EditorApplication.isPlayingOrWillChangePlaymode;

            bool prev = GUI.enabled;
            if (readOnly)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label, true);

            if (readOnly)
            {
                GUI.enabled = prev;
            }
        }
    }
}
