using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace CamLib
{
    [PublicAPI]
    public static class ExtensionsMonoBehaviour
    {
        public static Coroutine TryRestartCoroutine(this MonoBehaviour behaviour, Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null)
            {
                behaviour.StopCoroutine(coroutine);
            }

            return behaviour.StartCoroutine(routine);
        }
    }
}
