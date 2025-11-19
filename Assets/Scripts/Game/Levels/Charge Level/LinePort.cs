using System;
using UnityEngine;

namespace Charge
{
    public class LinePort : MonoBehaviour
    {
        [SerializeField] private Line _line;
        [SerializeField] private LinePort _linePort;
        [SerializeField] private ConnectableObject _connectableObject;

        [field : SerializeField] public bool IsTemplate { get; private set; }

        public void ChargeLine()
        {
            if (_line.IsCharged)
                return;

            _line.Charge();
            _linePort.TransitCharge();
        }

        public void TransitCharge()
        {
            if (_connectableObject == null)
                throw new InvalidProgramException();

            _connectableObject.TransitCharge(this);
        }
    }
}