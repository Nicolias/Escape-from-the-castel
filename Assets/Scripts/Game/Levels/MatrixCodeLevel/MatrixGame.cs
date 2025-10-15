using System;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGame : MonoBehaviour
{
    [SerializeField] private MatrixView _matrixView;
    [SerializeField] private CodeView _codeView;

    private MatrixCode _matrixCode;
    private CodeValidator _codeValidator;
    private IEnumerator<Action<Vector2Int>> _actions;

    public event Action Won;

    public void Init()
    {
        Restart();
        _matrixView.Interacted += OnInteracted;
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
    }

    private void OnPassed()
    {
        Won?.Invoke();
    }

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
        if (_codeValidator.Validate(_matrixCode.GetItem(position.y, position.x)) == true)
        {
            _codeView.Select();
        }
        else
        {
            Restart();
        }
    }
}
