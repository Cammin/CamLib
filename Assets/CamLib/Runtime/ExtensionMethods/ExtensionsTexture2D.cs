using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsTexture2D
    {
        public static Color32 AverageColor(this Texture2D tex)
        {
            Color32[] texColors = tex.GetPixels32().Where(p => p.a != 1).ToArray();

            int total = texColors.Length;

            float r = 0, g = 0, b = 0;
            
            for(int i = 0; i < total; i++)
            {
                r += texColors[i].r;
                g += texColors[i].g;
                b += texColors[i].b;
            }
            Color32 c = new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 255);
        
            return c;
        }
    }
}
