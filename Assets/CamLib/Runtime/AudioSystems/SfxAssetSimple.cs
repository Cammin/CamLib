using UnityEngine;

namespace CamLib.SoundAssets
{
    [CreateAssetMenu(menuName = CamLibAssetPath.SFX_PATH + nameof(SfxAssetSimple))]
    public class SfxAssetSimple : SfxAsset
    {
        [SerializeField] private AudioClip[] _clips = null;
        
        [SerializeField, MinMaxRange(0, 1)] private Vector2 _volume = new Vector2(1, 1);
        [SerializeField, MinMaxRange(0, 2)] private Vector2 _pitch = new Vector2(1, 1);
        [SerializeField] private bool _loop = false;

        public override void Play(AudioSource source)
        {
            if (_clips.IsNullOrEmpty())
            {
                Debug.LogError("No Sounds Assigned!");
                return;
            }

            source.clip = _clips.GetRandomElement();
            source.volume = _volume.MinMaxRandom();
            source.pitch = _pitch.MinMaxRandom();
            source.loop = _loop;
            
            source.Play();
        }

        public override void Prepare(AudioSource source)
        {
            source.clip = null;
            source.volume = 1;
            source.pitch = 1;
            source.loop = false;

        }
    }
}