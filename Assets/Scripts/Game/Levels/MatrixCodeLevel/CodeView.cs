using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public abstract class CodeView : MonoBehaviour
    {
        [SerializeField] private RectTransform _selection;
        [SerializeField] private List<TMP_Text> _items;

        private Queue<TMP_Text> _textPartsQueue;

        public IReadOnlyList<TMP_Text> Items => _items;

        public int Count => _textPartsQueue.Count;

        public TMP_Text CurrentItem => _textPartsQueue.Peek();

        protected void ResetQueueItems()
        {
            _textPartsQueue = new Queue<TMP_Text>(_items);

            MoveSelectionToPeekItem();
        }

        protected void SelectNext()
        {
            if (_textPartsQueue.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            _textPartsQueue.Dequeue();

            MoveSelectionToPeekItem();
        }

        protected void MoveSelectionToPeekItem()
        {
            if (_textPartsQueue.Count != 0)
            {
                _selection.anchoredPosition = new Vector2(_textPartsQueue.Peek().rectTransform.anchoredPosition.x, _selection.anchoredPosition.y);
            }
        }
    }
}
