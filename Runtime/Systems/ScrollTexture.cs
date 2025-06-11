using UnityEngine;
using UnityEngine.Serialization;

namespace CamLib
{
    /// <summary>
    /// Simple script that scrolls a repeating texture on a sprite renderer. Good for repeating overhead clouds, etc
    /// </summary>
    public class ScrollTexture : MonoBehaviour
    {
        [SerializeField] private Vector2 scroll;
        [SerializeField] private Vector2 resetThreshold;
        [SerializeField] private SpriteRenderer render;
    
        private Vector2 _size;

        private void Start()
        {
            if (!render)
            {
                render = GetComponent<SpriteRenderer>();
            }
        
            _size = render.size;
        }

        private void Update()
        {
            Vector2 newRenderSize = render.size + (scroll * Time.deltaTime);

            CheckToReset(ref newRenderSize.x, ref _size.x, ref resetThreshold.x, ref scroll.x);
            CheckToReset(ref newRenderSize.y, ref _size.y, ref resetThreshold.y, ref scroll.y);

            render.size = newRenderSize;
            return;

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
        }
    }
}
