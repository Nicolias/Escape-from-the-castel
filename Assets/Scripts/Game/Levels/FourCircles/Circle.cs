using System.Collections;
using UnityEngine;

namespace Scripts.Levels.FourCircles
{
    [RequireComponent(typeof(RectTransform))]
    public class Circle : MonoBehaviour
    {
        private RectTransform _rectTransform;

        public Vector2 Position => _rectTransform.anchoredPosition;

        public void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public IEnumerator RotateChilds(CircleItem[] items)
        {
            Coroutine currentItemRoutine = null;

            foreach (var item in items)
            {
                Vector2 resultDirection = (Vector2)(Quaternion.Euler(0, 0, 90) * (_rectTransform.anchoredPosition - item.Position));
                Vector2 position = _rectTransform.anchoredPosition + resultDirection;

                currentItemRoutine = item.StartCoroutine(item.Move(position));
            }

            yield return currentItemRoutine;
        }
    }
}