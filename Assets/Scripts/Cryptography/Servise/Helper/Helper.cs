using Cryptography.Ciphers;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Servis
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private GameObject _caeserTipWindow;
        [SerializeField] private GameObject _atbashTipWindow;
        [SerializeField] private GameObject _viginiorTipWindow;

        private GameObject _currentTipWindow;

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
            _currentTipWindow.SetActive(true);
            _closeButton.gameObject.SetActive(true);
        }

        private void Close()
        {
            _currentTipWindow.SetActive(false);
            _closeButton.gameObject.SetActive(false);
        }
    }
}