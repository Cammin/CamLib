using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    /// <summary>
    /// Simple attribute to add play and stop buttons to AudioClip fields.
    /// </summary>
    [CustomPropertyDrawer(typeof(AudioClipButtonsAttribute))]
    public class AudioClipButtonsAttributeDrawer : PropertyDrawer
    {
        private const float PLAY_BUTTON_WIDTH = 30;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.type.Contains(nameof(AudioClip)))
            {
                GUIContent error = new GUIContent()
                {
                    text = label.text,
                    tooltip = $"Invalid usage of {nameof(AudioClipButtonsAttribute)}.\nOnly use on {nameof(AudioClip)}",
                    image = EditorGUIUtil.GetUnityIcon("console.erroricon.sml", "")
                };
                
                EditorGUI.LabelField(position, error);
                return;
            }
            
            
            
            // assigning selection change may break the editor
            Selection.selectionChanged = OnLostObjectFocus;

            float labelWidth = EditorGUIUtility.labelWidth + 2;
            float fieldWidth = EditorGUIUtility.fieldWidth;
            int indentWidth = EditorGUI.indentLevel * 15;

            EditorGUI.BeginProperty(position, label, property);

            if (property.hasMultipleDifferentValues == false)
            {
                AudioClip clip = property.objectReferenceValue as AudioClip;
                float buttonWidth = (clip != null) ? PLAY_BUTTON_WIDTH : 0;

                //Label
                Rect labelRect = new Rect(
                    position.x + indentWidth,
                    position.y,
                    labelWidth - indentWidth,
                    position.height);

                //AudioClip
                Rect fieldRect = new Rect(
                    position.x + labelWidth - indentWidth,
                    position.y,
                    Mathf.Max(fieldWidth, position.width - labelWidth - (buttonWidth * 2) + indentWidth),
                    position.height);

                //Play
                Rect playButtonRect = new Rect(
                    position.x + position.width - (buttonWidth * 2) + 1,
                    position.y-1,
                    PLAY_BUTTON_WIDTH - 1,
                    position.height+2);

                //Stop
                Rect stopButtonRect = new Rect(
                    position.x + position.width - buttonWidth + 1,
                    position.y-1,
                    PLAY_BUTTON_WIDTH - 1,
                    position.height+2);


                //void DrawRects()
                //{
                //    float alpha = 0.5f;
                //    EditorGUI.DrawRect(position, Color.gray.Alpha(alpha));
                //    EditorGUI.DrawRect(labelRect, Color.red.Alpha(alpha));
                //    EditorGUI.DrawRect(fieldRect, Color.yellow.Alpha(alpha));
                //    EditorGUI.DrawRect(playButtonRect, Color.green.Alpha(alpha));
                //    EditorGUI.DrawRect(stopButtonRect, Color.blue.Alpha(alpha));
                //}

                //label
                GUI.Label(labelRect, property.displayName);


                //object field
                property.objectReferenceValue = EditorGUI.ObjectField(fieldRect, clip, typeof(AudioClip), false);

                //buttons
                if (clip != null)
                {
                    PlayClipButton();
                    StopClipButton();
                }

                void PlayClipButton()
                {
                    GUIContent playContent = new GUIContent()
                    {
                        image = EditorGUIUtil.GetUnityIcon("PlayButton", ""),
                        tooltip = "Play"
                    };


                    if (GUI.Button(playButtonRect, playContent))
                    {
                        //restart the already playing clip.
                        if (InternalEditorFunctions.IsPreviewClipPlaying(clip))
                        {
                            InternalEditorFunctions.StopAllPreviewClips();
                        }

                        //play it.
                        InternalEditorFunctions.PlayPreviewClip(clip);
                    }
                }

                void StopClipButton()
                {
                    GUIContent stopContent = new GUIContent()
                    {
                        image = EditorGUIUtil.GetUnityIcon("PreMatQuad", ""),
                        tooltip = "Stop"
                    };
                    
                    if (InternalEditorFunctions.IsPreviewClipPlaying(clip))
                    {
                        if (GUI.Button(stopButtonRect, stopContent))
                        {
                            InternalEditorFunctions.StopAllPreviewClips();
                        }
                    }
                    else
                    {
                        bool prev = GUI.enabled;
                        GUI.enabled = false;
                        GUI.Button(stopButtonRect, stopContent);
                        GUI.enabled = prev;
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        private static void OnLostObjectFocus()
        {
            Selection.selectionChanged = null;
            InternalEditorFunctions.StopAllPreviewClips();
        }
    }
}