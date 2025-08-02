using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CamLib
{
    /// <summary>
    /// Pools gameobjects and auto-release after a set time. Good for temporary visual effects
    /// </summary>
    public class TimedPool : MonoBehaviour
    {
        public GameObject _prefab;
        public int _prewarmCount = 2;
        public float _time = 1f;
        
        private WaitForSeconds _wait;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _wait = new WaitForSeconds(_time);
            
            if (_prefab == null)
            {
                Debug.LogError("Prefab is not assigned.");
                return;
            }

            GameObject CreateFunc()
            {
                GameObject obj = Instantiate(_prefab, transform);
                obj.SetActive(false);
                return obj;
            }

            void OnGet(GameObject obj)
            {
                obj.SetActive(true);
            }

            void OnRelease(GameObject obj)
            {
                obj.SetActive(false);
            }

            _pool = new ObjectPool<GameObject>(CreateFunc, OnGet, OnRelease);
        }

        /// <summary>
        /// Preallocate. Defers over multiple frames to spread out the processing time.
        /// </summary>
        private IEnumerator Start()
        {
            yield return null;
            
            List<GameObject> list = new(_prewarmCount);
            for (int i = 0; i < _prewarmCount; i++)
            {
                yield return null;
                list.Add(_pool.Get());
            }

            foreach (GameObject o in list)
            {
                yield return null;
                _pool.Release(o);
            }
        }
        
        public GameObject Spawn(Vector3 pos)
        {
            var v = _pool.Get();
            v.SetActive(true);
            v.transform.position = pos;
            StartCoroutine(AfterTime(v));
            return v;
        }

        private IEnumerator AfterTime(GameObject obj)
        {
            yield return _wait;
            _pool.Release(obj);
        }
    }
}