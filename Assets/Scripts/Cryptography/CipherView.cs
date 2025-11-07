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

        public event Action<string> PressedVerificationButton;
        public event Action<bool> ReceivedCorrectAnswer;

        public string VerificationResult {  get; private set; }

        private void OnEnable()
        {
            _resultVerificationButton.onClick.AddListener(PushDecryptText);
        }

        private void OnDisable()
        {
            _resultVerificationButton.onClick.RemoveListener(PushDecryptText);
        }

        public void UpdateUI(string encyptText, string keyText)
        {
            _decryptText.text = "";
            _encryptText.text = encyptText;
            _keyText.text = keyText;
        }

        public void ApplyAnswer(bool isCorrect)
        {
            ReceivedCorrectAnswer?.Invoke(isCorrect);
        }

        private void PushDecryptText()
        {
            PressedVerificationButton?.Invoke(_decryptText.text.ToUpper());
        }
    }
}