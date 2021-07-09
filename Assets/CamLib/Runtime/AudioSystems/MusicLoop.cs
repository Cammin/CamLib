using UnityEngine;

namespace CamLib.Music
{
    public class MusicLoop : MonoBehaviour
    {
        [SerializeField] private AudioSource _startSource = null;
        [SerializeField] private AudioSource _loopSource = null;

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
