using UnityEngine;

namespace CamLib
{
    public class ParallaxBackgroundLayerInstance : MonoBehaviour
    {
        private const float WRAP_SCALE = 4;
        
        [SerializeField] private SpriteRenderer _render = null;
        [SerializeField] private ParallaxBackgroundLayer _properties = null;
        
        private Camera _cam;

        private Vector2 _origin;
        private Vector2 _targetOffset;

        private Vector2 _autoScroll;
        private Vector2 _textureUnitSize = Vector2.one;

        public void SetProperties(ParallaxBackgroundLayer pairing) => _properties = pairing;
        public void SetSortingOrder(int order) => _render.sortingOrder = order;
        public void SetCamera(Camera cam) => _cam = cam;
        public void SetAlpha(float pairingImageAlpha)
        {
            Color newColor = _render.color;
            newColor.a = pairingImageAlpha;
            _render.color = newColor;
        }


        public void Start()
        {
            Setup();
        }
        private void Update()
        {
            UpdatePosition();
        }

        private void Setup()
        {
            Sprite sprite = _properties.BackgroundSprite;
            if (sprite == null)
            {
                Debug.LogError("Sprite is null!", gameObject);
                return;
            }

            Texture2D texture = sprite.texture;
            _textureUnitSize = new Vector2(texture.width, texture.height) / sprite.pixelsPerUnit;

            Vector2 cameraSize = _cam.OrthographicSize();
            //Debug.Log($"Camera Unit Size: {cameraSize}");
            
            
            Vector2 renderSize = GetMinimumComfortableSize(_textureUnitSize, cameraSize, _properties.Infinite);
            //Debug.Log($"Render Size: {renderSize}");
            
            _render.sprite = sprite;
            _render.sortingLayerID = _properties.Layer;
            _render.size = renderSize;
            
            SnapToCameraBounds(_textureUnitSize, cameraSize);
        }

        private Vector2 GetMinimumComfortableSize(Vector2 textureUnitSize, Vector2 cameraSize, Bool2 isInfinite)
        {
            Vector2 size = Vector2.zero;
            size.x = GetMinimumComfortableLength(textureUnitSize.x, cameraSize.x, isInfinite.x);
            size.y = GetMinimumComfortableLength(textureUnitSize.y, cameraSize.y, isInfinite.y);
            return size;
        }
        private float GetMinimumComfortableLength(float textureUnitSize, float cameraSize, bool isInfinite)
        {
            if (!isInfinite) return textureUnitSize;
            return cameraSize * WRAP_SCALE;
        }
        
        private void SnapToCameraBounds(Vector2 textureUnitOffset, Vector2 cameraUnitOffset)
        {
            BoundsSnapSide snapSide = _properties.SnapSide;
            
            if (snapSide == BoundsSnapSide.None)
            {
                return;
            }

            Rect area = _cam.OrthographicRect();
            if (area == Rect.zero)
            {
                return;
            }
            
            //bottom align with image, to bottom align with bottom of camera.
            Vector2 newOriginPos = Vector2.zero;
            Vector2 originOffset = textureUnitOffset / 2;
            switch (snapSide)
            {
                case BoundsSnapSide.Left:
                    newOriginPos.x = area.xMin + originOffset.x;
                    break;
                    
                case BoundsSnapSide.Right:
                    newOriginPos.x = area.xMax - originOffset.x;
                    break;
                
                case BoundsSnapSide.Top:
                    newOriginPos.y = area.yMax - originOffset.y;
                    break;
                
                case BoundsSnapSide.Bottom:
                    newOriginPos.y = area.yMin + originOffset.y;
                    break;
            }
            _origin = newOriginPos;

            
            float targetOffsetDirection = 0;
            switch (snapSide)
            {
                case BoundsSnapSide.Bottom:
                case BoundsSnapSide.Left:
                    targetOffsetDirection = 1;
                    break;
                    
                case BoundsSnapSide.Top:
                case BoundsSnapSide.Right:
                    targetOffsetDirection = -1;
                    break;
            }

            //const float digInMultiplier = 1.0035f;  //this extra tiny offset is to help with the anchored objects not moving up so fast that it creates a gap line within the pixel perfect camera. 
            _targetOffset = (textureUnitOffset - cameraUnitOffset) / 2 * (targetOffsetDirection);
            
            switch (snapSide)
            {
                case BoundsSnapSide.Top:
                case BoundsSnapSide.Bottom:
                    if (_properties.Infinite.y) Debug.LogWarning("Snapping up or down won't look correct if the infinite's y setting is on.");
                    break;
                    
                case BoundsSnapSide.Left:
                case BoundsSnapSide.Right:
                    if (_properties.Infinite.x) Debug.LogWarning("Snapping left or right won't look correct if the infinite's x setting is on.");
                    break;
            }
        }

        private void UpdatePosition()
        {
            Vector2 camPos = _cam.transform.position;

            Vector2 scrollSpeed = _properties.AutoScrollSpeed;
            scrollSpeed.y /= 2; //add extra autoscroll speed to compensate for an unknown, unexpected extra half-y value
            _autoScroll += scrollSpeed * Time.deltaTime;

            Vector2 startPos = _origin + _autoScroll;
            Vector2 targetPos = camPos + _targetOffset;

            transform.position = SolveVector2(startPos, targetPos, _properties.ParallaxEffectMultiplier, _properties.Infinite, _textureUnitSize);
        }

        private Vector2 SolveVector2(Vector2 start, Vector2 target, Vector2 factor, Bool2 isInfinite,
            Vector2 textureSizeInUnits)
        {
            Vector2 newPos = Vector2.zero;
            newPos.x = SolveVector(start.x, target.x, factor.x, isInfinite.x, textureSizeInUnits.x);
            newPos.y = SolveVector(start.y, target.y, factor.y, isInfinite.y, textureSizeInUnits.y);
            return newPos;
        }

        private float SolveVector(float start, float target, float factor, bool isInfinite, float textureSizeInUnits)
        {
            if (factor.IsEqual(1))
            {
                return target;
            }

            if (!isInfinite) return Mathf.LerpUnclamped(start, target, factor);
            
            float distance = target - start;
            float dir = Mathf.Sign(distance);
            
            int journeys = Mathf.FloorToInt(Mathf.Abs(distance / textureSizeInUnits));
            journeys += factor.IsNegative() ? 1 : 0;
            
            float targetOffset = (distance * factor) % textureSizeInUnits;
            start += (textureSizeInUnits * dir * journeys);
            return start + targetOffset;
        }


    }
}