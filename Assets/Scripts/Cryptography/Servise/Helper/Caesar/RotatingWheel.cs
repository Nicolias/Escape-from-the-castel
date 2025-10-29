using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cryptography.Servis.Caesar
{
    public class RotatingWheel : MonoBehaviour
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        [SerializeField] private CircularText _wheal;

        private float _angle = 0f;
        private bool _dragging = false;
        private Vector2 _lastMousePos;

        private void OnEnable()
        {
            _leftButton.onClick.AddListener(MoveLeft);
            _rightButton.onClick.AddListener(MoveRight);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveListener(MoveLeft);
            _rightButton.onClick.RemoveListener(MoveRight);
        }

        void Update()
        {
            if (_dragging)
            {
                Vector2 currentMousePos = Input.mousePosition;
                Vector2 centerScreen = RectTransformUtility.WorldToScreenPoint(null, transform.position);

                float prevAngle = Mathf.Atan2(_lastMousePos.y - centerScreen.y, _lastMousePos.x - centerScreen.x) * Mathf.Rad2Deg;
                float curAngle = Mathf.Atan2(currentMousePos.y - centerScreen.y, currentMousePos.x - centerScreen.x) * Mathf.Rad2Deg;

                float delta = curAngle - prevAngle;
                _angle += delta;

                transform.rotation = Quaternion.Euler(0, 0, _angle);
                _lastMousePos = currentMousePos;
            }
        }

        private void MoveLeft()
        {
            _angle += _wheal.CharStep * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        private void MoveRight()
        {
            _angle -= _wheal.CharStep * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}