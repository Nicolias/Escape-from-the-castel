using System;
using UnityEngine;

namespace Cryptography.Servis
{
    public class FailsCounter : MonoBehaviour
    {
        private const int MaxFailCount = 3;

        private CipherView _cipherView;
        public int CurrentFailCount { get; private set; }


        public event Action Lose;
        public event Action Faild;
        public event Action Reset;

        public void Initialize(CipherView cipherView)
        {
            if (cipherView == null)
                throw new NullReferenceException();

            _cipherView = cipherView;
        }

        public void Reseting()
        {
            Reset?.Invoke();
            CurrentFailCount = 0;
        }

        public void Enable()
        {
            CurrentFailCount = 0;
            _cipherView.IsAnswerCorrect += VerificatAnswer;
        }

        public void Disable()
        {
            _cipherView.IsAnswerCorrect -= VerificatAnswer;
        }

        private void VerificatAnswer(bool isAnswerCorrect)
        {
            if (isAnswerCorrect == false)
            {
                CurrentFailCount++;
                Faild?.Invoke();
            }

            if (CurrentFailCount == MaxFailCount)
                Lose?.Invoke();
        }
    }
}