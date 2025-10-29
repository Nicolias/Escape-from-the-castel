using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography
{
    public class CipherView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _encryptText;
        [SerializeField] private TMP_Text _keyText;
        [SerializeField] private TMP_InputField _decryptText;

        [SerializeField] private Button _resultVerificationButton;

        public event Func<string, bool> PressedVerificationButton;
        public event Action<bool> IsAnswerCorrect;

        private void OnEnable()
        {
            _resultVerificationButton.onClick.AddListener(PullDecryptText);
        }

        private void OnDisable()
        {
            _resultVerificationButton.onClick.RemoveListener(PullDecryptText);
        }

        public void UpdateUI(string encyptText, string keyText)
        {
            _decryptText.text = "";
            _encryptText.text = encyptText;
            _keyText.text = keyText;
        }

        private void PullDecryptText()
        {
            IsAnswerCorrect?.Invoke(PressedVerificationButton.Invoke(_decryptText.text.ToUpper()));
        }
    }
}