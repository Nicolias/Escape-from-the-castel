using Cryptography.Servis;
using System;

namespace Cryptography.Ciphers
{
    public abstract class AbstractCipher
    {
        private CipherView _cipherView;
        private Helper _tip;
        private string _text;

        protected string Alphabet { get; private set; }

        public AbstractCipher(CipherView cipherView, Helper tip, string text, string alphabet)
        {
            _cipherView = cipherView;
            _tip = tip;
            _text = text;
            Alphabet = alphabet;
        }

        public event Action Complete;

        public void Enter()
        {
            _cipherView.PressedVerificationButton += OnPressedVerificationButton;
            string encryptText = Encrypt(_text, out string key);

            _cipherView.UpdateUI(encryptText, key);
            Accept(_tip);
        }

        public void Exit()
        {
            _cipherView.PressedVerificationButton -= OnPressedVerificationButton;
        }

        protected abstract string Encrypt(string text, out string key);

        protected abstract void Accept(Helper tip);

        private void OnPressedVerificationButton(string decryptText)
        {
            if (decryptText == _text)
            {
                Exit();
                Complete?.Invoke();
            }

            _cipherView.ApplyAnswer(decryptText == _text);
        }
    }
}