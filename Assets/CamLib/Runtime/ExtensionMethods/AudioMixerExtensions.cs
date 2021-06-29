﻿using UnityEngine;
using UnityEngine.Audio;

namespace CamLib
{
    public static class AudioMixerExtensions
    {
        public static void SetNormalizedVolume(this AudioMixerGroup group, float normalizedVolume)
        {
            normalizedVolume = Mathf.Clamp(normalizedVolume, 0.001f, 1);
            float db = Mathf.Log10(normalizedVolume) * 20;
            group.audioMixer.SetFloat(group.name, db);
        }
    }
}