#if DOTWEEN
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace CamLib
{
    /// <summary>
    /// Spawns text that moves on spawned. Can work for both World-space and UI space.
    /// </summary>
    public class PulseTextPool : MonoBehaviour
    {
        public GameObject _textPrefab;
        public int _prewarmCount = 2;
        public float _duration = 1;
        public Vector2 _destinationOffset = Vector2.up;
        public Vector2 _destinationRandomOffset = new Vector2(0.1f, 0.1f);

        private float _fontSize;
        private ObjectPool<TMP_Text> _pool;

        private void Awake()
        {
            if (!_textPrefab)
            {
                Debug.LogError("Pulse text prefab is not assigned.");
                return;
            }

            TMP_Text tmp = _textPrefab.GetComponent<TMP_Text>();
            if (!tmp)
            {
                Debug.LogError("Text component in the prefab doesn't exist.");
                return;
            }
            
            _fontSize = tmp.fontSize;
            _pool = new ObjectPool<TMP_Text>(CreateFunc, OnGet, OnRelease);
            return;

            TMP_Text CreateFunc()
            {
                GameObject obj = Instantiate(_textPrefab, transform);
                obj.SetActive(false);
                return obj.GetComponent<TMP_Text>();
            }

            void OnGet(TMP_Text obj)
            {
                obj.gameObject.SetActive(true);
            }

            void OnRelease(TMP_Text obj)
            {
                obj.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Preallocate. Defers over multiple frames to spread out the processing time.
        /// </summary>
        private IEnumerator Start()
        {
            if (_prewarmCount <= 0) yield break;
            yield return null;
            
            List<TMP_Text> list = new(_prewarmCount);
            for (int i = 0; i < _prewarmCount; i++)
            {
                yield return null;
                list.Add(_pool.Get());
            }

            foreach (TMP_Text text in list)
            {
                yield return null;
                _pool.Release(text);
            }
        }

        /// <summary>
        /// Show some text at a location
        /// </summary>
        public TMP_Text ShowText(Vector2 from, string text, Color color = default, float textSizeMultiplier = 1f)
        {
            TMP_Text instance = _pool.Get();
            instance.transform.position = from;
            instance.color = color == default ? Color.white : color;
            instance.text = text;
            instance.fontSize = _fontSize * textSizeMultiplier;
        
            Vector2 destination = new(
                from.x + _destinationOffset.x + Random.Range(-_destinationRandomOffset.x, _destinationRandomOffset.x),
                from.y + _destinationOffset.y + Random.Range(-_destinationRandomOffset.y, _destinationRandomOffset.y));

            instance.transform.DOMove(destination, _duration).OnComplete(() =>
            {
                _pool.Release(instance);
            });
            return instance;
        }
    }
}
#endif