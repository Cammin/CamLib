#if CAMLIB_URP
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsLight2D
    {
        public static bool IsInLight(this Light2D light, Rect area, LayerMask? mask = null, Vector2Int divisions = new Vector2Int())
        {
            divisions += Vector2Int.one;
            for (int x = 0; x <= divisions.x; x++)
            {
                float interpX = Mathf.Lerp(area.xMin, area.xMax, (float)x / divisions.x);
                for (int y = 0; y <= divisions.y; y++)
                {
                    float interpY = Mathf.Lerp(area.yMin, area.yMax, (float)y / divisions.y);
                    Vector2 pos = new Vector2(interpX, interpY);
                    if (light.IsInLight(pos, mask))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public static bool IsInLight(this Light2D light, Vector2 pos, LayerMask? mask = null)
        {
            if (!light.gameObject.activeInHierarchy)
            {
                return false;
            }
            if (!light.enabled)
            {
                return false;
            }
            if (light.intensity <= 0f)
            {
                return false;
            }
		    
            //range
            float range = light.pointLightOuterRadius;
            float distance = Vector2.Distance(light.transform.position, pos);
            if (distance > range)
            {
                return false;
            }

            //angle
            Transform trans = light.transform;
            Vector2 dirToDest = pos - (Vector2)trans.position;
            float angle = light.pointLightOuterAngle * 0.5f;
            float lightAngle = Vector2.Angle(trans.up, dirToDest);
            if (Mathf.Abs(lightAngle) >= angle)
            {
                return false;
            }

            //physics
            if (mask == null)
            {
                return true;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(light.transform.position, dirToDest, distance, mask.Value);
            if (!hit)
            {
                return true;
            }
            
            if (hit.distance < distance)
            {
                return false;
            }
            
            return true;
        }
    }
}
#endif
