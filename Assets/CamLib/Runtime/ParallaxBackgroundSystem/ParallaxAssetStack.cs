using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(fileName = nameof(ParallaxAssetStack), menuName = ParallaxConsts.CREATE_ASSET_PATH + nameof(ParallaxAssetStack), order = 0)]
    public class ParallaxAssetStack : ScriptableObject
    {
        [Header("Highest on the list is the frontmost")]
        [SerializeField] private List<ParallaxAssetLayer> _backgrounds = null;

        public List<ParallaxAssetLayer> Backgrounds => _backgrounds;
    }
}