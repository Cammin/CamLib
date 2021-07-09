using UnityEngine;
using Object = UnityEngine.Object;

namespace CamLib
{
    public class BoundsProvider : MonoBehaviour, ISettableBounds
    {
        [SerializeField] private Bounds _bounds = new Bounds
        {
            center = Vector2.one/2,
            size = Vector2.one
        };

        public Bounds Bounds => _bounds;
        Object ISettableBounds.DirtiedObject => this;

        public void SetBounds(Bounds newBounds)
        {
            _bounds = newBounds;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        }
    }
}