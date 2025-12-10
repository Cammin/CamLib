using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Straightforward singleton
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _doNotDestroyOnLoad;
        
        private static T _instance;
        
        /// <summary>
        /// Indicates whether an instance exists without trying to create one
        /// </summary>
        public static bool HasInstance => _instance != null;
        
        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
            
                //find if nonexistent because race conditions. this will search all scenes, even if a scene is not the active scene
                _instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (_instance != null)
                {
                    Debug.Log($"[Singleton] Using existing {typeof(T).Name} found in scene by FindAnyObjectByType.");
                    return _instance;
                }

                Debug.LogError($"[Singleton] No instance of {typeof(T).Name} found.");
                return null;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                if (_doNotDestroyOnLoad)
                {
                    Destroy(gameObject);
                    Debug.LogWarning($"[Singleton] Duplicate {typeof(T).Name} destroyed.");
                    return;
                }
                
                Debug.LogWarning($"[Singleton] Multiple instances of {typeof(T).Name} detected! " +
                    $"Current: {name}, Existing: {_instance.name}");
                return;
            }

            _instance = this as T;
            
            if (_doNotDestroyOnLoad)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                Debug.Log($"[Singleton] {typeof(T).Name} marked as DontDestroyOnLoad");
            }
        }
        
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}