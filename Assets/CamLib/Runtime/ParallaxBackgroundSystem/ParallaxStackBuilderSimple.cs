using UnityEngine;

namespace CamLib
{
    public class ParallaxStackBuilderSimple : MonoBehaviour
    {
        [SerializeField] private ParallaxStackBuilder _builder;
        [SerializeField] private ParallaxAssetStack _stack;
        private void Start()
        {
            _builder.MakeLayers(_stack);
        }
    }
}