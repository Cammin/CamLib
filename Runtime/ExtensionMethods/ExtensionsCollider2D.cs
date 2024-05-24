using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsCollider2D
    {
        private static Rect GetRect(this BoxCollider2D coll)
        {
            Transform transform = coll.transform;
            Vector3 lossy = transform.lossyScale;
            Rect rect = new Rect
            {
                size = coll.size * lossy,
                center = (Vector2)transform.position + (coll.offset * lossy)
            };
            return rect;

        }
    }
}