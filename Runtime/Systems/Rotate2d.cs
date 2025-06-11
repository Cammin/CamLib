using UnityEngine;
using UnityEngine.Serialization;

namespace CamLib
{
    /// <summary>
    /// Simply rotates constantly.
    /// </summary>
    public class Rotate2d : MonoBehaviour
    {
        public float rotationSpeed = 360f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}