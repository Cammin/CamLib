using System;
using UnityEditor;
using UnityEngine;

namespace CamLib
{
    public class ParallaxLayerInstance : MonoBehaviour
    {
        private const float WRAP_SCALE = 4;
        
        [SerializeField] private SpriteRenderer _render = null;
        [SerializeField] private ParallaxAssetLayer _properties = null;
        [SerializeField] private bool _debugSetupEveryFrame;

        private Camera _cam;
        private Vector2 _camSize;

        /// <summary>
        /// origin is the position of the very starting point.
        /// </summary>
        private Vector2 _origin;
        private Vector2 _snappingOffsetRelativeToCamera; //this may not be used, but keep it just in case of emegency

        /// <summary>
        /// this constantly is added to every frame
        /// </summary>
        private Vector2 _autoScrollOffset;
        private Vector2 _spriteWorldSize = Vector2.one;

        public void SetProperties(ParallaxAssetLayer pairing) => _properties = pairing;
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
            if (_debugSetupEveryFrame)
            {
                Setup();
            }
            UpdatePosition();
            
            //DebugUtil.DrawRect(_cam.OrthographicRect());
        }

        

        private void Setup()
        {
            Sprite sprite = _properties.BackgroundSprite;
            if (sprite == null)
            {
                Debug.LogError("Sprite is null!", gameObject);
                return;
            }

            //Texture2D texture = sprite.texture;
            _spriteWorldSize = new Vector2(sprite.rect.width, sprite.rect.height) / sprite.pixelsPerUnit;

            Rect camArea = _cam.OrthographicRect();
            _camSize = camArea.size;

            //Debug.Log($"Camera Unit Size: {cameraSize}");
            //Debug.Log($"Render Size: {renderSize}");
            
            _render.sprite = sprite;
            _render.sortingLayerID = _properties.Layer;
            _render.size = GetSpriteRendererSize();
            
            SnapToCameraBounds();
        }

        
        private Vector2 GetSpriteRendererSize()
        {
            Vector2 size = Vector2.zero;
            size.x = GetSpriteRendererLength(_spriteWorldSize.x, _camSize.x, _properties.Infinite.x);
            size.y = GetSpriteRendererLength(_spriteWorldSize.y, _camSize.y, _properties.Infinite.y);
            return size;
        }
        private float GetSpriteRendererLength(float spriteWorldLength, float cameraLength, bool infinite)
        {
            if (infinite)
            {
                return Mathf.Max(cameraLength, spriteWorldLength) * WRAP_SCALE;
            }
            return spriteWorldLength;
        }

        private void SnapToCameraBounds()
        {
            ParallaxSnapSide snapSide = _properties.SnapSide;
            
            if (snapSide == ParallaxSnapSide.None)
            {
                return;
            }

            Rect camArea = _cam.OrthographicRect();
            if (camArea == Rect.zero)
            {
                return;
            }
            
            SetupOrigin(camArea, snapSide);


            float targetOffsetDirection = 0;
            switch (snapSide)
            {
                case ParallaxSnapSide.Bottom:
                case ParallaxSnapSide.Left:
                    targetOffsetDirection = 1;
                    break;
                    
                case ParallaxSnapSide.Top:
                case ParallaxSnapSide.Right:
                    targetOffsetDirection = -1;
                    break;
            }

            //const float digInMultiplier = 1.0035f;  //this extra tiny offset is to help with the anchored objects not moving up so fast that it creates a gap line within the pixel perfect camera. 
            _snappingOffsetRelativeToCamera = (_spriteWorldSize - _camSize) / 2 * targetOffsetDirection;
            
            switch (snapSide)
            {
                case ParallaxSnapSide.Top:
                case ParallaxSnapSide.Bottom:
                    if (_properties.Infinite.y) Debug.LogWarning("Snapping up or down won't look correct if the infinite's y setting is on.");
                    break;
                    
                case ParallaxSnapSide.Left:
                case ParallaxSnapSide.Right:
                    if (_properties.Infinite.x) Debug.LogWarning("Snapping left or right won't look correct if the infinite's x setting is on.");
                    break;
            }
        }

        private void SetupOrigin(Rect camArea, ParallaxSnapSide snapSide)
        {
            //bottom align with image, to bottom align with bottom of camera.
            Vector2 newOriginPos = camArea.center;
            Vector2 halfRendererSize = _spriteWorldSize / 2;
            switch (snapSide)
            {
                case ParallaxSnapSide.Left:
                    newOriginPos.x = camArea.xMin + halfRendererSize.x;
                    break;

                case ParallaxSnapSide.Right:
                    newOriginPos.x = camArea.xMax - halfRendererSize.x;
                    break;

                case ParallaxSnapSide.Top:
                    newOriginPos.y = camArea.yMax - halfRendererSize.y;
                    break;

                case ParallaxSnapSide.Bottom:
                    newOriginPos.y = camArea.yMin + halfRendererSize.y;
                    break;
            }

            _origin = newOriginPos;
        }

        private void UpdatePosition()
        {
            UpdateAutoScroll();
            
            Vector2 camPos = _cam.transform.position;
            Vector2 startPos = _origin + _autoScrollOffset;
            Vector2 solvedPos = SolveVector2(startPos, camPos);
            
            transform.position = solvedPos;
        }

        private void UpdateAutoScroll()
        {
            Vector2 scrollSpeed = _properties.AutoScrollSpeed;
            scrollSpeed.y *= 0.5f; //add extra autoscroll speed to compensate for an unknown, unexpected extra half-y value
            _autoScrollOffset += scrollSpeed * Time.deltaTime;
        }

        private Vector2 SolveVector2(Vector2 start, Vector2 target)
        {
            Vector2 newPos = Vector2.zero;
            newPos.x = SolveVector(start.x, target.x, _properties.ParallaxEffectMultiplier.x,  _properties.Infinite.x, _spriteWorldSize.x, _camSize.x);
            newPos.y = SolveVector(start.y, target.y, _properties.ParallaxEffectMultiplier.y,  _properties.Infinite.y, _spriteWorldSize.y, _camSize.y);
            return newPos;
        }

        private float SolveVector(float start, float target, float factor, bool infinite, float spriteRendererLength, float cameraLength)
        {
            if (factor.IsEqual(1))
            {
                return target;
            }

            if (!infinite)
            {
                return Mathf.LerpUnclamped(start, target, factor);
            }
            
            float direction = target - start;

            float factoredDistance = (direction * factor);
            float targetOffset = factoredDistance % spriteRendererLength;
            start += targetOffset;
            
            float abs = Mathf.Abs(direction / spriteRendererLength);
            int laps = Mathf.FloorToInt(abs);
            float directionNormalized = Mathf.Sign(direction);
            start += (spriteRendererLength * laps * directionNormalized);

            return start;
        }


    }
}