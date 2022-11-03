using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsVector
    {
        public static Vector2 Magnitude(this Vector2 vector, float newMagnitude)
        {
            return vector.normalized * newMagnitude;
        }

        public static float MinMaxRandom(this Vector2 vector)
        {
            Debug.Assert(vector.x <= vector.y, "Unexpected min-max value; min is greater that max");
            return Random.Range(vector.x, vector.y);
        }

        public static bool MinMaxIsInRange(this Vector2 vector, float value)
        {
            Debug.Assert(vector.x <= vector.y, "Unexpected min-max value; min is greater that max");
            return value.IsInRange(vector.x, vector.y);
        }
    }
}
