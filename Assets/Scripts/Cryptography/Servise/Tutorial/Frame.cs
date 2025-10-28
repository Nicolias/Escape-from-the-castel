using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Servis
{
    public class Frame : MonoBehaviour
    {
        [SerializeField] private Frame _nextFrame;
        [SerializeField] private Button _switchFrameButton;

        public event Action End;

        private void OnEnable()
        {
            _switchFrameButton.onClick.AddListener(SwitchFrame);
        }

        private void OnDisable()
        {
            _switchFrameButton.onClick.RemoveListener(SwitchFrame);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void SwitchFrame()
        {
            gameObject.SetActive(false);

            if (_nextFrame != null)
                _nextFrame.Open();

            End?.Invoke();
        }
    }
}