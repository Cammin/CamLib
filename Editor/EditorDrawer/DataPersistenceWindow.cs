using System;
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
        public SerializedProperty _propManagerObj;
        public SerializedProperty _propId;

        public DataPersistenceManager<T> _managerPrefab;
        public SerializedObject _managerObj;
        public SerializedProperty _managerPropFileName;

        private GUIContent managerContent;
        
        private GUIContent contentPrefDisableDataPersistence;
        private GUIContent contentPrefInitializeDataIfNull;
        private GUIContent contentPrefLoadBeforeFirstSceneLoad;
        private GUIContent contentPrefTestSelectedProfileId;
        
        
        
        
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
            _propManagerObj = _serializedObject.FindProperty(nameof(_managerPrefab));
            _propSaveData = _serializedObject.FindProperty(nameof(_gameData));

            
             
            
            managerContent = new GUIContent("Manager Object", "The object that manages the data");

            contentPrefDisableDataPersistence = new GUIContent("Disable Data Persistence", EditorGUIUtility.IconContent("d_CacheServerDisabled").image, "Turns off saving/loading. Good if you just want to test the game unhindered by save data influence");
            contentPrefInitializeDataIfNull = new GUIContent("Initialize Data If Null", EditorGUIUtility.IconContent("d_CreateAddNew").image, "If no save data can be found, create a save file");
            contentPrefLoadBeforeFirstSceneLoad = new GUIContent("Load Before First Scene Load", EditorGUIUtility.IconContent("Loading").image, "Do this if you must. It's a bit of a hack to load data before the first scene loads");
            contentPrefTestSelectedProfileId = new GUIContent("Test Selected Profile Id", EditorGUIUtility.IconContent("d_DebuggerEnabled").image, "Forceably start with a specific save file. Good for trying to reproduce a bug from a specific state");
        }

        public void OnGUI()
        {
            _serializedObject.Update();

            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 190;
            DrawGui();
            EditorGUIUtility.labelWidth = labelWidth;
            _serializedObject.ApplyModifiedProperties();
        }

        private void DrawGui()
        {
            DataPersistenceEditorPrefs.DisableDataPersistence = EditorGUILayout.Toggle(contentPrefDisableDataPersistence, DataPersistenceEditorPrefs.DisableDataPersistence);
            DataPersistenceEditorPrefs.InitializeDataIfNull = EditorGUILayout.Toggle(contentPrefInitializeDataIfNull, DataPersistenceEditorPrefs.InitializeDataIfNull);
            DataPersistenceEditorPrefs.LoadBeforeFirstSceneLoad = EditorGUILayout.Toggle(contentPrefLoadBeforeFirstSceneLoad, DataPersistenceEditorPrefs.LoadBeforeFirstSceneLoad);
            DataPersistenceEditorPrefs.TestSelectedProfileId = EditorGUILayout.TextField(contentPrefTestSelectedProfileId, DataPersistenceEditorPrefs.TestSelectedProfileId);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_propManagerObj, managerContent);
            if (_propManagerObj.objectReferenceValue == null)
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


            EditorGUILayout.PropertyField(_propId);
            if (string.IsNullOrEmpty(_propId.stringValue))
            {
                EditorGUILayout.HelpBox("Set a profile", MessageType.Warning);

                string[] dirs = Directory.GetDirectories(Application.persistentDataPath);
                foreach (string s in dirs)
                {
                    EditorGUILayout.LabelField(s, EditorStyles.miniLabel);
                }

                return;
            }

            string dir = Path.Combine(Application.persistentDataPath, _propId.stringValue);
            string path = Path.Combine(dir, _managerPropFileName.stringValue);
            EditorGUILayout.LabelField(path, EditorStyles.miniLabel);

            bool dirExists = Directory.Exists(dir);
            
            
            bool fileExists = File.Exists(path);
            
            
            GUILayout.BeginHorizontal();
            
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
            
            if (GUILayout.Button("Clear"))
            {
                _gameData = (T)Activator.CreateInstance(typeof(T));
                _gameData.OnConstruct();
            }
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Open Save Path"))
            {
                Application.OpenURL(Application.persistentDataPath);
            }
            
            GUILayout.EndHorizontal();

            if (!dirExists)
            {
                EditorGUILayout.HelpBox("Directory does not yet exist; Can save but cannot load", MessageType.Info);
            }
            
            if (dirExists && !fileExists)
            {
                EditorGUILayout.HelpBox("File does not exist; Can save but cannot load. Check the folder's contents and ensure nothing is corrupted", MessageType.Warning);
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