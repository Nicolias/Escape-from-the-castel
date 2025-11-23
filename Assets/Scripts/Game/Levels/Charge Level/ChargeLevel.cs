using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charge
{
    public class ChargeLevel : Level
    {
        [SerializeField] private LinePort _firstLinePort;

        [SerializeField] private List<Node> _nodes = new List<Node>();
        [SerializeField] private List<Line> _lines = new List<Line>();
        [SerializeField] private List<Battery> _batteries = new List<Battery>();

        private Coroutine _checkComplete;

        public override event Action Complet;

        public override void Init()
        {
            _nodes.ForEach(node => node.Initialize());

            _firstLinePort.ChargeLine();
        }

        private void OnEnable()
        {
            _nodes.ForEach(node => node.Rotated += OnRotated);
        }

        private void OnDisable()
        {
            _nodes.ForEach(node => node.Rotated -= OnRotated);
        }

        private void OnRotated()
        {
            if(_checkComplete != null) 
                StopCoroutine(_checkComplete);

            _lines.ForEach(line => line.Discharge());
            _batteries.ForEach(battery => battery.Discharge());
            _firstLinePort.ChargeLine();

            _checkComplete = StartCoroutine(CheckComplete());
        }

        private IEnumerator CheckComplete()
        {
            int chargedCount = 0;
            WaitForSeconds checkDelay = new WaitForSeconds(3f);

            while (chargedCount < _batteries.Count)
            {
                yield return checkDelay;
                chargedCount = 0;

                foreach (Battery battery in _batteries)
                {
                    if (battery.IsCharged)
                        chargedCount++;
                }
            }

            Complet?.Invoke();
        }
    }
}