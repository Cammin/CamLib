using UnityEngine;

namespace CamLib
{
    public static class CameraExtensions
    {
        public static Vector2 OrthographicSize(this Camera cam)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            return new Vector2(width, height);
        }
    }
}