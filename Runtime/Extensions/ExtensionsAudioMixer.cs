using UnityEngine;
using UnityEngine.Audio;

namespace CamLib
{
    public static class ExtensionsAudioMixer
    {
        /// <summary>
        /// Wonderful function to serve as setting decibel volume much more nicely with respect to a number from 0 to 1.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="exposedParam">Pass null to use the group name</param>
        /// <param name="normalizedVolume"></param>
        public static void SetNormalizedVolume(this AudioMixerGroup group, string exposedParam, float normalizedVolume)
        {
            normalizedVolume = Mathf.Clamp(normalizedVolume, 0.001f, 1);
            float db = Mathf.Log10(normalizedVolume) * 20;
            exposedParam ??= group.name;
            group.audioMixer.SetFloat(exposedParam, db);
        }
    }
}