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
        public bool _onlyInDevBuild;

        private void Start()
        {
            if (_onlyInDevBuild && !Debug.isDebugBuild)
            {
                gameObject.SetActive(false);
                return;
            }
            
            _text.text = $"{_prefix}{Application.version}";
        }
    }
}