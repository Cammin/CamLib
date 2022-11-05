using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsRectTransform
    {
        public static Rect GetWorldRect(this RectTransform rectT)
        {
            Vector3[] corners = new Vector3[4];
            rectT.GetWorldCorners(corners);
            Vector3 bottomLeft = corners[0];
            Vector3 lossy = rectT.lossyScale;
            Rect rect = rectT.rect;
            Vector2 size = new Vector2(lossy.x * rect.size.x, lossy.y * rect.size.y);
            return new Rect(bottomLeft, size);
        }        
        public static Vector2 RandomPoint(this RectTransform rectT)
        {
            Rect bounds = rectT.rect;
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }
    }
}