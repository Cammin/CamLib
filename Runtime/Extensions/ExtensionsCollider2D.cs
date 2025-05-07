using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsCollider2D
    {
        /// <summary>
        /// World space rectangle of this collider
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
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