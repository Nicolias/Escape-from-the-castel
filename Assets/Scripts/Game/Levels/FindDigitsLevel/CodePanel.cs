using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Levels.FindDigitsLevel
{
    public class CodePanel : DigitRaw<CodeItem>
    {
        [SerializeField] private List<Operator> _operators;

        public void SetOperators(List<Operators> operators)
        {
            for (int i = 0; i < _operators.Count; i++)
            {
                _operators[i].SetOperator(operators[i]);
            }
        }

        public override void ResetState()
        {
            base.ResetState();

            CurrentItem.Select();
        }

        public void SetDigit(int digit)
        {
            if (ItemsCount == 0)
            {
                throw new InvalidOperationException();
            }

            GetItem().SetState(digit);

            if (ItemsCount > 0)
            {
                CurrentItem.Select();
            }
        }
    }
}