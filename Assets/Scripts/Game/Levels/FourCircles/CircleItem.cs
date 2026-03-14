using DG.Tweening;
using System.Collections;
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

        public IEnumerator Move(Vector2 position)
        {
            float duration = 0.5f;

            yield return _rectTransform.DOAnchorPos(position, duration).WaitForCompletion();
        }
    }
}