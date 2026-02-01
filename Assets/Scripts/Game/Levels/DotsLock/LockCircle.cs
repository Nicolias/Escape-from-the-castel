using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    public class LockCircle : GameCircle<LockItem>
    {
        private Coroutine _animationCoroutine;

        public IEnumerator ValidateItems(LightCircle items, Action onValidAction)
        {
            bool isValidState = true;

            for (int i = 0; i < items.Count; i++)
            {
                if (ValidateItem(i, items[i]) == false)
                {
                    isValidState = false;
                }
            }

            while (_animationCoroutine != null)
            {
                yield return null;
            }

            if (isValidState)
            {
                onValidAction?.Invoke();
            }
        }

        private bool ValidateItem(int index, CirclePoint circlePoint)
        {
            LockItem item = this[index];

            if (item.IsActive == false)
            {
                return true;
            }

            if (circlePoint.IsActive == false)
            {
                StartCoroutine(AnimateShutter(item.CloseShutter()));

                return false;
            }

            if (item.Color == circlePoint.Color)
            {
                StartCoroutine(AnimateShutter(item.OpenShutter()));

                return true;
            }
            else
            {
                StartCoroutine(AnimateShutter(item.CloseShutter()));

                return false;
            }
        }

        private IEnumerator AnimateShutter(IEnumerator routine)
        {
            _animationCoroutine = StartCoroutine(routine);

            yield return _animationCoroutine;

            _animationCoroutine = null;
        }
    }
}