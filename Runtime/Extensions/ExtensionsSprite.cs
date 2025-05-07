using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsSprite
    {
        /// <summary>
        /// Used to validate in-editor if a sprite has an expected pivot for the project
        /// </summary>
        public static bool IsPivotPixelPerfect(this Sprite sprite)
        {
            Vector2 pixelPivot = sprite.pivot;
            return pixelPivot.x.IsWhole() && pixelPivot.y.IsWhole();
        }
    }
}
