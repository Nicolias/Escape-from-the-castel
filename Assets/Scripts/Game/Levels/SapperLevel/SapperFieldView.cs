using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Levels.SapperLevel
{
    public class SapperFieldView : MonoBehaviour
    {
        [SerializeField] private List<CellView> _cells;

        [field : SerializeField] public int FieldHeight { get; private set; }
        [field: SerializeField] public int FieldWidth { get; private set; }

        private CellView[,] _cellField;

        public event Action<Vector2Int> CellInteracted;

        public event Action<Vector2Int> BombDetected;

        public void Init()
        {
            _cellField = new CellView[FieldHeight, FieldWidth];

            foreach (CellView cell in _cells)
            {
                cell.Init();
                cell.Clicked += OnCellClicked;
                cell.Pressed += OnCellPressed;
                Vector2Int gridPosition = CalculateGridPosition(cell.Position);

                _cellField[gridPosition.x, gridPosition.y] = cell;
            }
        }

        private void OnDisable()
        {
            foreach (CellView cell in _cells)
            {
                cell.Clicked -= OnCellClicked;
                cell.Pressed -= OnCellPressed;
            }
        }

        private void OnValidate()
        {
            string errorMassae = "Неверное количество ячеек!!!";

            if (_cells.Count != FieldHeight * FieldWidth)
            {
                throw new Exception(errorMassae);
            }
        }

        public void ResetState()
        {
            foreach (CellView cell in _cells)
            {
                cell.Init();
            }
        }

        public CellView GetItem(Vector2Int gridPosition) => _cellField[gridPosition.x, gridPosition.y];

        private void OnCellClicked(CellView view) => CellInteracted?.Invoke(CalculateGridPosition(view.Position));

        private void OnCellPressed(CellView view) => BombDetected?.Invoke(CalculateGridPosition(view.Position));

        private Vector2Int CalculateGridPosition(Vector2 worldPosition)
        {
            float cellSize = 0.1f;
            float padding = 0.05f;

            int xPosition = Mathf.RoundToInt((worldPosition.x - padding) / cellSize);
            int yPosition = Mathf.RoundToInt((worldPosition.y + padding) / cellSize) * -1;

            Vector2Int gridPosition = new Vector2Int(yPosition, xPosition);

            return gridPosition;
        }    
    }
}