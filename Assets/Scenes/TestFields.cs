using System.Collections.Generic;
using CamLib;
using UnityEngine;
using UnityEngine.Audio;

namespace Scenes
{
    public class TestFields : MonoBehaviour
    {
        [SerializeField, ReadOnly] private float _readonlyValue;
        [SerializeField] private Bool2 _bool2;

        //[Space]
        
        [SerializeField, AudioClipButtons] private AudioClip _clip;
        [SerializeField, AudioClipButtons] private AudioMixerGroup _clipImproper;
        
        //[Space]
        
        [SpriteRender, SerializeField] private Sprite _sprite;
        [SpriteRender, SerializeField] private Texture2D _tex2D;
        [SpriteRender, SerializeField] private Texture _tex;
        [SpriteRender, SerializeField] private GameObject _texImproper;
        
        //[Space]
        
        [SerializeField, MinMaxRange(0, 10)] private Vector2 _minMax;
        [SerializeField, MinMaxRange(0, 10)] private float _minMaxImproper;

        //[SerializeField] private Inside _inside;

        [SerializeField] private List<Inside> _insides;


        [System.Serializable]
        public class Test
        {
            [AudioClipButtons] public AudioClip _clip;
            [SpriteRender] public Sprite _spr;
        }

        [System.Serializable]
        public class Inside
        { 
            public List<Test> _tests;
            public Inside1 _inside;
        }
        [System.Serializable]
        public class Inside1
        {
            public List<Test> _tests;
            public Inside2 _inside2;
        }
        [System.Serializable]
        public class Inside2
        {
            public List<Test> _inside3;
        }


    }
}
