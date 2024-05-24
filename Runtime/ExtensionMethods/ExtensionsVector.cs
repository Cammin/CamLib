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
        
        private const float DEFAULT_SNAP_VALUE = 0.0625f;
        
        public static Vector3 SnappedToGrid(this Vector3 vector, float snapValue = DEFAULT_SNAP_VALUE) => SnappedToGrid((Vector2)vector, snapValue);
        public static Vector2 SnappedToGrid(this Vector2 vector, float snapValue = DEFAULT_SNAP_VALUE)
        {
            float snapInverse = 1f/snapValue;

            vector.x = FloatSnappedToGrid(vector.x);
            vector.y = FloatSnappedToGrid(vector.y);

            return vector;
            
            float FloatSnappedToGrid(float f)
            {
                // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
                // so 1.45 to nearest .5 is 1.5
                return Mathf.Round(f * snapInverse)/snapInverse;
            }
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
