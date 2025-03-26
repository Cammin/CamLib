using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsTransform
    {
        /// <summary>
        /// Conserves Z
        /// </summary>
        public static void SetXY(this Transform transform, Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
    }
}