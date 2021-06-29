using UnityEngine;

namespace CamLib
{
    public static class SpriteExtensions
    {
        public static bool IsPivotPixelPerfect(this Sprite sprite)
        {
            Vector2 pixelPivot = sprite.pivot;
            return pixelPivot.x.IsWhole() && pixelPivot.y.IsWhole();
        }
    }
}
