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
    /// </summary>
    public abstract class DataPersistenceManager<T> : MonoBehaviour where T : GameData
    {
        [Header("Debugging")]
        [SerializeField] private bool _disableDataPersistence = false;
        [SerializeField] private bool _initializeDataIfNull = false;
        [SerializeField] private bool _loadBeforeFirstSceneLoad = false;
        [Space]
        [SerializeField] private bool _overrideSelectedProfileId = false;
        [SerializeField] private string _testSelectedProfileId = "test";

        [Header("File Storage Config")]
        [SerializeField] private string _fileName = "save.data";
        [SerializeField] private bool _useEncryption;

        [Header("Auto Saving Configuration")]
        [SerializeField] private float _autoSaveTimeSeconds = 60f;

        private T _gameData;
        private List<IDataPersistence<T>> _dataPersistenceObjects = new List<IDataPersistence<T>>();
        private FileDataHandler<T> _dataHandler;
        private string _selectedProfileId = "";
        private Coroutine _autoSaveCoroutine;
        
        public static DataPersistenceManager<T> Instance { get; private set; }

        public int PersistentObjectCount => _dataPersistenceObjects.Count;

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

            if (_disableDataPersistence) 
            {
                Debug.LogWarning("Data Persistence is currently disabled!");
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
            if (_loadBeforeFirstSceneLoad)
            {
                SetupLoadingMecanism();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => SetupLoadingMecanism();
        private void SetupLoadingMecanism()
        {
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();

            // start up the auto saving coroutine
            if (_autoSaveCoroutine != null)
            {
                StopCoroutine(_autoSaveCoroutine);
            }

            _autoSaveCoroutine = StartCoroutine(AutoSave());
        }

        [PublicAPI]
        public void ChangeSelectedProfileId(string newProfileId) 
        {
            // update the profile to use for saving and loading
            _selectedProfileId = newProfileId;
            // load the game, which will use that profile, updating our game data accordingly
            LoadGame();
        }

        [PublicAPI]
        public void DeleteProfileData(string profileId) 
        {
            // delete the data for this profile id
            _dataHandler.Delete(profileId);
            // initialize the selected profile id
            InitializeSelectedProfileId();
            // reload the game so that our data matches the newly selected profile id
            LoadGame();
        }

        private void InitializeSelectedProfileId() 
        {
            _selectedProfileId = _dataHandler.GetMostRecentlyUpdatedProfileId();
            if (_overrideSelectedProfileId) 
            {
                _selectedProfileId = _testSelectedProfileId;
                Debug.LogWarning("Overrode selected profile id with test id: " + _testSelectedProfileId);
            }
        }

        [PublicAPI]
        public void NewGame()
        {
            _gameData = (T)Activator.CreateInstance(typeof(T));
            _gameData.OnConstruct();
        }

        [PublicAPI]
        public T LoadGame(string profileId = null)
        {
            profileId ??= _selectedProfileId;

            // return right away if data persistence is disabled
            if (_disableDataPersistence) 
            {
                return null;
            }

            // load any saved data from a file using the data handler
            _gameData = _dataHandler.Load(profileId);

            // start a new game if the data is null and we're configured to initialize data for debugging purposes
            if (_gameData == null && _initializeDataIfNull) 
            {
                NewGame();
            }

            // if no data can be loaded, don't continue
            if (_gameData == null) 
            {
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
                return null;
            }

            if (Application.isPlaying)
            {
                // push the loaded data to all other scripts that need it
                foreach (IDataPersistence<T> dataPersistenceObj in _dataPersistenceObjects)
                {
                    dataPersistenceObj?.LoadData(_gameData);
                }
            }

            return _gameData;
        }

        [PublicAPI]
        public void SaveGame(string profileId = null, T overrideData = null)
        {
            profileId ??= _selectedProfileId;
            if (overrideData != null)
            {
                _gameData = overrideData;
            }
            
            // return right away if data persistence is disabled
            if (_disableDataPersistence) 
            {
                return;
            }

            // if we don't have any data to save, log a warning here
            if (_gameData == null) 
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
                return;
            }

            if (Application.isPlaying)
            {
                // pass the data to other scripts so they can update it
                foreach (IDataPersistence<T> dataPersistenceObj in _dataPersistenceObjects) 
                {
                    dataPersistenceObj?.SaveData(_gameData);
                }
            }

            // timestamp the data so we know when it was last saved
            _gameData.LastUpdated = System.DateTime.Now.ToBinary();

            // save that data to a file using the data handler
            _dataHandler.Save(_gameData, profileId);
        }

        private void OnApplicationQuit() 
        {
            SaveGame();
        }

        private List<IDataPersistence<T>> FindAllDataPersistenceObjects() 
        {
            var monos = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var dataPersistenceObjects = new List<IDataPersistence<T>>();
    
            foreach (var mono in monos)
            {
                if (mono is IDataPersistence<T> dataPersistenceObj)
                {
                    dataPersistenceObjects.Add(dataPersistenceObj);
                }
            }

            return dataPersistenceObjects;
        }

        [PublicAPI]
        public bool HasGameData() 
        {
            return _gameData != null;
        }

        [PublicAPI]
        public Dictionary<string, T> GetAllProfilesGameData() 
        {
            return _dataHandler.LoadAllProfiles();
        }

        private IEnumerator AutoSave() 
        {
            while (true) 
            {
                yield return new WaitForSeconds(_autoSaveTimeSeconds);
                SaveGame();
                Debug.Log("Auto Saved Game");
            }
        }
    }
}
