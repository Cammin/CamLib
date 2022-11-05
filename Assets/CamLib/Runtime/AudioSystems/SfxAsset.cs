using UnityEngine;

namespace CamLib
{
    public abstract class SfxAsset : ScriptableObject
    {
        /// <summary>
        /// What to do with the source when it's pooled 
        /// </summary>
        public abstract void Play(AudioSource source);
        
        /// <summary>
        /// What to do with the source when it's released 
        /// </summary>
        public abstract void Prepare(AudioSource source);
    }
}