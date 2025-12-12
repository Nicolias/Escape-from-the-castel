using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(Image))]
    public class Light : MonoBehaviour
    {
        private Image _image;

        public virtual void Init(Color color)
        {
            _image = GetComponent<Image>();
            _image.color = color;
        }
    }

    [RequireComponent (typeof(RectTransform))]
    public class TransformableLight : Light
    {
        private RectTransform _rectTransform;

        public override void Init(Color color)
        {
            base.Init(color);
            _rectTransform = GetComponent<RectTransform>();
        }


    }
}