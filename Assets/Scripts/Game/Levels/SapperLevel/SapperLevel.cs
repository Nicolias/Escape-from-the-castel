using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Levels.SapperLevel
{
    public class SapperLevel : Level
    {
        [SerializeField] private SapperFieldView _fieldView;
        [SerializeField] private BombCounter _bombCounter;

        private SapperField _field;
        private List<Vector2Int> _markedPositions;
        private HashSet<Vector2Int> _touchedPositions;

        public override event Action Complet;

        public override void Init()
        {
            _fieldView.Init();
            _fieldView.CellInteracted += OnCellInteracted;
            _fieldView.BombDetected += OnBombDetected;

            Restart();
        }

        private void Restart()
        {
            _field = new SapperField(_fieldView.FieldHeight, _fieldView.FieldWidth, 7);
            _markedPositions = new List<Vector2Int>();
            _touchedPositions = new HashSet<Vector2Int>();
            _fieldView.ResetState();
            _bombCounter.Init(_field.BombMap.Count);

            SetView();
        }

        private void OnBombDetected(Vector2Int position)
        {
            if (_touchedPositions.Contains(position))
            {
                return;
            }

            if (_markedPositions.Contains(position))
            {
                _markedPositions.Remove(position);
                _fieldView.GetItem(position).SetMarkState(false);
                _bombCounter.Add();

                return;
            }

            if (_markedPositions.Count < _field.BombMap.Count)
            {
                _fieldView.GetItem(position).SetMarkState(true);
                _markedPositions.Add(position);
                _bombCounter.Reduce();
            }

            CheckWin();
        }

        private void OnCellInteracted(Vector2Int position)
        {
            if (_touchedPositions.Contains(position) || _markedPositions.Contains(position))
            {
                return;
            }

            _fieldView.GetItem(position).Show();

            if (_field.CheckBomb(position))
            {
                Restart();
            }
            else
            {
                ClearCellsAroundPosition(position, _touchedPositions);

                CheckWin();
            }
        }

        private void CheckWin()
        {
            bool fieldCleared = _touchedPositions.Count + _field.BombMap.Count == _field.Width * _field.Height;

            if (fieldCleared)
            {
                Complet?.Invoke();

                return;
            }

            if (_markedPositions.Count != _field.BombMap.Count)
            {
                return;
            }

            foreach (Vector2Int position in _markedPositions)
            {
                if (_field.CheckBomb(position) == false)
                {
                    return;
                }
            }

            Complet?.Invoke();
        }

        private void SetView()
        {
            foreach (Vector2Int bombPosition in _field.BombMap)
            {
                _fieldView.GetItem(bombPosition).SetBombState();
            }

            foreach (Vector2Int digitPosition in _field.DigitsMap.Keys)
            {
                _fieldView.GetItem(digitPosition).SetDigitState(_field.DigitsMap[digitPosition].ToString());
            }
        }

        private void ClearCellsAroundPosition(Vector2Int position, HashSet<Vector2Int> touchedPositions)
        {
            if (touchedPositions.Contains(position))
            {
                return;
            }

            _fieldView.GetItem(position).Show();
            touchedPositions.Add(position);

            if (_field.DigitsMap.ContainsKey(position))
            {
                return;
            }

            foreach (Vector2Int gridPosition in _field.GetNeightbors(position))
            {
                ClearCellsAroundPosition(gridPosition, touchedPositions);
            }
        }
    }
}
