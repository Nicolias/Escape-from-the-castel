using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.FindColorsGame
{
    public class FindColorsLevel : Level
    {
        [SerializeField] private List<ColorItem> _colorItems;
        [SerializeField] private List<Color> _colors;

        private Queue<ColorItem> _itemsQueue;

        public override event Action Complet;

        public override void Init()
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

        private bool CheckQueue() => _itemsQueue.First().CurrentColor == _itemsQueue.Last().CurrentColor;

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
            int maxQueueCount = 2;

            if (_itemsQueue.Count < maxQueueCount)
            {
                item.StartCoroutine(InteractItemRoutine(item));
            }

            if (_itemsQueue.Count == maxQueueCount)
            {
                if (CheckQueue() == true)
                {
                    ColorItem firstItem = _itemsQueue.Dequeue();
                    ColorItem secondItem = _itemsQueue.Dequeue();

                    firstItem.StopAllCoroutines();
                    secondItem.StopAllCoroutines();

                    firstItem.Clicked -= OnItemClicked;
                    secondItem.Clicked -= OnItemClicked;

                    firstItem.Disable();
                    secondItem.Disable();

                    _colorItems.Remove(secondItem);
                    _colorItems.Remove(firstItem);

                    if (_colorItems.Count == 0)
                    {
                        Complet?.Invoke();
                    }
                }
            }
        }

        private IEnumerator InteractItemRoutine(ColorItem item)
        {
            _itemsQueue.Enqueue(item);
            item.Clicked -= OnItemClicked;

            yield return item.StartCoroutine(item.LightUp());

            item.Clicked += OnItemClicked;
            _itemsQueue.Dequeue();
        }
    }
}