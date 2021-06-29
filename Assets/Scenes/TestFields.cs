using UnityEngine;

namespace CamLib.Scenes
{
    public class TestFields : MonoBehaviour
    {
        [AudioClipButtons]
        [SerializeField] private AudioClip clip;
        
        [SpriteRender]
        [SerializeField] private Sprite sprite;
    }
}
