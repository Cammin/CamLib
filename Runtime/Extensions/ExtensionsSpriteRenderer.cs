using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsSpriteRenderer
    {
        public static void SetA(this SpriteRenderer render, float a)
        {
            Color color = render.color;
            color.a = a;
            render.color = color;
        }
    }
}