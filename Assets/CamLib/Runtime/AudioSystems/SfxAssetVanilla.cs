using UnityEngine;

namespace CamLib.SoundAssets
{
    [CreateAssetMenu(menuName = CamLibAssetPath.SFX_PATH + nameof(SfxAssetVanilla))]
    public class SfxAssetVanilla : SfxAsset
    {
        [SerializeField] private AudioClip _clip = null;
        
        public override void Play(AudioSource source)
        {
            if (_clip == null)
            {
                Debug.LogError("No Sound Assigned!");
                return;
            }
            
            source.clip = _clip;
            source.Play();
        }

        public override void Prepare(AudioSource source)
        {
            source.clip = null;
        }
    }
}