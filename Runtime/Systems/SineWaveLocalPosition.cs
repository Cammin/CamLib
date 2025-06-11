using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Creates repeating movement. Good for creating some quick simple visual movements. 
    /// </summary>
    public class SineWaveLocalPosition : MonoBehaviour
    {
        [Tooltip("How far it extents")] 
        public Vector2 _amplitude = Vector2.one;
        [Tooltip("How quickly does it move back and forth")] 
        public Vector2 _oscillation = Vector2.one;
        [Tooltip("Offset the movement relative to other objects")] 
        public Vector2 _oscillationOffset;

        public float _time;

        private void LateUpdate()
        {
            _time += Time.deltaTime;
            
            float x = Mathf.Sin(_time * _oscillation.x + _oscillationOffset.x) * _amplitude.x;
            float y = Mathf.Sin(_time * _oscillation.y + _oscillationOffset.y) * _amplitude.y;
            transform.localPosition = new Vector2(x, y);
        }

        public void ResetTime()
        {
            _time = 0;
        }
    }
}