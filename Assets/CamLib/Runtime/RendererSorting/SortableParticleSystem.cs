using UnityEngine;

namespace CamLib
{
    public class SortableParticleSystem : SortableBase<ParticleSystem>
    {
        private ParticleSystemRenderer _renderer = null;

        protected override void SetOrderInLayer(ParticleSystem component, int order)
        {
            if (ReferenceEquals(_renderer, null))
            {
                _renderer = component.GetComponent<ParticleSystemRenderer>();
            }

            _renderer.sortingOrder = order;
        }
    }
}
