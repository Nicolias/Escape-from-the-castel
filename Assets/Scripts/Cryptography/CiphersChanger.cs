using Cryptography.Ciphers;
using Cryptography.Servis;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Cryptography
{
    public class CiphersChanger : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private Helper _helper;
        [SerializeField] private Locolizer _locolizer;

        private CipherView _cipherView;
        private List<AbstractCipher> _ciphers;
        private List<string> _texts;

        public event Action Win;

        public void Initialize(CipherView cipherView)
        {
            if (cipherView == null)
                throw new NullReferenceException();  

            _cipherView = cipherView;
        }

        public void Enable()
        {
            _texts = new List<string>(_locolizer.CurrentTexts);
            Shuffle(_texts);

            _ciphers = new List<AbstractCipher>()
            {
                new CaesarCipher(_cipherView, _helper, _texts[0], _locolizer.CurrentAlphabet),
                new AtbashCipher(_cipherView, _helper, _texts[1], _locolizer.CurrentAlphabet),
                new VigenereCipher(_cipherView, _helper, _texts[2], _locolizer.CurrentAlphabet, _locolizer.CurrentLanguage),
            };

            _timer.Play();

            if (_ciphers.Count > 1)
            {
                for (int i = 0; i < _ciphers.Count - 1; i++)
                    _ciphers[i].Complete += _ciphers[i + 1].Enter;
            }

            _ciphers[0].Enter();

            _ciphers[_ciphers.Count - 1].Complete += Complete;
        }

        public void Disable()
        {
            if (_ciphers == null)
                return;

            if (_ciphers.Count > 1)
            {
                for (int i = 0; i < _ciphers.Count - 1; i++)
                    _ciphers[i].Complete -= _ciphers[i + 1].Enter;
            }

            _ciphers[0].Exit();

            _ciphers[_ciphers.Count - 1].Complete -= Complete;
        }

        private void Complete()
        {
            if ((int)_timer.CurrentTime.TotalSeconds > YG2.saves.CiphersBestTime)
            {
                YG2.saves.CiphersBestTime = (int)_timer.CurrentTime.TotalSeconds;
                YG2.SaveProgress();
            }

            Win?.Invoke();
        }

        private void Shuffle(List<string> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;

            while (--n > 0)
            {
                int k = random.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}