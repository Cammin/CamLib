﻿using System;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsPrimitive
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
        
        public static bool IsWhole(this float val)
        {
            return Math.Abs(val % 1f) < THRESHOLD;
        }
        
        public static int ToPercent(this float normalizedValue)
        {
            return Mathf.RoundToInt(normalizedValue * 100);
        }

        public static float RandomVariance(this float val)
        {
            return UnityEngine.Random.Range(-val, val);
        }
        
        public static string StringFormatNoTrailedZeros(this float val)
        {
            string number = val.ToString(CultureInfo.CurrentCulture);
            int i = val.IsWhole() ? 0 : number.Substring(number.IndexOf(".", StringComparison.Ordinal) + 1).Length;
            return $"F{i}";
        }

    }
}