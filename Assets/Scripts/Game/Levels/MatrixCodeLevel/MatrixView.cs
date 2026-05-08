using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Levels.MatrixGame
{
    public class MatrixView : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private List<MatrixItem> _items;
        [SerializeField] private SelectionBorder _selection;

        private RectTransform _rectTransform;
        private MatrixItemsGrid _matrixItemsGrid;
        
        public event Action<Vector2Int> Interacted;

        public void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
            _selection.Init();

            foreach (MatrixItem item in _items)
            {
                item.Init();
                item.Clicked += OnItemClicked;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, Camera.main, out Vector2 localPoint);

            Vector2Int matrixPosition = _matrixItemsGrid.CalculateMatrixPosition(localPoint);

            if (_matrixItemsGrid.TrySetPointerPosition(matrixPosition))
            {
                _selection.MovePointer(_matrixItemsGrid.AllItems[_matrixItemsGrid.PointerPosition].Position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_matrixItemsGrid.SelectedPositions.Contains(_matrixItemsGrid.PointerPosition) == false)
            {
                Interacted?.Invoke(_matrixItemsGrid.PointerPosition);
            }
        }

        public void SetState(MatrixCode matrix)
        {
            _matrixItemsGrid = new MatrixItemsGrid(_items);

            foreach (Vector2Int position in _matrixItemsGrid.AllItems.Keys)
            {
                MatrixItem itemMatrixPosition = _matrixItemsGrid.AllItems[position];
                itemMatrixPosition.SetText(matrix.GetItem(position.y, position.x));
            }

            ActivateRaw(0);
        }

        public void ExcludeItem(Vector2Int position)
        {
            _matrixItemsGrid.AllItems[position].ActivateSpan();
            _matrixItemsGrid.SelectedPositions.Add(position);
        }

        public void ActivateRaw(int value) => ActivateMatrixItems(value, _matrixItemsGrid.ActivateRaw, _selection.SetRawState);

        public void ActivateColumn(int value) => ActivateMatrixItems(value, _matrixItemsGrid.ActivateColumn, _selection.SetColumnState);

        private void ActivateMatrixItems(int value, Action<int> activateTarget, Action<Vector2> activateSelectionState)
        {
            Vector2 position = _matrixItemsGrid.AllItems[_matrixItemsGrid.PointerPosition].Position;
            activateTarget?.Invoke(value);
            activateSelectionState?.Invoke(position);
            _selection.MovePointer(position);
        }

        private void OnItemClicked(MatrixItem item)
        {
            Vector2Int position = _matrixItemsGrid.CalculateMatrixPosition(item.Position);

            if (_matrixItemsGrid.TrySetPointerPosition(position))
            {
                Interacted?.Invoke(_matrixItemsGrid.PointerPosition);
            }
        }
    }
}