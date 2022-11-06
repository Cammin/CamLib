using UnityEditor;
using UnityEngine;

namespace CamLib.Editor.AudioSystems
{
    [CustomEditor(typeof(SfxAsset), true)]
    public class SoundAssetEditor : UnityEditor.Editor
    {

        [SerializeField] private AudioSource _previewer;

        public void OnEnable()
        {
            _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        public void OnDisable()
        {
            DestroyImmediate(_previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((SfxAsset)target).Play(_previewer);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}