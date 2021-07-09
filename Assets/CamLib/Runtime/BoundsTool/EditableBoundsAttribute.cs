using System;
using UnityEngine;

namespace CamLib
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EditableBoundsAttribute : Attribute
    {
        private readonly Color _color;

        public Color Color => _color;

        public EditableBoundsAttribute()
        {
        }

        public EditableBoundsAttribute(Color color)
        {
            _color = color;
        }
    }
}