using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindColorsGame : MonoBehaviour
{
    [SerializeField] private List<ColorItem> _colorItems;
    [SerializeField] private List<Color> _colors;

    private float _interactTime = 2f;
    private Queue<ColorItem> _itemsQueue;

    public event Action Won;

    public void Init()
    {
        _itemsQueue = new Queue<ColorItem>();
        _colors = _colors.Concat(_colors).ToList();

        _colors.Shuffle();
        InitializeItems();
    }

    private void OnDisable()
    {
        foreach (ColorItem item in _colorItems)
        {
            item.Clicked -= OnItemClicked;
        }
    }

    private void InitializeItems()
    {
        for (int i = 0; i < _colors.Count; i++)
        {
            _colorItems[i].Init(_colors[i]);
            _colorItems[i].Clicked += OnItemClicked;
        }
    }

    private void OnItemClicked(ColorItem item)
    {
        if (_itemsQueue.Count < 2)
        {
            item.StartCoroutine(InteractItemRoutine(item));
        }

        if (_itemsQueue.Count == 2)
        {
            if (CheckQueue() == true)
            {
                ColorItem firstItem = _itemsQueue.Dequeue();
                ColorItem secondItem = _itemsQueue.Dequeue();
                firstItem.Clicked -= OnItemClicked;
                secondItem.Clicked -= OnItemClicked;
                firstItem.StopAllCoroutines();
                secondItem.StopAllCoroutines();

                _colorItems.Remove(secondItem);
                _colorItems.Remove(firstItem);

                if (_colorItems.Count == 0)
                {
                    Won?.Invoke();
                }
            }

            return;
        }
    }

    private bool CheckQueue()
    {
        return _itemsQueue.First().Color == _itemsQueue.Last().Color;
    }

    private IEnumerator InteractItemRoutine(ColorItem item)
    {
        WaitForSeconds wait = new WaitForSeconds(_interactTime);

        _itemsQueue.Enqueue(item);
        item.Clicked -= OnItemClicked;
        item.SetTargetColor();

        yield return wait;

        item.ReturnToBaseColor();
        item.Clicked += OnItemClicked;
        _itemsQueue.Dequeue();
    }
}
