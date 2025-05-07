using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class BoundsExtensions
    {
        /// <summary>
        /// Random point inside a bounds
        /// </summary>
        public static Vector3 RandomPoint(this Bounds bounds) 
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
