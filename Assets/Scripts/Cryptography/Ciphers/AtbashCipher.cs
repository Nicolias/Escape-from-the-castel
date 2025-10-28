using Cryptography.Servis;
using System;
using System.Text;

namespace Cryptography.Ciphers
{
    public class AtbashCipher : AbstractCipher
    {
        public AtbashCipher(CipherView cipherView, Helper helper, string text, string alphabet) 
            : base(cipherView, helper, text, alphabet)
        {
        }

        protected override string Encrypt(string text, out string key)
        {
            key = "";

            if (string.IsNullOrEmpty(text))
                return text;

            var stringBuilder = new StringBuilder(text.Length);

            foreach (char symbol in text)
            {
                int index = Array.IndexOf(Alphabet.ToCharArray(), symbol);

                if (index >= 0 && index <= Alphabet[Alphabet.Length - 1])
                {
                    char cipherChar = Alphabet[Alphabet.Length - 1 - index];

                    stringBuilder.Append(char.IsUpper(symbol) ? char.ToUpperInvariant(cipherChar) : cipherChar);
                }
                else
                {
                    stringBuilder.Append(symbol);
                }
            }

            return stringBuilder.ToString();
        }

        protected override void Accept(Helper tip)
        {
            tip.Visit(this);
        }
    }
}