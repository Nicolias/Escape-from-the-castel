using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class Light : MonoBehaviour
    {
        private Image _image;

        [field: SerializeField] public Color Color { get; private set; }

        public Vector2 AnchoredPosition { get; private set; }

        public void Init()
        {
            _image = GetComponent<Image>();
            _image.color = Color;
            AnchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}