using Cryptography.Servis;
using System;
using System.Collections.Generic;

namespace Cryptography.Ciphers
{
    public class VigenereCipher : AbstractCipher
    {
        private Random _random = new Random();
        private Languages _currentLanguage;

        private Dictionary<Languages, List<string>> _keys = new Dictionary<Languages, List<string>>()
        {
            {Languages.English, new List<string>() { "Falcon", "Star", "Leaf", "Flame", "Wave" } },
            {Languages.Russian, new List<string>() { "Сокол", "Звезда", "Листок", "Пламя", "Волна"}},
            {Languages.Turkish, new List<string>() { "Kartal", "Yıldız", "Yaprak", "Alev", "Dalga"}}
            
        };

        private Dictionary<char, int> CharIndex = new Dictionary<char, int>();

        public VigenereCipher(CipherView cipherView, Helper helper, string text, string alphabet, Languages currentLanguage) 
            : base(cipherView, helper, text, alphabet)
        {
            for (int i = 0; i < Alphabet.Length; i++)
                CharIndex[Alphabet[i]] = i;

            _currentLanguage = currentLanguage;
        }

        protected override string Encrypt(string message, out string key)
        {
            List<string> keys = _keys[_currentLanguage];

            key = keys[_random.Next(0, _keys.Count)];

            key = key.ToUpper();

            var result = new char[message.Length];
            int keyLength = key.Length;

            for (int i = 0; i < message.Length; i++)
            {
                char messageChar = message[i];

                char keyChar = key[i % keyLength];

                int resultPosition = (CharIndex[messageChar] + CharIndex[keyChar]) % Alphabet.Length;
                result[i] = Alphabet[resultPosition];
            }

            return new string(result);
        }

        protected override void Accept(Helper tip)
        {
            tip.Visit(this);
        }
    }
}