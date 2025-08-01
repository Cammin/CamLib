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