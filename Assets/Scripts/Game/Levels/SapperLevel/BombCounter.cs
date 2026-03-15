using TMPro;
using UnityEngine;
using System;

namespace Scripts.Levels.SapperLevel
{
    public class BombCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        private int _count;
        private int _minValue = 0;

        public void Init(int bombCount) => ChangeValue(bombCount);

        public void Add() => ChangeValue(_count + 1);

        public void Reduce() => ChangeValue(_count - 1);

        private void ChangeValue(int value)
        {
            if (value < _minValue)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            _count = value;
            _countText.text = value.ToString();
        }
    }
}
