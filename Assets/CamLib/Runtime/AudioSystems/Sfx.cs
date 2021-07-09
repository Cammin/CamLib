using CamLib.SoundAssets;
using UnityEngine;

namespace CamLib
{
    public static class Sfx
    {
        private static readonly ComponentPooler<SfxInstance> Pooler = new ComponentPooler<SfxInstance>("Pooled Audio Source");
        private static GameObject _poolerGameObject;

        private static GameObject GameObj
        {
            get
            {
                if (_poolerGameObject != null)
                {
                    return _poolerGameObject;
                }

                _poolerGameObject = new GameObject("SFX Pooler");
                Object.DontDestroyOnLoad(_poolerGameObject);
                return _poolerGameObject;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetVars()
        {
            _poolerGameObject = null;
        }

        /// <summary>
        /// Play.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx)
        {
            return PlayAtTransform(sfx, GameObj.transform);
        }

        /// <summary>
        /// Play at position.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx, Vector3 position)
        {
            return PlayAtTransformPosition(sfx, GameObj.transform, position);
        }

        /// <summary>
        /// Play in transform. This would not be maintained through a scene change.
        /// </summary>
        public static SfxInstance Play(SfxAsset sfx, Transform parentTo)
        {
            return PlayAtTransform(sfx, parentTo);
        }

        /// <summary>
        /// Play in transform, at a position. This would not be maintained through a scene changed.
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
            SfxInstance instance = Pooler.Get();
            instance.Pool = Pooler.Pool;
            
            Transform transform = instance.transform;
            transform.SetParent(parentTo, true);
            transform.position = position;
            
            instance.Play(sfx);
            return instance;
        }
    }
}