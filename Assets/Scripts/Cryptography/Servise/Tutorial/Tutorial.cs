using System;
using UnityEngine;

namespace Cryptography.Servis
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Frame _firstFrame;
        [SerializeField] private Frame _lastFrame;

        private bool _isNeedTutorial = true;

        public event Action End;

        private void Awake()
        {
            if (_isNeedTutorial)
                _firstFrame.Open();
            else
                EndTutorial();
        }

        private void OnEnable()
        {
            _lastFrame.End += EndTutorial;
        }

        private void OnDisable()
        {
            _lastFrame.End -= EndTutorial;
        }

        private void EndTutorial()
        {
            End?.Invoke();
        }
    }
}