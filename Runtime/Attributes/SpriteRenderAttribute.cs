using System;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Use on sprite fields to draw a preview image of the sprite with a checkerboard background
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SpriteRenderAttribute : PropertyAttribute { }
}
