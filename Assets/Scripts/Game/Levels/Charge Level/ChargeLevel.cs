using System;
using System.Collections.Generic;
using UnityEngine;

namespace Charge
{
    public class ChargeLevel : Level
    {
        [SerializeField] private LinePort _firstLinePort;

        [SerializeField] private List<Node> _nodes = new List<Node>();
        [SerializeField] private List<Line> _lines = new List<Line>();
        [SerializeField] private List<Battery> _battery = new List<Battery>();

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
            _lines.ForEach(line => line.Discharge());
            _battery.ForEach(battery => battery.Discharge());
            _firstLinePort.ChargeLine();
        }
    }
}