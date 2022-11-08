using System;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Use this to draw some buttons to test audio clips from the AudioClip field itself.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AudioClipButtonsAttribute : PropertyAttribute { }
}
