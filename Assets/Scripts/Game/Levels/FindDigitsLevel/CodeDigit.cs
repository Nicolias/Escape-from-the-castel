using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Levels.FindDigitsLevel
{
    public abstract class CodeDigit : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _span;

        public virtual void ResetState()
        {
            _span.enabled = true;
            _text.text = "";
        }

        protected void SetDigit(int digit)
        {
            int minDigit = 0;
            int maxDigit = 9;

            if (digit < minDigit || digit > maxDigit)
            {
                throw new ArgumentOutOfRangeException();
            }

            SetText(_text, digit);
        
            _span.enabled = false;
        }

        protected virtual void SetText(TMP_Text text, int digit) => _text.text = digit.ToString();
    }
}