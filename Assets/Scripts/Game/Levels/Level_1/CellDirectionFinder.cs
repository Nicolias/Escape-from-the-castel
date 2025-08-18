using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.Levels.Level_1
{
    public class CellDirectionFinder : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
    {
        private Vector2 _lastMousePosition;
        private Vector2 _currentMousePosition;
        private bool _isMouseDown = false;

        public event Action<Direction> DirectionFound;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isMouseDown = true;
            _lastMousePosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isMouseDown == false) return;

            _currentMousePosition = eventData.position;

            Vector2 direction = (_currentMousePosition - _lastMousePosition).normalized;

            _lastMousePosition = _currentMousePosition;

            _isMouseDown = false;

            DirectionFound?.Invoke(GetDirection8(direction));
        }

        private Direction GetDirection8(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (angle < 0) angle += 360;

            if (angle < 22.5f || angle > 337.5f) return Direction.Right;
            if (angle < 67.5f) return Direction.UpRight;
            if (angle < 112.5f) return Direction.Up;
            if (angle < 157.5f) return Direction.UpLeft;
            if (angle < 202.5f) return Direction.Left;
            if (angle < 247.5f) return Direction.DownLeft;
            if (angle < 292.5f) return Direction.Down;
            return Direction.DownRight;
        }
    }
}