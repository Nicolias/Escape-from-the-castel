using System;
using TMPro;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public class CodeBufferView : CodeView
    {
        public event Action BufferEmptied;

        public void Init()
        {
            foreach (TMP_Text item in Items)
            {
                item.text = "*";
            }

            ResetQueueItems();
        }

        public void InsertItem(string codePart)
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            CurrentItem.text = codePart;
            SelectNext();

            if (Count == 0)
            {
                BufferEmptied?.Invoke();

                return;
            }
        }
    }
}
