using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(menuName = Consts.PATH + nameof(SfxAssetBasic))]
    public class SfxAssetBasic : SfxAsset
    {
        [SerializeField] private AudioClip _clip = null;
        
        public override void Play(AudioSource source)
        {
            if (_clip == null)
            {
                Debug.LogError("No Sound Assigned!", this);
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