using Assets.Game.Levels.Level_1;
using System;
using System.Collections;
using UnityEngine;

namespace MoveByLine
{
    public class MoveByLine : Level
    {
        [SerializeField] private Slot _blueSlot;
        [SerializeField] private Slot _redSlot;
        [SerializeField] private Slot _yellowSlot;
        [SerializeField] private Slot _greenSlot;

        private bool _isGameFinish;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(2);

        public override event Action Complete;

        public override void Init()
        {
            StartCoroutine(CheckCondition());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CheckCondition()
        {
            while (_isGameFinish == false)
            {
                yield return _waitForSeconds;

                _isGameFinish = true;

                if (_blueSlot.CurrentCollor != CellCollor.Blue)
                    _isGameFinish = false;

                if (_redSlot.CurrentCollor != CellCollor.Red)
                    _isGameFinish = false;

                if (_yellowSlot.CurrentCollor != CellCollor.Yellow)
                    _isGameFinish = false;

                if (_greenSlot.CurrentCollor != CellCollor.Green)
                    _isGameFinish = false;
            }

            Complete?.Invoke();
        }
    }
}