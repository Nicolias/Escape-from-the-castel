using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Charge
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private List<Image> _images;

        private Color _chargeColor = new Color(0.3098039f, 1, 0.6627451f);
        private Color _dischargeColor = new Color(0, 0.2078431f, 0);

        public bool IsCharged { get; private set; }

        private void OnEnable()
        {
            SwitchColor();
        }

        public void Charge()
        {
            IsCharged = true;
            SwitchColor();
        }

        public void Discharge()
        {
            IsCharged = false;
            SwitchColor();
        }

        private void SwitchColor()
        {
            _images.ForEach(image => image.color = IsCharged ? _chargeColor : _dischargeColor);
        }
    }
}