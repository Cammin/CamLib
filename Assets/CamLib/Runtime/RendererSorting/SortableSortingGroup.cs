using UnityEngine.Rendering;

namespace CamLib
{
    public class SortableSortingGroup : SortableBase<SortingGroup>
    {
        protected override void SetOrderInLayer(SortingGroup component, int order)
        {
            component.sortingOrder = order;
        }
    }
}
