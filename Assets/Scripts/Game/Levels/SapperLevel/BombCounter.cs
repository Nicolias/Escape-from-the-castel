using TMPro;
using UnityEngine;

namespace Scripts.Levels.SapperLevel
{
    public class BombCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        private int _count;

        public void Init(int bombCount) => ChangeValue(bombCount);

        public void Add() => ChangeValue(_count + 1);

        public void Reduce() => ChangeValue(_count - 1);

        private void ChangeValue(int value)
        {
            _count = value;
            _countText.text = value.ToString();
        }
    }
}
