using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Pool;

namespace CamLib
{
    public class SfxInstance : MonoBehaviour
    {
        private SfxAsset _asset;
        private AudioSource _source;
        private PositionConstraint _contraint;
        private WaitUntil _wait;
        private Coroutine _stopCo = null;
        private IObjectPool<SfxInstance> _pool;

        private void Awake()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _contraint = gameObject.AddComponent<PositionConstraint>();
            _wait = new WaitUntil(() => !_source.isPlaying);
        }
        
        public void SetPool(IObjectPool<SfxInstance> pool)
        {
            _pool = pool;
        }
        
        public void Play(SfxAsset asset, Transform follow = null, Vector3 position = default)
        {
            if (asset == null)
            {
                Debug.LogWarning("Sfx: Asset null");
                return;
            }

            if (follow)
            {
                _contraint.AddSource(new ConstraintSource(){sourceTransform = follow, weight = 1});
            }
            
            _asset = asset;
            _asset.Play(_source);
            _stopCo = StartCoroutine(WaitToStop());
        }
        
        private IEnumerator WaitToStop()
        {
            yield return _wait;
            Stop();
        }
        
        public void Stop()
        {
            if (!_source.isPlaying)
            {
                Debug.LogWarning("Sfx: Trying to stop when the audio source is not playing");
                return;
            }
            
            if (_stopCo == null)
            {
                return;
            }
            StopCoroutine(_stopCo);
            
            _pool.Release(this);
            _asset.Prepare(_source);
            _contraint.SetSources(null);
        }
    }
}

