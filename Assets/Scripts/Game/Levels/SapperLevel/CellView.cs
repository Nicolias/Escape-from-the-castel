using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Levels.SapperLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class CellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _span;
        [SerializeField] private Image _bomb;
        [SerializeField] private Image _mark;

        [SerializeField] private float _doubleClickThreshold = 0.3f;
        [SerializeField] private float _holdThreshold = 0.5f;

        private float _lastDownTime;
        private int _clickCount = 0;

        private bool _isPressed;

        private RectTransform _rectTransform = null;

        public event Action<CellView> Clicked;

        public event Action<CellView> Pressed;

        public bool IsMarked => _mark.enabled;

        public Vector2 Position => _rectTransform.anchoredPosition;

        public void Init()
        {
            _rectTransform = _rectTransform == null ? GetComponent<RectTransform>() : _rectTransform;
            _mark.enabled = false;
            _bomb.enabled = false;
            _text.enabled = false;

            Hide();
        }

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isPressed)
                return;

            _clickCount++;

            if (_clickCount == 2)
            {
                Pressed?.Invoke(this);
                Reset();
            }
            else
            {
                Invoke(nameof(OneClickIfNoSecond), _doubleClickThreshold);
            }
        }

        private void OneClickIfNoSecond()
        {
            if (_clickCount == 1)
                Clicked?.Invoke(this);

            Reset();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _lastDownTime = Time.unscaledTime;
            _isPressed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            float duration = Time.unscaledTime - _lastDownTime;

            if (duration >= _holdThreshold)
            {
                _isPressed = true;
                Pressed?.Invoke(this);
            }
        }

        private void Reset()
        {
            _clickCount = 0;
            CancelInvoke(nameof(OneClickIfNoSecond));
        }
    }
}