using System;
using UnityEngine;

namespace CamLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        private const float DEFAULT_SNAP_THRESHOLD = 0.1f;
        public MinMaxRangeAttribute(float min = 0, float max = 1, float snapThreshold = DEFAULT_SNAP_THRESHOLD)
        {
            Min = min;
            Max = max;
            SnapThreshold = snapThreshold;
        }
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float SnapThreshold { get; private set; }
    }
}