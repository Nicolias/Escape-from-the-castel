using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Levels.FourCircles
{
    public class FourCirclesLevel : Level
    {
        [SerializeField] private List<CircleItem> _circlesItems;
        [SerializeField] private List<CircleItem> _targetItems;
        [SerializeField] private List<Vector2> _targetPositions;
        [SerializeField] private List<CircleButton> _circleBbuttons;

        private Vector2 _leftTopPoint;
        private Coroutine _rotateCoroutine;

        public override event Action Complete;

        public override void Init()
        {
            foreach (CircleButton button in _circleBbuttons)
            {
                button.Init();
                button.Clicked += OnButtonClicked;
            }

            foreach (CircleItem item in _circlesItems)
            {
                item.Init();
            }

            _leftTopPoint = GetBounds();
        }

        private void OnDisable()
        {
            foreach(CircleButton button in _circleBbuttons)
            {
                button.Clicked -= OnButtonClicked;
            }
        }

        private void OnButtonClicked(Circle circle)
        {
            if (_rotateCoroutine == null)
            {
                _rotateCoroutine = StartCoroutine(CircleInteractRoutine(circle));
            }
        }

        private IEnumerator CircleInteractRoutine(Circle circle)
        {
            yield return circle.RotateChilds(GetNearestItems(circle));

            _rotateCoroutine = null;

            CheckWin();
        }

        private void CheckWin()
        {
            for (int i = 0; i < _targetItems.Count; i++)
            {
                if (CalculateIntegerPosition(_targetItems[i].Position) != CalculateIntegerPosition(_targetPositions[i]))
                {
                    return;
                }
            }

            Complete?.Invoke();
        }

        private CircleItem[] GetNearestItems(Circle circle)
        {
            int nearestCount = 4;
            CircleItemComparer circleItemComparer = new CircleItemComparer(circle);
            _circlesItems.Sort(circleItemComparer);

            return _circlesItems.Take(nearestCount).ToArray();
        }

        private Vector2 GetBounds()
        {
            float minX = float.MaxValue;
            float maxY = float.MinValue;

            foreach (CircleItem item in _circlesItems)
            {
                minX = item.Position.x < minX ? item.Position.x : minX;
                maxY = item.Position.y > maxY ? item.Position.y : maxY;
            }

            return new Vector2(minX, maxY);
        }

        private Vector2Int CalculateIntegerPosition(Vector2 worldPosition)
        {
            int xPosition = Mathf.RoundToInt((worldPosition.x - _leftTopPoint.x) / 0.1f);
            int yPosition = Mathf.RoundToInt((_leftTopPoint.y - worldPosition.y) / 0.1f);

            return new Vector2Int(yPosition, xPosition);
        }
    }
}
