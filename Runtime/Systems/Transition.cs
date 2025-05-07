using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CamLib
{
    /// <summary>
    /// Simple component that will fade a screen. Uses unscaled time.
    /// You'll have to set up the image component yourself.
    /// </summary>
    public class Transition : Singleton<Transition>
    {
        public Image ScreenFade;
        public bool FadeFromBlackOnStart = true;

        private Coroutine CurrentCoroutine;
        
        private void Start()
        {
            if (FadeFromBlackOnStart)
            {
                ScreenFade.color = Color.black;
                FadeToClear();
            }
        }

        [ContextMenu("FadeToClear")]
        public void FadeToClear() => FadeToColor(Color.clear);
        
        [ContextMenu("FadeToBlack")]
        public void FadeToBlack() => FadeToColor(Color.black);
        
        [ContextMenu("FadeToWhite")]
        public void FadeToWhite() => FadeToColor(Color.white);

        public void FadeToColor(Color color, float duration = 1)
        {
            ValidateNotFading();
            StartCoroutine(DoFadeToColor(color, duration));
        }

        /// <summary>
        /// Main useful transition. Will fade from clear, to a color, and then fade back to clear. Has a callback in the middle of it.
        /// </summary>
        public void DoTransition(Color color, Action callback = null, float duration = 1, float delay = 0)
        {
            ValidateNotFading();
            StartCoroutine(DoColor());
            return;

            IEnumerator DoColor()
            {
                if (delay > 0)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }

                ScreenFade.color = Color.clear;
                yield return DoFadeToColor(color, duration);

                callback?.Invoke();
                
                yield return DoFadeToColor(color, duration);
                CurrentCoroutine = null;
            }
        }

        /// <summary>
        /// Slightly more useful scene restart that involves a fade before restart
        /// </summary>
        public void RestartScene(float duration = 1, float delay = 0)
        {
            DoTransition(Color.black, Reload, duration, delay);
            void Reload()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void ValidateNotFading()
        {
            if (CurrentCoroutine != null) Debug.LogWarning("Tried fading when already fading!");
        }
        
        private IEnumerator DoFadeToColor(Color color, float duration = 1)
        {
            Color initialColor = ScreenFade.color;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                ScreenFade.color = Color.Lerp(initialColor, color, elapsedTime / duration);
                yield return null;
            }
            ScreenFade.color = color;
        }
    }
}