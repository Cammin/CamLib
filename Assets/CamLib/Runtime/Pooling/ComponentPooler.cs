using UnityEngine;
using UnityEngine.Pool;

namespace CamLib
{
    public sealed class ComponentPooler<T> where T : Component
    {
        private readonly string _gameObjectName;
        private readonly ObjectPool<T> _pool;

        public IObjectPool<T> Pool => _pool;

        public ComponentPooler()
        {
            _gameObjectName = "Pool";
            _pool = new ObjectPool<T>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, AudioSettings.GetConfiguration().numRealVoices);
        }
        public ComponentPooler(string name)
        {
            _gameObjectName = name;
            _pool = new ObjectPool<T>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, AudioSettings.GetConfiguration().numRealVoices);
        }

        private T CreateFunc()
        {
            GameObject obj = new GameObject(_gameObjectName);
            T instance = obj.AddComponent<T>();
            return instance;
        }

        private void ActionOnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(T obj)
        {
            Object.Destroy(obj.gameObject);
        }
        
        public T Get()
        {
            return _pool.Get();
        }
    }
}