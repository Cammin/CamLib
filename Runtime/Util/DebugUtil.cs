using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CamLib
{
    /// <summary>
    /// Utility class to draw more things easily for debugging, like a circle.
    /// </summary>
    public static class DebugUtil
    {
        [Conditional("UNITY_EDITOR")]
        public static void DrawCross(Vector2 pos, Color color, float size = 0.2f, float duration = 0)
        {
            if (duration == 0)
            {
                duration = Time.deltaTime;
            }
            
            pos += Random.insideUnitCircle * 0.01f;
            
            Vector2 upRight = new Vector2(size, size);
            Vector2 upLeft = new Vector2(-size, size);
            
            Debug.DrawLine(pos + upRight, pos - upRight, color, duration);
            Debug.DrawLine(pos + upLeft, pos - upLeft, color, duration);
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawPoint(Vector3 position, Color color, float duration)
        {
            DrawCross(position, color, 0.1f, duration);
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawCircle(Vector3 position, float radius, Color color, float duration, int pointCount = 24)
        {
            Vector3 prevPos = Vector2.zero;
            for (int i = 0; i <= pointCount; i++)
            {
                float angle = i / (float)pointCount * (Mathf.PI * 2);
                
                var nextPoint = position + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
                
                if (i != 0)
                {
                    Debug.DrawLine(prevPos, nextPoint, color, duration, false);
                }
                
                prevPos = nextPoint;
            }
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawCone(Vector2 position, Vector2 direction, float fov, float range, Color color, float duration)
        {
            float halfFov = fov * 0.5f;
            
            Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFov, Vector3.forward );
            Quaternion rightRayRotation = Quaternion.AngleAxis( halfFov, Vector3.forward );
            
            Vector3 leftRayDirection = leftRayRotation * direction;
            Vector3 rightRayDirection = rightRayRotation * direction;
            
            Debug.DrawRay(position, leftRayDirection * range, color, duration);
            Debug.DrawRay(position, rightRayDirection * range, color, duration);
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawCircleCast(Vector3 origin, float radius, Vector3 direction, float distance, LayerMask layerMask, float duration)
        {
            DrawCircle(origin, radius, Color.white, duration);
            RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
            if (hit)
            {
                Vector3 end = origin + direction * hit.distance;
                DrawCircle(end, radius, Color.green, duration);
                DrawPoint(hit.point, Color.magenta, duration);
                Debug.DrawLine(origin, end, Color.green, duration);
            }
            else
            {
                Vector3 end = origin + direction * distance;
                DrawCircle(end, radius, Color.red, duration);
                Debug.DrawLine(origin, end, Color.red, duration);
            }
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawBounds(Bounds bounds, Color color, float duration)
        {
            DrawRect(new Rect(bounds.min, bounds.size), color, duration);
        }
        
        [Conditional("UNITY_EDITOR")]
        public static void DrawRect(Rect rect, Color color, float duration)
        {
            Vector2 min = rect.min;
            Vector2 max = rect.max;
            Vector2 tl = new Vector2(min.x, max.y);
            Vector2 br = new Vector2(max.x, min.y);
            Debug.DrawLine(min, br, color, duration);
            Debug.DrawLine(br, max, color, duration);
            Debug.DrawLine(max, tl, color, duration);
            Debug.DrawLine(tl, min, color, duration);
        }
    }

    public static class HandlesUtil
    {
        [Conditional("UNITY_EDITOR")]
        public static void DrawText(Vector2 pos, string txt)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = new Color(0.2f,0.2f,0.2f,1);
            UnityEditor.Handles.Label(pos, txt);
#endif
        }
    } 
}