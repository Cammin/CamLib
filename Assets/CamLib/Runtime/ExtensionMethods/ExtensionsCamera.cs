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
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector2 pos = cam.transform.position;
            Vector2 position = new Vector2(pos.x - halfWidth, pos.y + halfHeight);
            return new Rect(position, size);
        }
    }
}