using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class LightCircle : GameCircle<TransformableLightPoint>
    {
        private Coroutine _rotateCoroutine;

        public event Action Changed;

        public bool IsMoving => _rotateCoroutine != null;

        public void Rotate()
        {
            if (IsMoving == false)
            {
                StartCoroutine(AnimateRoutine(RotateRoutine()));
            }
        }

        public void SwitchBottomItems()
        {
            if (IsMoving == false)
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

            while (RectTransform.rotation != targetRotation)
            {
                RectTransform.rotation = Quaternion.RotateTowards(RectTransform.rotation, targetRotation, Time.deltaTime * 45f * 2f);

                yield return null;
            }
        }

        private IEnumerator AnimateRoutine(IEnumerator routine)
        {
            _rotateCoroutine = StartCoroutine(routine);

            yield return _rotateCoroutine;

            _rotateCoroutine = null;

            ResetState();
            Changed?.Invoke();
        }
    }
}