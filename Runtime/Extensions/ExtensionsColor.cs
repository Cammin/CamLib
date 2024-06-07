using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsColor
    {
        public static Color Alpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }
        public static Color Hue(this Color color, float h)
        {
            Color.RGBToHSV(color, out float _, out float s, out float v);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }
        public static Color Saturation(this Color color, float s)
        {
            Color.RGBToHSV(color, out float h, out float _, out float v);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }
        public static Color Value(this Color color, float v)
        {
            Color.RGBToHSV(color, out float h, out float s, out float _);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }
    }
}