using DG.Tweening;
using UnityEngine;

namespace Scripts.Levels.FourCircles
{
    [RequireComponent(typeof(RectTransform))]
    public class CircleItem : MonoBehaviour
    {
        private RectTransform _rectTransform;

        public Vector2 Position => _rectTransform.anchoredPosition;
    
        public void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Move(Vector2 position)
        {
            float delay = 0.5f;

            _rectTransform.DOAnchorPos(position, delay);
        }
    }
}