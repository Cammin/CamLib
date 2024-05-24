using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Pool;

namespace CamLib
{
    public static class Sfx
    {
        private static ObjectPool<SfxInstance> _pool;
        private static GameObject _root;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SetupVars()
        {
            _root = new GameObject("SfxPool");
            Object.DontDestroyOnLoad(_root);

            _pool = new ObjectPool<SfxInstance>(() =>
                {
                    GameObject obj = new GameObject($"Sfx{_root.transform.childCount}");
                    SfxInstance instance = obj.AddComponent<SfxInstance>();
                    obj.transform.SetParent(_root.transform);
                    return instance;
                }, instance =>
                {
                    instance.gameObject.SetActive(true);
                }, instance =>
                {
                    instance.gameObject.SetActive(false);
                }, 
                Object.Destroy, true, AudioSettings.GetConfiguration().numRealVoices);
        }

        /// <summary>
        /// Play.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx)
        {
            return PlayAtTransform(sfx, _root.transform);
        }

        /// <summary>
        /// Play at position.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx, Vector3 position)
        {
            return PlayAtTransformPosition(sfx, _root.transform, position);
        }

        /// <summary>
        /// Play in transform.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx, Transform parentTo)
        {
            return PlayAtTransform(sfx, parentTo);
        }

        /// <summary>
        /// Play in transform, at a position.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx, Transform parentTo, Vector3 position)
        {
            return PlayAtTransformPosition(sfx, parentTo, position);
        }
        
        private static SfxInstance PlayAtTransform(SfxAsset sfx, Transform parentTo)
        {
            return PlayAtTransformPosition(sfx, parentTo, parentTo.position);
        }

        private static SfxInstance PlayAtTransformPosition(SfxAsset sfx, Transform parentTo, Vector3 position)
        {
            SfxInstance instance = _pool.Get();
            instance.SetPool(_pool);
            
            instance.Play(sfx, parentTo, position);
            return instance;
        }
    }
}