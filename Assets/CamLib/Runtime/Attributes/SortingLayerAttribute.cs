using System;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Use on int fields to draw a dropdown for picking among the sorting layers defined in the project
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SortingLayerAttribute : PropertyAttribute { }
}