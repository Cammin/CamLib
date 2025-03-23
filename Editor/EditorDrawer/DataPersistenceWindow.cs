using System.IO;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    /// <summary>
    /// Derive this so you can use it with your GameData-derived class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataPersistenceWindow<T> : EditorWindow where T : GameData
    {
        public T _gameData;
        public string _profileId;

        public SerializedObject _serializedObject;
        public SerializedProperty _propSaveData;
        public SerializedProperty _propManagerPrefab;
        public SerializedProperty _propId;

        public DataPersistenceManager<T> _managerPrefab;
        public SerializedObject _managerObj;
        public SerializedProperty _managerPropFileName;
        
        private Vector2 scroll;
        
        public static void GetWindow<TWindow>(DataPersistenceManager<T> ctx = null, string initialProfileId = null) where TWindow : DataPersistenceWindow<T>
        {
            TWindow saveDataWindow = EditorWindow.GetWindow<TWindow>();
            saveDataWindow.titleContent = new GUIContent()
            {
                text = "Save Data",
                image = EditorGUIUtility.IconContent("d_SaveAs").image
            };
            saveDataWindow._managerPrefab = ctx;
            saveDataWindow._profileId = initialProfileId;
        }

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(this);
            _propId = _serializedObject.FindProperty(nameof(_profileId));
            _propManagerPrefab = _serializedObject.FindProperty(nameof(_managerPrefab));
            _propSaveData = _serializedObject.FindProperty(nameof(_gameData));
        }

        public void OnGUI()
        {
            _serializedObject.Update();
            DrawGui();
            _serializedObject.ApplyModifiedProperties();
        }

        private void DrawGui()
        {
            if (GUILayout.Button("Open Save Path"))
            {
                Application.OpenURL(Application.persistentDataPath);
            }
            

            EditorGUILayout.PropertyField(_propManagerPrefab);
            if (_propManagerPrefab.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Assign the instance/prefab to begin", MessageType.Warning);
                return;
            }

            if (_managerObj == null)
            {
                _managerObj = new SerializedObject(_managerPrefab);
                _managerPropFileName = _managerObj.FindProperty("_fileName");
            }
            _managerObj.Update();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_propId);
            if (string.IsNullOrEmpty(_propId.stringValue))
            {
                EditorGUILayout.HelpBox("Set a profile", MessageType.Warning);
                return;
            }

            string dir = Path.Combine(Application.persistentDataPath, _propId.stringValue);
            string path = Path.Combine(dir, _managerPropFileName.stringValue);
            EditorGUILayout.LabelField(path, EditorStyles.miniLabel);

            bool dirExists = Directory.Exists(dir);
            if (!dirExists)
            {
                EditorGUILayout.HelpBox("Directory does not yet exist; Can save but cannot load", MessageType.Info);
            }
            
            bool fileExists = File.Exists(path);
            if (dirExists && !fileExists)
            {
                EditorGUILayout.HelpBox("File does not exist; Can save but cannot load. Check the folder's contents and ensure nothing is corrupted", MessageType.Warning);
            }
            
            if (GUILayout.Button("Save"))
            {
                _managerPrefab.InitializeDataHandler();
                _managerPrefab.SaveGame(_profileId, _gameData);
            }

            using (new EditorGUI.DisabledScope(!fileExists || !dirExists))
            {
                if (GUILayout.Button("Load"))
                {
                    _managerPrefab.InitializeDataHandler();
                    _gameData = _managerPrefab.LoadGame(_profileId);
                }
                
                if (GUILayout.Button("Delete"))
                {
                    _managerPrefab.InitializeDataHandler();
                    _managerPrefab.ClearProfileData(_profileId);
                }
            }

            if (_propSaveData == null)
            {
                EditorGUILayout.HelpBox("Save Data property appears to be null. Ensure your data is serializable", MessageType.Error);
                return;
            }
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            scroll = GUILayout.BeginScrollView(scroll);
            EditorGUILayout.PropertyField(_propSaveData);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }
    }
}