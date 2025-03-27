using UnityEngine;

namespace Game
{
    /// <summary>
    /// Simple script that scrolls a repeating texture on a sprite renderer. Good for repeating overhead clouds, etc
    /// </summary>
    public class ScrollTexture : MonoBehaviour
    {
        [SerializeField] private Vector2 _scroll;
        [SerializeField] private Vector2 _resetThreshold;
        [SerializeField] private SpriteRenderer _render;
    
        private Vector2 _size;

        private void Start()
        {
            if (_render == null)
            {
                _render = GetComponent<SpriteRenderer>();
            }
        
            _size = _render.size;
        }

        private void Update()
        {
            Vector2 newRenderSize = _render.size + (_scroll * Time.deltaTime);

            CheckToReset(ref newRenderSize.x, ref _size.x, ref _resetThreshold.x, ref _scroll.x);
            CheckToReset(ref newRenderSize.y, ref _size.y, ref _resetThreshold.y, ref _scroll.y);

            void CheckToReset(ref float renderSize, ref float baseSize, ref float threshold, ref float speed)
            {
                float magnitude = baseSize * threshold;
                if (speed > 0 && renderSize > magnitude)
                {
                    renderSize -=  (magnitude - baseSize);
                }
                if (speed < 0 && renderSize < magnitude)
                {
                    renderSize +=  (magnitude - baseSize);
                }
            }

            _render.size = newRenderSize;
        }
    }
}
