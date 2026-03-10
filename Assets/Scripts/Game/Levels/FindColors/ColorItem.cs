using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.FindColorsGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorItem : MonoBehaviour
    {
        [field: SerializeField] public Color BaseColor { get; private set; }

        private Color _disableColor = Color.black;
        private Sequence _sequence;
        private SpriteRenderer _spriteRenderer;
        private float _animationInterval = 2f;
        private float _animationTime = 0.5f;

        public event Action<ColorItem> Clicked;

        public Color CurrentColor { get; private set; }

        public void Init(Color color)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            CurrentColor = color;
        }

        private void OnMouseDown() => Clicked?.Invoke(this);

        public void Disable()
        {
            _sequence?.Kill();

            _spriteRenderer.material.DOColor(_disableColor, _animationTime).WaitForCompletion();
        }

        public IEnumerator LightUp()
        {
            _sequence = DOTween.Sequence();

            yield return _sequence
                .Append(_spriteRenderer.material.DOColor(CurrentColor, _animationTime))
                .AppendInterval(_animationInterval)
                .Append(_spriteRenderer.material.DOColor(BaseColor, _animationTime))
                .Play()
                .WaitForCompletion();

            _sequence = null;
        }
    }
}