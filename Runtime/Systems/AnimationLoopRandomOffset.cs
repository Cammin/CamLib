using UnityEngine;
using Random = UnityEngine.Random;

namespace CamLib
{
    /// <summary>
    /// Simple component that Makes an animator play with a random offset. For example, if multiple flags placed next to each other should have variety 
    /// </summary>
    public class AnimationLoopRandomOffset : MonoBehaviour
    {
        [SerializeField] private Animator _anim = null;
        [SerializeField] private string _animName = "";

        private void Start()
        {
            if (_anim != null)
            {
                _anim.Play(_animName, 0, Random.value);
            }
        }
    }
}