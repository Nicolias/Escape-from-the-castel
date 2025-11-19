using UnityEngine;
using UnityEngine.UI;

namespace Charge
{
    [RequireComponent(typeof(Image))]
    public class Battery : ConnectableObject
    {
        private Image _image;

        private Color _chargeColor = new Color(0.3098039f, 1, 0.6627451f);
        private Color _dischargeColor = new Color(0, 0.2078431f, 0);

        private void Awake()
        {
            _image = GetComponent<Image>();
            Discharge();
        }

        public override void TransitCharge(LinePort chargedLinePort)
        {
            _image.color = _chargeColor;
        }

        public void Discharge()
        {
            _image.color = _dischargeColor;
        }
    }
}