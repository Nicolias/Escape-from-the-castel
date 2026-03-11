using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Levels.FindDigitsLevel
{
    public class Numpad : MonoBehaviour
    {
        [SerializeField] private List<NumpadItem> _buttons;

        public event Action<NumpadItem> ButtonClicked;

        public void Init()
        {
            foreach (NumpadItem item in _buttons)
            {
                item.Clicked += OnItemClicked;
            }
        }

        private void OnDisable()
        {
            foreach (NumpadItem item in _buttons)
            {
                item.Clicked -= OnItemClicked;
            }
        }

        private void OnItemClicked(NumpadItem item) => ButtonClicked?.Invoke(item);
    }
}