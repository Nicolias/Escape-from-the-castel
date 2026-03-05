using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Levels.MatrixGame
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class SelectionBorder : MonoBehaviour
    {
        [SerializeField] private RectTransform _rawSelection;
        [SerializeField] private RectTransform _supportedSelection;

        private Image _borderImage;
        private RectTransform _rectTransform;
        private Action<Vector2> MoveSelectionAction;
        private float _actionDurationn = 0.1f;
        private float _rawSelectionScale = 0.5f;
        private Quaternion _selectionRawRotation = Quaternion.Euler(0f, 0f, 90f);

        public void Init()
        {
            _borderImage = _borderImage == null ? GetComponent<Image>() : _borderImage;
            _rectTransform = _rectTransform == null ? GetComponent<RectTransform>() : _rectTransform;
        }

        public void SetRawState(Vector2 pointer) => SetSelectionState(new Vector2(_rawSelectionScale, pointer.y), new Vector2(pointer.x, -_rawSelectionScale), SetRawSelection);

        public void SetColumnState(Vector2 pointer) => SetSelectionState(new Vector2(pointer.x, -_rawSelectionScale), new Vector2(_rawSelectionScale, pointer.y), SetColumnSelection);

        public void MovePointer(Vector2 pointer)
        {
            MoveTo(pointer);
            MoveSelectionAction?.Invoke(pointer);
        }

        private void SetRawSelection(Vector2 position) => _supportedSelection.DOAnchorPos(new Vector2(position.x, -_rawSelectionScale), _actionDurationn);

        private void SetColumnSelection(Vector2 position) => _supportedSelection.DOAnchorPos(new Vector2(_rawSelectionScale, position.y), _actionDurationn);

        private void MoveTo(Vector2 position) => _rectTransform.DOAnchorPos(position, _actionDurationn);

        private void SetSelectionState(Vector2 rawPosition, Vector2 selectionPosition, Action<Vector2> moveSelectionAction)
        {
            _selectionRawRotation *= Quaternion.Euler(0f, 0f, 90f);
            MoveSelectionAction = moveSelectionAction;

            TransformSelectionRaw(rawPosition, _selectionRawRotation);
            _supportedSelection.DOAnchorPos(selectionPosition, _actionDurationn);
            _supportedSelection.DORotateQuaternion(_selectionRawRotation * Quaternion.Euler(0f, 0f, 90f), _actionDurationn);
        }

        private void TransformSelectionRaw(Vector2 position, Quaternion rotation)
        {
            float duration = 0.5f;

            _rawSelection.DOAnchorPos(position, duration);
            _rawSelection.DORotateQuaternion(rotation, duration);
        }
    }
}