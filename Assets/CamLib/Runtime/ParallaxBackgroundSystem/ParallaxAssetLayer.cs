﻿using UnityEngine;

namespace CamLib
{
    [CreateAssetMenu(fileName = nameof(ParallaxAssetLayer), menuName = "CamLib/" + nameof(ParallaxAssetLayer), order = 0)]
    public class ParallaxAssetLayer : ScriptableObject
    {
        [SpriteRender]
        [SerializeField] private Sprite _backgroundSprite = null;
        [SerializeField, SortingLayer] private int _layer = 0; //SpriteLayerDrawer for this todo
        [SerializeField, Range(0, 1)] private float _imageAlpha = 1;
        
        [Header("Alignment/Movement")]
        [Tooltip("Make this layer anchor to a specific side of the level's bounds (if available)")]
        [SerializeField] private ParallaxSnapSide _snapSide = ParallaxSnapSide.None;
        
        [SerializeField] private Vector2 _autoScrollSpeed = Vector2.zero;
        [SerializeField] private Bool2 _infinite = new Bool2(true, false);
        [SerializeField, Range(-1, 1)] private float _parallaxEffectXMultiplier = 0;
        [SerializeField, Range(-1, 1)] private float _parallaxEffectYMultiplier = 0;

        public int Layer => _layer;
        public Sprite BackgroundSprite => _backgroundSprite;
        
        public Vector2 ParallaxEffectMultiplier => new Vector2(_parallaxEffectXMultiplier, _parallaxEffectYMultiplier);
        public Bool2 Infinite => _infinite;
        public Vector2 AutoScrollSpeed => _autoScrollSpeed;
        public ParallaxSnapSide SnapSide => _snapSide;
        public float ImageAlpha => _imageAlpha;
    }
}