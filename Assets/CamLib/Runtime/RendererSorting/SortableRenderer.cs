using UnityEngine;

namespace CamLib.RendererSorting
{
    public class SortableRenderer : SortableBase<Renderer>
    {
        protected override void SetOrderInLayer(Renderer component, int order)
        {
            component.sortingOrder = order;
        }
    }
}
