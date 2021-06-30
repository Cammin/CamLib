using System.Collections;
using UnityEngine;

namespace CamLib
{
    public static class MonoBehaviourExtensions
    {
        public static Coroutine RestartCoroutine(this MonoBehaviour behaviour, Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null)
            {
                behaviour.StopCoroutine(coroutine);
            }

            return behaviour.StartCoroutine(routine);
        }
    }
}
