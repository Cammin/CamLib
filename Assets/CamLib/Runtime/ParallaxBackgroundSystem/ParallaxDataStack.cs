using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(fileName = nameof(ParallaxDataStack), menuName = ParallaxConsts.CREATE_ASSET_PATH + nameof(ParallaxDataStack), order = 0)]
    public class ParallaxDataStack : ScriptableObject
    {
        [Header("Highest on the list is the frontmost")]
        [SerializeField] private List<ParallaxDataLayer> _backgrounds = null;

        public List<ParallaxDataLayer> Backgrounds => _backgrounds;
    }
}