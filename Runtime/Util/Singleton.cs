using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Straightforward singleton
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public bool _doNotDestroyOnLoad = false;
        
        private static T _instance;
        
        /// <summary>
        /// If trying to access an instance every update (or a lot) that may not exist, but so it's not doing FindAnyObjectByType every time
        /// </summary>
        public static bool HasInstance => _instance != null;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetInstance()
        {
            _instance = null;
        }
#endif
        
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
                    Debug.Log($"{typeof(T).Name} cached via FindAnyObjectByType");
                    return _instance;
                }
                
                Debug.LogError($"Singleton {typeof(T).Name} not found.");
                return null;
            }
        }

        protected virtual void Awake()
        {
            if (_instance)
            {
                if (_doNotDestroyOnLoad)
                {
                    Destroy(gameObject);
                    return;
                }
                
                Debug.LogWarning($"Singleton {typeof(T).Name} instance already exists! {_instance.name}", gameObject);
                return;
            }

            _instance = this as T;
            //Debug.Log($"Singleton {typeof(T).Name} cached");
            if (_doNotDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
