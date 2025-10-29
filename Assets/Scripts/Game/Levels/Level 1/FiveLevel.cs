using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Levels.Level_1
{
    public class FiveLevel : Level
    {
        [SerializeField] private List<Slot> _blueSlots;
        [SerializeField] private List<Slot> _redSlots;

        private bool _isGameFinish;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(2);

        public override event Action Complet;

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

                foreach (Slot slot in _blueSlots)
                    if (slot.CurrentCollor != CellCollor.Blue)
                        _isGameFinish = false;

                foreach (Slot slot in _redSlots)
                    if (slot.CurrentCollor != CellCollor.Red)
                        _isGameFinish = false;
            }

            Complet?.Invoke();
        }     
    }
}