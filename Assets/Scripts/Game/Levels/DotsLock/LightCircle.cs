using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class LightCircle : GameCircle<TransformableLightPoint>
    {
        private Coroutine _animateCoroutine;

        public event Action Changed;

        public void Rotate()
        {
            if (_animateCoroutine == null)
            {
                StartCoroutine(AnimateRoutine(RotateRoutine()));
            }
        }

        public void SwitchBottomItems()
        {
            if (_animateCoroutine == null)
            {
                StartCoroutine(AnimateRoutine(SwitchItemsRoutine()));
            }
        }

        private IEnumerator SwitchItemsRoutine()
        {
            Vector2 fiveIndexItemPosition = this[4].AnchoredPosition;
            Vector2 fourIndexItemPosition = this[3].AnchoredPosition;
            Vector2 thirdIndexitemPosition = this[5].AnchoredPosition;

            this[5].Move(fiveIndexItemPosition);
            this[4].Move(fourIndexItemPosition);

            yield return this[3].Move(thirdIndexitemPosition);
        }

        private IEnumerator RotateRoutine()
        {
            Quaternion targetRotation = RectTransform.rotation * Quaternion.Euler(0f, 0f, 45f);
            float rotation = 90f;

            while (RectTransform.rotation != targetRotation)
            {
                RectTransform.rotation = Quaternion.RotateTowards(RectTransform.rotation, targetRotation, Time.deltaTime * rotation);

                yield return null;
            }
        }

        private IEnumerator AnimateRoutine(IEnumerator routine)
        {
            _animateCoroutine = StartCoroutine(routine);

            yield return _animateCoroutine;

            _animateCoroutine = null;

            ResetState();
            Changed?.Invoke();
        }
    }
}