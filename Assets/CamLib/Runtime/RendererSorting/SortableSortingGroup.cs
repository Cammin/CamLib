using UnityEngine.Rendering;

namespace CamLib.RendererSorting
{
    public class SortableSortingGroup : SortableBase<SortingGroup>
    {
        protected override void SetOrderInLayer(SortingGroup component, int order)
        {
            component.sortingOrder = order;
        }
    }
}
