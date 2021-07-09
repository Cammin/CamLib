using UnityEngine;

namespace CamLib
{
    public abstract class SfxAsset : ScriptableObject
    {
        public abstract void Play(AudioSource source);
        public abstract void Prepare(AudioSource source);
    }
}