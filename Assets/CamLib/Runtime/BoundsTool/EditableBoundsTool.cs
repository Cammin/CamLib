using UnityEngine;

namespace CamLib
{
    public class EditableBoundsTool : MonoBehaviour
    {
        [SerializeField] private bool _drawBounds = false;
        [SerializeField] private Color _drawBoundsColor = Color.white;

        public bool DrawBounds => _drawBounds;

        public Color DrawBoundsColor => _drawBoundsColor;
    }
}
