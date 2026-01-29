using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class CirclePoint : MonoBehaviour
    {
        [SerializeField] private Image _image;

        protected RectTransform RectTransform;

        [field : SerializeField] public Color Color { get; private set; }

        [field : SerializeField] public bool IsActive { get; private set; }

        public Vector2 AnchoredPosition => RectTransform.anchoredPosition;

        public virtual void Init()
        {
            RectTransform = GetComponent<RectTransform>();
            _image.color = Color;
            _image.enabled = IsActive;
        }
    }
}