using Cryptography.Ciphers;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Servis
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private TipWindow _caeserTipWindow;
        [SerializeField] private TipWindow _atbashTipWindow;
        [SerializeField] private TipWindow _viginiorTipWindow;

        private TipWindow _currentTipWindow;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Initialize()
        {
            _caeserTipWindow.Initialize();
            _atbashTipWindow.Initialize();
            _viginiorTipWindow.Initialize();
        }

        public void Visit(CaesarCipher caesarCipher)
        {
            _currentTipWindow = _caeserTipWindow;
        }

        public void Visit(AtbashCipher atbashCipher)
        {
            _currentTipWindow = _atbashTipWindow;
        }

        public void Visit(VigenereCipher vigenereCipher)
        {
            _currentTipWindow = _viginiorTipWindow;
        }

        private void Open()
        {
            _currentTipWindow.Open();
            _closeButton.gameObject.SetActive(true);
        }

        private void Close()
        {
            _currentTipWindow.Close();
            _closeButton.gameObject.SetActive(false);
        }
    }
}