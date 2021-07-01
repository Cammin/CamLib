using UnityEngine;

namespace CamLib
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool _dontDestroyOnLoad;
        private static T _instance = null;
        
        protected static T Instance
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
            if (_instance == null)
            {
                _instance = this as T;

                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Debug.LogWarning($"Singleton instance already exists; destroying self {_instance.name}");
                Destroy(gameObject);
            }
        }
    }
}
