using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Charge
{
    [RequireComponent(typeof(Button))]
    public class Node : ConnectableObject
    {
        [SerializeField] private Port _currentUpPort;
        [SerializeField] private Port _currentDownPort;
        [SerializeField] private Port _currentLeftPort;
        [SerializeField] private Port _currentRightPort;

        [SerializeField] private LinePort _upLine;
        [SerializeField] private LinePort _downLine;
        [SerializeField] private LinePort _leftLine;
        [SerializeField] private LinePort _rightLine;

        [SerializeField] private LinePort _template;

        private Dictionary<LinePort, Port> _connections = new Dictionary<LinePort, Port>();

        private Transform _selfTransform;
        private Button _selfButton;

        public event Action Rotated;

        private void Awake()
        {
            _selfButton = GetComponent<Button>();
            _selfTransform = transform;
        }

        public void Initialize()
        {
            if (_upLine == null) _upLine = Instantiate(_template);
            if (_downLine == null) _downLine = Instantiate(_template);
            if (_leftLine == null) _leftLine = Instantiate(_template);
            if (_rightLine == null) _rightLine = Instantiate(_template);

            Connect(_upLine, _currentUpPort);
            Connect(_downLine, _currentDownPort);
            Connect(_leftLine, _currentLeftPort);
            Connect(_rightLine, _currentRightPort);
        }

        private void OnEnable()
        {
            _selfButton.onClick.AddListener(Rotate);
        }

        private void OnDisable()
        {
            _selfButton.onClick.RemoveListener(Rotate);
        }

        public override void TransitCharge(LinePort chargedLinePort)
        {
            if (_connections.ContainsKey(chargedLinePort) == false)
            {
                return;
                throw new InvalidProgramException();
            }

            if (_connections[chargedLinePort] == null)
                return;

            foreach (Port connectionPort in _connections[chargedLinePort].ConnectionPorts)
                foreach (LinePort linePort in _connections.Keys)
                    if(linePort.IsTemplate == false)
                        if (_connections[linePort] == connectionPort)
                            linePort.ChargeLine();
        }

        private void Rotate()
        {
            _selfTransform.Rotate(new Vector3(0, 0, -90));

            Port temp = _connections[_leftLine];
            SwitchConnections(_leftLine, _connections[_downLine]);
            SwitchConnections(_downLine, _connections[_rightLine]);
            SwitchConnections(_rightLine, _connections[_upLine]);
            SwitchConnections(_upLine, temp);

            Rotated?.Invoke();
        }

        private void SwitchConnections(LinePort linePort, Port port)
        {
            _connections[linePort] = port;
        }

        private void Connect(LinePort linePort, Port port)
        {
            _connections.Add(linePort, port);
        }
    }
}