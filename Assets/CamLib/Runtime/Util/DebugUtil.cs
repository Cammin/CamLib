using UnityEditor;
using UnityEngine;

namespace CamLib
{
    public static class DebugUtil
    {
        public static void DrawRect(Rect rect)
        {
            Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y ),Color.green);
            Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x , rect.y + rect.height), Color.red);
            Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x + rect.width, rect.y), Color.green);
            Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height), Color.red);
        }
        public static void DrawCross(Vector2 pos, Color color, float size = 0.2f)
        {
            pos += Random.insideUnitCircle * 0.01f;
            
            Vector2 upRight = new Vector2(size, size);
            Vector2 upLeft = new Vector2(-size, size);
            
            Debug.DrawLine(pos + upRight, pos - upRight, color);
            Debug.DrawLine(pos + upLeft, pos - upLeft, color);
        }
        public static void DrawText(Vector2 pos, string txt)
        {
            Handles.color = new Color(0.2f,0.2f,0.2f,1);
            Handles.Label(pos, txt);
        }
    }
}