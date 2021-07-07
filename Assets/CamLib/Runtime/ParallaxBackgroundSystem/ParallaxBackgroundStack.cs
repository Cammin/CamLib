using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(fileName = nameof(ParallaxBackgroundStack), menuName = ParallaxBackgroundConstPath.CREATE_ASSET_PATH + nameof(ParallaxBackgroundStack), order = 0)]
    public class ParallaxBackgroundStack : ScriptableObject
    {
        [Header("Highest on the list is the frontmost")]
        [SerializeField] private List<ParallaxBackgroundLayer> _backgrounds = null;

        public List<ParallaxBackgroundLayer> Backgrounds => _backgrounds;
    }
}