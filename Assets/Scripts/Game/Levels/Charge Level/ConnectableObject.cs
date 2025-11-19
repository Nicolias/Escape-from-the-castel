using UnityEngine;

namespace Charge
{
    public abstract class ConnectableObject : MonoBehaviour
    {
        public abstract void TransitCharge(LinePort chargedLinePort);
    }
}