using System;
using UnityEngine;
using Object = UnityEngine.Object;

#pragma warning disable 0649

namespace CamLib.RendererSorting
{
    public abstract class SortableBase<T> : MonoBehaviour, ISortable where T : Component
    {
        private const int ORIGIN_ORDER = 100;
        private const float PRECISION = PIXEL_SIZE;
        private const float PIXEL_SIZE = 1/16f;
    
        [SerializeField] private T _sortableComponent = null;
        [Space]
        [SerializeField] private SortableUpdateStyle _updateStyle = SortableUpdateStyle.UpdatedByManager;
        [SerializeField, Range(-50, 30)] private int _offset = 0;

        private float YOffset => PIXEL_SIZE * _offset;
        public Object DirtiedEditorObject => _sortableComponent;
        
        private void Awake()
        {
            if (_sortableComponent == null)
            {
                _sortableComponent = GetComponent<T>();
                Debug.LogWarning("GetComponent SortableRenderer", gameObject);
            }

            if (_sortableComponent == null)
            {
                Debug.LogError("GetComponent SortableRenderer Error", gameObject);
                return;
            }

            switch (_updateStyle)
            {
                case SortableUpdateStyle.UpdatedByManager:
                    SortableManager.Add(this);
                    break;
                case SortableUpdateStyle.OnlyOnce:
                    UpdateOrder();
                    enabled = false;
                    break;
                case SortableUpdateStyle.SelfManaged:
                    //we would be responsible for our own update to order, so do nothing.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected abstract void SetOrderInLayer(T component, int order);

        public void UpdateOrder()
        {
            if (_sortableComponent == null)
            {
                Debug.LogError("Did not update sort order, _sortableComponent null!", gameObject);
                return;
            }
            
            float yPos = transform.position.y + YOffset;
            int order = (int) (ORIGIN_ORDER - (yPos) / PRECISION + YOffset);

            SetOrderInLayer(_sortableComponent, order);
        }
        
        #region Gizmos
        private void OnDrawGizmosSelected()
        {
            Vector3 pos = transform.position + Vector3.up * YOffset;

            if (_sortableComponent != null && _sortableComponent is SpriteRenderer spriteRenderer && spriteRenderer.sprite != null)
            {
                Color gizmoColor = spriteRenderer.sprite.texture.AverageColor();
                gizmoColor.a = spriteRenderer.color.a;
                Gizmos.color = gizmoColor;
            }
            Gizmos.DrawSphere(pos, 0.03125F);
        }
        #endregion
        
    }
}