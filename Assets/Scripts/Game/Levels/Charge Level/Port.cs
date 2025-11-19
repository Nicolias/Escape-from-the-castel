using System.Collections.Generic;
using UnityEngine;

namespace Charge
{
    public class Port : MonoBehaviour
    {
        [SerializeField] private List<Port> _ports;

        public IEnumerable<Port> ConnectionPorts => _ports;
    }
}