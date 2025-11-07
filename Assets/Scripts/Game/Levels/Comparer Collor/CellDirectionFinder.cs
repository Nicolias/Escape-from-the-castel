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

            if (angle < 11.25f || angle > 348.75f) return Direction.Right;
            if (angle < 33.75f) return Direction.RightUpRight;
            if (angle < 56.25f) return Direction.UpRight;
            if (angle < 78.75f) return Direction.UpUpRight;
            if (angle < 101.25f) return Direction.Up;
            if (angle < 123.75f) return Direction.UpUpLeft;
            if (angle < 146.25f) return Direction.UpLeft;
            if (angle < 168.75f) return Direction.LeftUpLeft;
            if (angle < 191.25f) return Direction.Left;
            if (angle < 213.75f) return Direction.LeftDownLeft;
            if (angle < 236.25f) return Direction.DownLeft;
            if (angle < 258.75f) return Direction.DownDownLeft;
            if (angle < 281.25f) return Direction.Down;
            if (angle < 303.75f) return Direction.DownDownRight;
            if (angle < 326.25f) return Direction.DownRight;
            return Direction.RightDownRight;
        }
    }
}