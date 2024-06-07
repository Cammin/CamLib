using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Straightforward singleton
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance = null;
        
        /// <summary>
        /// If trying to access an instance every update (or a lot) that may not exist, but so it's not doing FindAnyObjectByType every time
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
            
                //find if nonexistent
                _instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (_instance != null)
                {
                    return _instance;
                }
                
                //We should always assert that an expected singleton should exist
                Debug.LogError($"No singleton found for {typeof(T).Name}.");
                return null;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetVars()
        {
            _instance = null;
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning($"Singleton instance already exists! {_instance.name}", gameObject);
                return;
            }

            _instance = this as T;
        }
    }
}
