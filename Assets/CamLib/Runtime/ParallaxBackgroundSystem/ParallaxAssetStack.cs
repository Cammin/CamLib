using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(fileName = nameof(ParallaxAssetStack), menuName = "CamLib/" + nameof(ParallaxAssetStack), order = 0)]
    public class ParallaxAssetStack : ScriptableObject
    {
        [Header("Lowest on the list is the front-most")]
        [SerializeField] private List<ParallaxAssetLayer> _backgrounds = null;

        public List<ParallaxAssetLayer> Backgrounds => _backgrounds;
    }
}