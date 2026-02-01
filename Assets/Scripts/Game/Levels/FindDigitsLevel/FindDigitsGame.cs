using System;
using System.Collections.Generic;
using UnityEngine;

public class FindDigitsGame : MonoBehaviour
{
    [SerializeField] private List<CodeBuffer> _digitsRaws;
    [SerializeField] private CodePanel _mainRaw;
    [SerializeField] private Numpad _numPad;

    private Queue<CodeBuffer> _raws;
    private List<int> _code;
    private List<int> _playerCode;

    public event Action Won;

    public void Init()
    {
        _mainRaw.Init();
        _numPad.Init();
        _numPad.ButtonClicked += OnNumpadClicked;

       Restart();
    }

    private bool CheckWin()
    {
        for (int i = 0; i < _code.Count; i++)
        {
            if (_code[i] != _playerCode[i])
            {
                return false;
            }
        }

        return true;
    }

    private void Restart()
    {
        _raws = new Queue<CodeBuffer>(_digitsRaws);

        foreach (CodeBuffer raw in _raws)
        {
            raw.ResetState();
        }

        GenerateCode();
        _mainRaw.ResetState();
        _mainRaw.SetOperators(GenerateOperators());
    }

    private List<Operators> GenerateOperators()
    {
        List<Operators> operators = new List<Operators>();

        for (int i = 0; i < _code.Count - 1; i++)
        {
            if (_code[i] > _code[i + 1])
            {
                operators.Add(Operators.Larger);
            }
            else
            {
                operators.Add(Operators.Less);
            }
        }

        return operators;
    }

    private void GenerateCode()
    {
        int digitsCount = 4;
        List<int> result = new List<int>(digitsCount);
        List<int> digits = new List<int>
        {
            1,2,3,4,5,6,7,8,9
        };

        for (int i = 0; i < digitsCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, digits.Count);

            result.Add(digits[randomIndex]);
            digits.RemoveAt(randomIndex);
        }

        _code = result;
        _playerCode = new List<int>(_code.Count);
    }

    private void OnNumpadClicked(NumpadItem item)
    {
        if (_playerCode.Contains(item.Digit))
        {
            return;
        }

        _playerCode.Add(item.Digit);
        _mainRaw.SetDigit(item.Digit);

        if (_playerCode.Count == _code.Count)
        {
            _mainRaw.ResetState();
            SetRawState(_raws.Dequeue());

            if (CheckWin() == true)
            {
                Won?.Invoke();

                return;
            }

            _playerCode = new List<int>(_code.Count);

            if (_raws.Count == 0)
            {
                Restart();

                return;
            }
        }
    }

    private void SetRawState(CodeBuffer raw)
    {
        raw.Init();

        for (int i = 0; i < _playerCode.Count; i++)
        {
            raw.SetDigit(_playerCode[i], GetColor(_playerCode[i]));
        }
    }

    private Color GetColor(int playerDigit)
    {
        int playerDigitIndex = _playerCode.IndexOf(playerDigit);

        if (playerDigitIndex == -1)
        {
            throw new InvalidOperationException();
        }

        if (playerDigit == _code[playerDigitIndex])
        {
            return Color.green;
        }

        if (_code.Contains(playerDigit))
        {
            return Color.yellow;
        }

        return Color.red;
    }
}
