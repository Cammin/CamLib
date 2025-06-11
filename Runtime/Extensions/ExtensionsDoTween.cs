#if DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace CamLib
{
    public static class ExtensionsDoTween
    {
        public static Tweener DOAnchorPos(this RectTransform target, Vector2 endValue, float duration)
        {
            return DOTween.To(() => target.anchoredPosition,
                x => target.anchoredPosition = x,
                endValue,
                duration);
        }
        
        public static Tweener DOAnchorPosX(this RectTransform target, float endValue, float duration)
        {
            return DOTween.To(() => target.anchoredPosition.x,
                x => target.anchoredPosition = new Vector2(x, target.anchoredPosition.y),
                endValue,
                duration);
        }
        
        public static Tweener DOAnchorPosY(this RectTransform target, float endValue, float duration)
        {
            return DOTween.To(() => target.anchoredPosition.y,
                y => target.anchoredPosition = new Vector2(target.anchoredPosition.x, y),
                endValue,
                duration);
        }

        public static Tweener DOFade(this Image image, float endValue, float duration)
        {
            return DOTween.To(() => image.color.a,
                alpha => image.color = new Color(image.color.r, image.color.g, image.color.b, alpha),
                endValue,
                duration);
        }
        
        public static Tweener DOColor(this Image image, Color endValue, float duration)
        {
            return DOTween.To(() => image.color,
                color => image.color = color,
                endValue,
                duration);
        }
        
        public static Tweener DOFade(this CanvasGroup group, float endValue, float duration)
        {
            return DOTween.To(() => group.alpha,
                color => group.alpha = color,
                endValue,
                duration);
        }

        #if CAMLIB_RP
        public static Tweener DoFade(this UnityEngine.Rendering.Volume target, float endValue, float duration, bool snapping = false)
        {
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.weight, x => target.weight = x, endValue, duration);
            t.SetOptions(snapping).SetTarget(target);
            return t;
        }
        #endif
    }
}
#endif