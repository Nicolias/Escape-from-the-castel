using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Levels.SapperLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class CellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _span;
        [SerializeField] private Image _bomb;
        [SerializeField] private Image _mark;

        private RectTransform _rectTransform = null;
        private bool _isPressed;

        public event Action<CellView> Clicked;

        public event Action<CellView> Pressed;

        public Vector2 Position => _rectTransform.anchoredPosition;

        public void Init()
        {
            _rectTransform = _rectTransform == null ? GetComponent<RectTransform>() : _rectTransform;
            _mark.enabled = false;
            _bomb.enabled = false;
            _text.enabled = false;

            Hide();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;

            StartCoroutine(GetInputRoutine());
        }

        public void OnPointerUp(PointerEventData eventData) => _isPressed = false;

        public void SetMarkState(bool enable) => _mark.enabled = enable;

        public void Show() => _span.enabled = false;

        public void Hide() => _span.enabled = true;

        public void SetBombState()
        {
            _text.enabled = false;
            _bomb.enabled = true;
            _mark.enabled = false;
        }

        public void SetDigitState(string text)
        {
            _bomb.enabled = false;
            _text.enabled = true;
            _text.text = text;
            _mark.enabled = false;
        }

        private IEnumerator GetInputRoutine()
        {
            float clickTimer = 0f;
            float timeLimit = 0.3f;

            while (_isPressed == true && clickTimer < timeLimit)
            {
                clickTimer += Time.deltaTime;
                 yield return null;
            }

            if (clickTimer < timeLimit)
            {
                Clicked?.Invoke(this);
            }
            else
            {
                Pressed?.Invoke(this);
            }
        }
    }
}