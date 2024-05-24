using System;
using UnityEngine;

namespace CamLib
{
    public class ParallaxStackBuilder : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        //having a prefab here means that we could configure the sprite renderer in any way we want
        [SerializeField] private ParallaxLayerInstance _prefab = null;

        public void MakeLayers(ParallaxAssetStack stack)
        {
            if (stack == null)
            {
                Debug.LogWarning("Tried making background, but StackCollection was null. Intentional?");
                return;
            }

            if (_prefab == null)
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
                ParallaxAssetLayer layer = stack.Backgrounds[i];
                if (layer == null)
                {
                    continue;
                }
                MakeLayer(layer, i);
            }
        }

        private void MakeLayer(ParallaxAssetLayer pairing, int i)
        {
            string layerName = SortingLayer.IDToName(pairing.Layer);
            
            ParallaxLayerInstance bg = Instantiate(_prefab, transform);
            bg.gameObject.name = $"{pairing.BackgroundSprite.name}";
            bg.SetProperties(pairing);
            bg.SetSortingOrder(i);
            bg.SetCamera(_camera);
            bg.SetAlpha(pairing.ImageAlpha);
        }
    }
}