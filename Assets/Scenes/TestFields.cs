using CamLib.Attributes;
using CamLib.DataTypes;
using UnityEngine;

namespace CamLib.Scenes
{
    public class TestFields : MonoBehaviour
    {
        [SerializeField] private Bool2 _bool2;
        
        [AudioClipButtons]
        [SerializeField] private AudioClip clip;
        
        [SpriteRender]
        [SerializeField] private Sprite sprite;
    }
}
