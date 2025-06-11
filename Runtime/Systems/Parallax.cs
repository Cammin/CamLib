using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace CamLib
{
    /// <summary>
    /// Simple parallax element that moves at a factor relative to the main camera (or a custom transform)
    /// </summary>
    public sealed class Parallax : MonoBehaviour
    {
        [Tooltip("Whether to use FixedUpdate; otherwise LateUpdate")]
        public bool useFixedUpdate;
        [Tooltip("1 moves with the camera, 0 doesn't move. -1 moves opposite for foreground")]
        public Vector2 parallaxFactor = new Vector2(1, 1);
        
        private Transform _cameraTransform;
        private Vector3 _prevPos;
        private Vector3 _delta;

        private void Start()
        {
            _prevPos = transform.position;
            
            //If already cached from external code, don't fight with it
            if (_cameraTransform) return;
            
            Camera cam = Camera.main;
            if (cam)
            {
                SetCamera(cam.transform);
            }
        }
        
        [PublicAPI]
        public void SetCamera(Transform cam)
        {
            _cameraTransform = cam;
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
            {
                UpdateParallax();
            }
        }
        private void LateUpdate()
        {
            if (!useFixedUpdate)
            {
                UpdateParallax();
            }
        }

        private void UpdateParallax()
        {
            if (!_cameraTransform) return;
            if (parallaxFactor == Vector2.zero) return;

            Vector3 cameraPos = _cameraTransform.position;
	        
            _delta = cameraPos - _prevPos;
            transform.position += Vector3.Scale(_delta, parallaxFactor);
            _prevPos = cameraPos;
        }
    }
}