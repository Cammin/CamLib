using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Straightforward singleton
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public bool _doNotDestroyOnLoad = true;
        
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
            
                //find if nonexistent
                _instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (_instance != null)
                {
                    return _instance;
                }
                
                Debug.LogError($"{typeof(T).Name} singleton not found in the scene.");
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
                
                Debug.LogWarning($"Singleton instance already exists! {_instance.name}", gameObject);
                return;
            }

            _instance = this as T;
            if (_doNotDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
