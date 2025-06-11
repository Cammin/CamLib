using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace CamLib
{
    public static class TextUtils
    {
        private const string ColorTagPrefix = "<color=#";
        private const string ColorTagSuffix = "</color>";
        private const string CloseBracket = ">";
        
        private static readonly Dictionary<Color32, string> ColorCache = new Dictionary<Color32, string>();
        
        private static readonly StringBuilder StringBuilder = new StringBuilder(64);

        public static string ColorRichText(string text, Color color)
        {
            Color32 color32 = color;
            if (!ColorCache.TryGetValue(color32, out string hexColor))
            {
                hexColor = ColorUtility.ToHtmlStringRGB(color);
                ColorCache[color32] = hexColor;
            }

            StringBuilder.Clear()
                .Append(ColorTagPrefix)
                .Append(hexColor)
                .Append(CloseBracket)
                .Append(text)
                .Append(ColorTagSuffix);
                
            return StringBuilder.ToString();
        }
        
        public static string ColorRichText(string text, string colorHex)
        {
            colorHex = colorHex.TrimStart('#');
        
            StringBuilder.Clear()
                .Append(ColorTagPrefix)
                .Append(colorHex)
                .Append(CloseBracket)
                .Append(text)
                .Append(ColorTagSuffix);
            
            return StringBuilder.ToString();
        }
    
        // Optional: Preload commonly used colors
        public static void PreloadCommonColors()
        {
            ColorCache[Color.red] = "FF0000";
            ColorCache[Color.green] = "00FF00";
            ColorCache[Color.blue] = "0000FF";
            ColorCache[Color.white] = "FFFFFF";
            ColorCache[Color.black] = "000000";
            // Add more common colors as needed
        }
    
        // Optional: Clear cache if needed (rarely used)
        public static void ClearColorCache()
        {
            ColorCache.Clear();
        }
    }
}