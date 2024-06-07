using TMPro;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Simple component to make text display the app version
    /// </summary>
    public class VersionText : MonoBehaviour
    {
        public string _prefix = "ver. ";
        public TMP_Text _text;

        private void Start()
        {
            _text.text = $"{_prefix}{Application.version}";
        }
    }
}