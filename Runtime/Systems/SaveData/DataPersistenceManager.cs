using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CamLib
{
    /// <summary>
    /// Derive an empty class from this where T is the data class you want to serialize.
    /// Add this component to your scene to manage save data.
    /// Based on the save system from Trevor Mock https://github.com/shapedbyrainstudios/save-load-system
    /// </summary>
    public abstract class DataPersistenceManager<T> : MonoBehaviour where T : GameData
    {
        [SerializeField] private string _fileName = "save.data";
        [SerializeField] private bool _useEncryption;
        [Tooltip("Leave as 0 to disable")]
        [SerializeField] private float _autoSaveTimeSeconds = 0f;

        /// <summary>
        /// Our active hot save data. it will eventually read/write with a specified profile
        /// </summary>
        private T _activeData;
        
        /// <summary>
        /// The objects in the active scene that will interact with the save data 
        /// </summary>
        internal List<IDataPersistence<T>> DataPersistenceObjects = new List<IDataPersistence<T>>();
        internal List<GameObject> DataPersistenceGameObjects = new List<GameObject>();
        
        /// <summary>
        /// Object that saves/loads the data to/from a file
        /// </summary>
        private FileDataHandler<T> _dataHandler;
        
        /// <summary>
        /// Current profile. Talks to the file reader/writer
        /// </summary>
        private string _selectedProfileId = "";
        
        private Coroutine _autoSaveCoroutine;
        
        public static DataPersistenceManager<T> Instance { get; private set; }

        public int PersistentObjectCount => DataPersistenceObjects.Count;
        public bool HasGameData => _activeData != null;

#if  UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatics()
        {
            Instance = null;
        }
#endif

        private void Awake() 
        {
            if (Instance != null) 
            {
                Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (DataPersistenceEditorPrefs.DisableDataPersistence) 
            {
                Debug.Log("Data Persistence is disabled");
                return;
            }

            InitializeDataHandler();
            InitializeSelectedProfileId();
        }

        [PublicAPI]
        public void InitializeDataHandler()
        {
            _dataHandler = new FileDataHandler<T>(Application.persistentDataPath, _fileName, _useEncryption);
        }

        private void OnEnable() 
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() 
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Start()
        {
            if (DataPersistenceEditorPrefs.LoadBeforeFirstSceneLoad)
            {
                InitializeSaveSystem();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => InitializeSaveSystem();
        protected virtual void InitializeSaveSystem()
        {
            DataPersistenceObjects = FindAllPersistenceObjectsInScene();
            LoadGame();
            TryAutoSave();
        }
        
        public void SetProfile(string newProfileId) 
        {
            _selectedProfileId = newProfileId;
            LoadGame();
        }
        
        public void ClearProfileData(string profileId) 
        {
            _dataHandler.Delete(profileId);

            //reload anything possible after having deleted data
            if (Application.isPlaying)
            {
                InitializeSelectedProfileId();
                LoadGame();
            }
        }

        private void InitializeSelectedProfileId() 
        {
            string testProfile = DataPersistenceEditorPrefs.TestSelectedProfileId;
            if (!string.IsNullOrEmpty(testProfile))
            {
                _selectedProfileId = testProfile;
                Debug.LogWarning($"Loading TEST profile \"{testProfile}\"");
            }
            else
            {
                _selectedProfileId = _dataHandler.FindMostRecentlyUpdatedProfileId();
            }
        }

        [PublicAPI]
        public void NewGame()
        {
            _activeData = (T)Activator.CreateInstance(typeof(T));
            _activeData.OnConstruct();
        }

        /// <summary>
        /// Load the game, which will use that profile.
        /// If no profile exists, will create a new one on it's own.
        /// </summary>
        [PublicAPI]
        public T LoadGame(string profileId = null)
        {
            profileId ??= _selectedProfileId;

            if (Application.isPlaying && DataPersistenceEditorPrefs.DisableDataPersistence)
            {
                Debug.Log($"Tried loading save \"{profileId}\" but was disabled for editor");
                return null;
            }

            // load any saved data from a file using the data handler
            _activeData = _dataHandler.Load(profileId);

            #if UNITY_EDITOR
            // start a new game if the data is null and we're configured to initialize data for debugging purposes
            if (_activeData == null && DataPersistenceEditorPrefs.InitializeDataIfNull) 
            {
                Debug.Log($"Save data: Initialized debug save file for \"{profileId}\"");
                NewGame();
            }
            #endif

            // if no data can be loaded, don't continue
            if (_activeData == null) 
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be loaded.");
                return null;
            }

            TryMigrateVersion();
            
            if (Application.isPlaying)
            {
                OnBeforePersistentObjectsLoadData(_activeData);
                
                // push the loaded data to all other scripts that need it
                foreach (IDataPersistence<T> dataPersistenceObj in DataPersistenceObjects)
                {
                    dataPersistenceObj?.LoadData(_activeData);
                }
            }

            return _activeData;
        }

        /// <summary>
        /// When the save data is loaded, but before the data is applied for all persistence objects. Use if you want to authorize something beforehand like level information
        /// </summary>
        protected virtual void OnBeforePersistentObjectsLoadData(T data)
        {
        }

        /// <summary>
        /// If there are save-related bugs like broken unique ids, this can be used to repair it. It wil also make the save data changed to the new one
        /// </summary>
        protected virtual void TryMigrateVersion()
        {
            string lastVersionString = _activeData.BuildVersion;
            string currentVersionString = Application.version;
            
            try
            {
                Version lastVersion = new Version(lastVersionString);
                Version currentVersion = new Version(currentVersionString);

                if (lastVersion != currentVersion)
                {
                    Debug.LogWarning($"Migrate Save data! from {lastVersion} to {currentVersion}");
                    _activeData.MigrateVersion(lastVersion, currentVersion);
                    _activeData.BuildVersion = Application.version;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed migration attempt: from \"{lastVersionString}\" to \"{currentVersionString}\":\n{e}");
            }
        }

        /// <summary>
        /// Call this before you load a new scene so that persistence objects properly write their state before they are destroyed!
        /// This 
        /// </summary>
        [PublicAPI]
        public void SaveGame(string profileId = null, T overrideData = null)
        {
            profileId ??= _selectedProfileId;
            if (overrideData != null)
            {
                _activeData = overrideData;
            }
            
            // return right away if data persistence is disabled
            if (Application.isPlaying && DataPersistenceEditorPrefs.DisableDataPersistence) 
            {
                Debug.Log($"Tried saving save \"{profileId}\" but was disabled for editor");
                return;
            }

            if (_activeData == null) 
            {
                Debug.LogWarning($"Tried to save NULL data to \"{profileId}\", cancelled.");
                return;
            }

            if (Application.isPlaying)
            {
                // pass the data to other scripts so they can update it
                foreach (IDataPersistence<T> dataPersistenceObj in DataPersistenceObjects) 
                {
                    dataPersistenceObj?.SaveData(_activeData);
                }
            }
            
            _activeData.LastUpdated = System.DateTime.Now.ToBinary();

            //only force a version naturally if it's not artificially overridden 
            if (overrideData == null)
            {
                _activeData.BuildVersion = Application.version;
            }
            
            _dataHandler.Save(_activeData, profileId);
        }

        private void OnApplicationQuit() 
        {
            SaveGame();
        }

        private List<IDataPersistence<T>> FindAllPersistenceObjectsInScene() 
        {
            var monos = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var dataPersistenceObjects = new List<IDataPersistence<T>>();
    
            foreach (var mono in monos)
            {
                if (mono is IDataPersistence<T> dataPersistenceObj)
                {
                    DataPersistenceGameObjects.Add(mono.gameObject);
                    dataPersistenceObjects.Add(dataPersistenceObj);
                }
            }

            return dataPersistenceObjects;
        }

        [PublicAPI]
        public Dictionary<string, T> LoadAllProfiles() 
        {
            return _dataHandler.LoadAllProfiles();
        }

        private void TryAutoSave()
        {
            if (_autoSaveTimeSeconds <= 0)
            {
                return;
            }
            
            // start up the auto saving coroutine
            if (_autoSaveCoroutine != null)
            {
                StopCoroutine(_autoSaveCoroutine);
            }

            _autoSaveCoroutine = StartCoroutine(AutoSave());
            
            IEnumerator AutoSave() 
            {
                while (true) 
                {
                    yield return new WaitForSeconds(_autoSaveTimeSeconds);
                    SaveGame();
                    Debug.Log($"Auto Saved Game \"{_selectedProfileId}\"");
                }
            }
        }
    }
}
