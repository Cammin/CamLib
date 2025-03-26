using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsTransform
    {
        /// <summary>
        /// Conserves Z
        /// </summary>
        public static void SetXY(this Transform transform, Vector3 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
        
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        
        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static void SetLocalX(this Transform transform, float x)
        {
	        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
        
        public static void SetLocalY(this Transform transform, float y)
        {
	        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }
        
        public static void SetLocalZ(this Transform transform, float z)
        {
	        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }
    }
}