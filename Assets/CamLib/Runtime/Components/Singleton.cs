using UnityEngine;

namespace CamLib
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance = null;
        
        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
            
                //find if nonexistent
                _instance = FindObjectOfType<T>(true);
                if (_instance != null)
                {
                    return _instance;
                }
                
                //create if not found
                GameObject obj = new GameObject(typeof(T).Name);
                _instance = obj.AddComponent<T>();
                Debug.LogWarning("A singleton instance was created; didn't exist in the scene.", _instance.gameObject);
                return _instance;
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
                Debug.LogWarning($"Singleton instance already exists! {_instance.name}");
                return;
            }

            _instance = this as T;
        }
    }
}
