using UnityEngine;

namespace CamLib
{
    public interface ISettableBounds : IBounds
    {
        Object DirtiedObject { get; }
        void SetBounds(Bounds newBounds);
    }
}
