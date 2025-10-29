using Cryptography.Servis;
using System;
using System.Text;

namespace Cryptography.Ciphers
{
    public class CaesarCipher : AbstractCipher
    {
        private Random _random = new Random();

        public CaesarCipher(CipherView cipherView, Helper helper, string text, string alphabet) 
            : base(cipherView, helper, text, alphabet)
        {
        }

        protected override string Encrypt(string text, out string key)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            int shift = _random.Next(0, Alphabet.Length - 1);
            int normalizedShift = ((shift % Alphabet.Length) + Alphabet.Length) % Alphabet.Length;
            var stringBuilder = new StringBuilder(text.Length);

            foreach (char symbol in text)
            {
                int index = Array.IndexOf(Alphabet.ToCharArray(), symbol);

                if (index >= 0)
                {
                    int newIndex = (index + normalizedShift) % Alphabet.Length;
                    stringBuilder.Append(Alphabet[newIndex]);
                }
                else
                {
                    stringBuilder.Append(symbol);
                }
            }

            key = normalizedShift.ToString();

            return stringBuilder.ToString();
        }

        protected override void Accept(Helper tip)
        {
            tip.Visit(this);
        }
    }
}