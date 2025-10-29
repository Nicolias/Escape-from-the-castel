using System.Collections.Generic;
using UnityEngine;

public abstract class DigitRaw<T> : MonoBehaviour where T : CodeDigit
{
    [SerializeField] private List<T> _codeItems;

    private Queue<T> _selectedQueue;

    protected T CurrentItem => _selectedQueue.Peek();

    protected int ItemsCount => _selectedQueue.Count;

    public virtual void Init()
    {
        ResetState();
    }

    public virtual void ResetState()
    {
        _selectedQueue = new Queue<T>(_codeItems);

        foreach (T item in _codeItems)
        {
            item.ResetState();
        }
    }

    protected T GetItem() => _selectedQueue.Dequeue();
}