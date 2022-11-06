using System.IO;
using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public class SaveDataWindow : EditorWindow
    {
        public GameData _gameData = new GameData();
        public string _profileId;

        public SerializedObject _serializedObject;
        private SerializedProperty _propSaveData;
        public SerializedProperty _propManagerPrefab;
        private SerializedProperty _propId;

        public DataPersistenceManager _managerPrefab;
        public SerializedObject _managerObj;
        public SerializedProperty _managerPropFileName;

        //not doing a menuitem, but enable if this is preferred
        //[MenuItem("Tools/SaveDataWindow")]
        public static void CreateWindow(DataPersistenceManager ctx = null)
        {
            SaveDataWindow saveDataWindow = GetWindow<SaveDataWindow>();
            saveDataWindow.titleContent = new GUIContent()
            {
                text = "Save Data",
                image = EditorGUIUtility.IconContent("d_SaveAs").image
            };
            saveDataWindow._managerPrefab = ctx;
        }
        
        public void OnGUI()
        { 
            _serializedObject = new SerializedObject(this);
            if (_serializedObject == null)
            {
            }
            _propId = _serializedObject.FindProperty(nameof(_profileId));
            _propManagerPrefab = _serializedObject.FindProperty(nameof(_managerPrefab));
            _propSaveData = _serializedObject.FindProperty(nameof(_gameData));
            
            if (GUILayout.Button("Open Save Path"))
            {
                Application.OpenURL(Application.persistentDataPath);
            }
            
            _serializedObject.Update();

            EditorGUILayout.PropertyField(_propManagerPrefab);
            if (_propManagerPrefab.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Assign the prefab to begin", MessageType.Warning);
                _serializedObject.ApplyModifiedProperties();
                return;
            }

            if (_managerObj == null)
            {
                _managerObj = new SerializedObject(_managerPrefab);
                _managerPropFileName = _managerObj.FindProperty("_fileName");
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_propId);

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
                _managerPrefab.InitializeFormatter();
                _managerPrefab.SaveGame(_profileId, _gameData);
            }

            using (new EditorGUI.DisabledScope(!fileExists || !dirExists))
            {
                if (GUILayout.Button("Load"))
                {
                    _managerPrefab.InitializeFormatter();
                    _gameData = _managerPrefab.LoadGame(_profileId);
                }
            }

            if (_propSaveData == null)
            {
                EditorGUILayout.HelpBox("Save Data property appears to be null. Ensure your data is serializable", MessageType.Error);
                _serializedObject.ApplyModifiedProperties();
                return;
            }
            
            GUILayout.BeginVertical(EditorStyles.helpBox);
            scroll = GUILayout.BeginScrollView(scroll);
            EditorGUILayout.PropertyField(_propSaveData);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            

            _serializedObject.ApplyModifiedProperties();
        }

        private Vector2 scroll;
    }
}