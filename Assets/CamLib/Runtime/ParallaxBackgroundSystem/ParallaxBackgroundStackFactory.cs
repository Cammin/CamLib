using System;
using UnityEngine;

namespace CamLib
{
    public class ParallaxBackgroundStackFactory : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ParallaxBackgroundLayerInstance _instanceTemplate = null;
        [SerializeField] private ParallaxBackgroundStack _stack;
        private void Start()
        {
            MakeLayers(_stack); //todo maybe control this outside the factory? for now, its run by itself
        }

        public void MakeLayers(ParallaxBackgroundStack stack)
        {
            if (stack == null)
            {
                Debug.LogWarning("Tried making background, but StackCollection was null. Intentional?");
                return;
            }

            if (_camera == null)
            {
                Debug.LogWarning("Tried making background, but Prefab was null.");
                return;
            }
            
            if (_camera == null)
            {
                Debug.LogWarning("Tried making background, but Camera was null.");
                return;
            }


            for (int i = 0; i < stack.Backgrounds.Count; i++)
            {
                ParallaxBackgroundLayer pairing = stack.Backgrounds[i];
                MakeLayer(pairing, i);
            }
        }

        private void MakeLayer(ParallaxBackgroundLayer pairing, int i)
        {
            string layerName = SortingLayer.IDToName(pairing.Layer);
            
            ParallaxBackgroundLayerInstance bg = Instantiate(_instanceTemplate, transform);
            bg.gameObject.name = $"{layerName} {i}";
            bg.SetProperties(pairing);
            bg.SetSortingOrder(i);
            bg.SetCamera(_camera);
            bg.SetAlpha(pairing.ImageAlpha);
        }
    }
}