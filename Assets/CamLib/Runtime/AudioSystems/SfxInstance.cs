using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace CamLib
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxInstance : MonoBehaviour
    {
        private AudioSource _source;
        private Coroutine _stopCo = null;
        private SfxAsset _asset;
        
        internal IObjectPool<SfxInstance> Pool;


        private AudioSource Source
        {
            get
            {
                if (_source != null)
                {
                    return _source;
                }
                _source = GetComponent<AudioSource>();
                if (_source == null)
                {
                    _source = gameObject.AddComponent<AudioSource>();
                }
                return _source;
            }
        }
        
        public void Play(SfxAsset asset)
        {
            if (asset == null)
            {
                Debug.LogWarning("Sfx: Asset null");
                return;
            }
            
            asset.Play(Source);
            _stopCo = StartCoroutine(WaitToStop());
        }
        
        private IEnumerator WaitToStop()
        {
            yield return new WaitForSeconds(_source.clip.length);
            Stop();
        }
        
        public void Stop()
        {
            if (!Source.isPlaying)
            {
                Debug.LogWarning("Sfx: Trying to stop when the audio source is not playing");
                return;
            }
            
            if (_stopCo == null)
            {
                return;
            }
            
            Pool.Release(this);
            StopCoroutine(_stopCo);
            _asset.Prepare(_source);
        }
    }
}

