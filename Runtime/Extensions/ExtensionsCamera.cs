using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class CameraExtensions
    {
        public static Vector2 OrthographicSize(this Camera cam)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            return new Vector2(width, height);
        }
        
        public static Rect OrthographicRect(this Camera cam)
        {
            Vector2 size = OrthographicSize(cam);
            float halfWidth = size.x * 0.5f;
            float halfHeight = size.y * 0.5f;
            Vector2 pos = cam.transform.position;
            Vector2 bottomLeft = new Vector2(pos.x - halfWidth, pos.y - halfHeight);
            return new Rect(bottomLeft, size);
        }
        
        public static bool IsPointInView(this Camera cam, Vector2 pos)
        {
            Vector3 view = cam.WorldToViewportPoint(pos);
            return view.z > 0 &&
                   view.x >= 0 && view.x <= 1 &&
                   view.y >= 0 && view.y <= 1;
        }
    }
}