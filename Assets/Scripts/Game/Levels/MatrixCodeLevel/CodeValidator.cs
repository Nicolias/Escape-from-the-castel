using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CodeValidator
{
    private Queue<string> _valueQueue;
    private int _codeLength;

    public CodeValidator(MatrixCode matrix)
    {
        _valueQueue = new Queue<string>(_codeLength);
        _codeLength = 4;
        GenerateCode(matrix);
        Code = _valueQueue.ToList();
    }

    public event Action Passed;

    public IReadOnlyList<string> Code { get; private set; }

    public void GenerateCode(MatrixCode matrix)
    {
        int randomRaw = UnityEngine.Random.Range(0, matrix.Height);
        int randomColumn = 3;

        for (int i = 0; i < matrix.Width / 2; i++)
        {
            randomColumn = UnityEngine.Random.Range(randomColumn + 1, randomColumn + matrix.Width) % matrix.Width;
            _valueQueue.Enqueue(matrix.GetItem(randomRaw, randomColumn));
            randomRaw = UnityEngine.Random.Range(randomRaw + 1, randomRaw + matrix.Height) % matrix.Height;
            _valueQueue.Enqueue(matrix.GetItem(randomRaw, randomColumn));
        }
    }

    public bool Validate(string value)
    {
        if (_valueQueue.Peek() == value)
        {
            _valueQueue.Dequeue();

            if (_valueQueue.Count == 0)
            {
                Passed?.Invoke();
            }

            return true;
        }

        return false;
    }
}