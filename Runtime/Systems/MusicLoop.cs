using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Simple component that starts music with an intro, then loop.
    /// </summary>
    public class MusicLoop : MonoBehaviour
    {
        public AudioSource _startSource = null;
        public AudioSource _loopSource = null;

        private void Awake()
        {
            _startSource.loop = false;
            _loopSource.loop = true;

            _startSource.playOnAwake = false;
            _loopSource.playOnAwake = false;
        }

        private void Start()
        {
            if (_startSource.clip == null) return;
            
            _startSource.Play();
            _loopSource.PlayScheduled(_startSource.clip.length);
        }

    }
}
