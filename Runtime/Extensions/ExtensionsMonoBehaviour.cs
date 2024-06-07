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
        
        public static void SetVisible(this CanvasGroup group, bool active)
        {
            group.alpha = active ? 1 : 0;
        }
    }
}
