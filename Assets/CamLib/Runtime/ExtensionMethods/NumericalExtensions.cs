using System;

namespace CamLib
{
    public static class NumericalExtensions
    {
        private const float THRESHOLD = 0.001f;
        
        public static bool IsInRange(this float val, float min, float max)
        {
            return val >= min && val <= max;
        }
        public static bool IsInRange(this int val, int min, int max)
        {
            return val >= min && val <= max;
        }

        public static bool IsEqual(this float val, float other)
        {
            return Math.Abs(val - other) < THRESHOLD;
        }

        public static bool IsWhole(this float val)
        {
            return Math.Abs(val % 1f) < THRESHOLD;
        }

        public static float RandomVariance(this float val)
        {
            return UnityEngine.Random.Range(-val, val);
        }
    }
}