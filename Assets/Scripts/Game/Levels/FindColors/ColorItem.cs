using System;
using UnityEngine;

namespace Assets.Scripts.FindColorsGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Light))]
    public class ColorItem : MonoBehaviour
    {
        [field: SerializeField] public Color BaseColor { get; private set; }

        private Color _disableColor = Color.black;
        private Light _light;
        private SpriteRenderer _spriteRenderer;

        public event Action<ColorItem> Clicked;

        public Color Color { get; private set; }

        public void Init(Color color)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _light = GetComponent<Light>();
            Color = color;
            _light.enabled = false;
        }

        private void OnMouseDown()
        {
            Clicked?.Invoke(this);
        }

        public void Disable()
        {
            _light.enabled = false;
            _spriteRenderer.material.color = _disableColor;
            StopAllCoroutines();    
        }

        public void On()
        {
            _spriteRenderer.material.color = Color;
            _light.color = Color;
            _light.enabled = true;
        }

        public void Off()
        {
            _spriteRenderer.material.color = BaseColor;
            _light.enabled = false;
        }
    }
}