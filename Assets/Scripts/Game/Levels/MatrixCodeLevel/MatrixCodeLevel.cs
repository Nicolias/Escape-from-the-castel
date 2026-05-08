using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public class MatrixCodeLevel : Level
    {
        [SerializeField] private MatrixView _matrixView;
        [SerializeField] private CodeTargetView _codeView;
        [SerializeField] private CodeBufferView _codeBufferView;

        private MatrixCode _matrixCode;
        private CodeValidator _codeValidator;
        private IEnumerator<Action<Vector2Int>> _actions;

        public override event Action Complet;

        public override void Init()
        {
            _matrixView.Init();
            Restart();
            _matrixView.Interacted += OnInteracted;
            _codeBufferView.BufferEmptied += Restart;
        }

        private void OnDisable()
        {
            _matrixView.Interacted -= OnInteracted;
            _codeValidator.Passed -= OnPassed;
        }

        private void ActivateRaw(Vector2Int matrixPosition) => _matrixView.ActivateRaw(matrixPosition.y);

        private void ActivateColumn(Vector2Int matrixPosition) => _matrixView.ActivateColumn(matrixPosition.x);

        private void Restart()
        {
            _matrixCode = new MatrixCode();
            _codeValidator = new CodeValidator(_matrixCode);
            _matrixView.SetState(_matrixCode);
            _codeValidator.Passed += OnPassed;
            _codeView.Init(_codeValidator.Code);
            _actions = GetActions().GetEnumerator();
            _codeBufferView.Init();

            _matrixView.ActivateRaw(0);
        }

        private void OnPassed() => Complet?.Invoke();

        private void OnInteracted(Vector2Int position)
        {
            _actions.MoveNext();
            _actions.Current.Invoke(position);

            ValidateCodeItem(position);
        }

        private IEnumerable<Action<Vector2Int>> GetActions()
        {
            while (enabled)
            {
                yield return ActivateColumn;
                yield return ActivateRaw;
            }
        }

        private void ValidateCodeItem(Vector2Int position)
        {
            string codeItem = _matrixCode.GetItem(position.y, position.x);

            if (_codeValidator.Validate(codeItem) == true)
            {
                _codeView.Select();
            }

            _matrixView.ExcludeItem(position);
            _codeBufferView.InsertItem(codeItem);
        }
    }
}