using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private EntryPoint _entryPoint;

        private GameObject _gameObject;

        private void OnValidate()
        {
            _gameObject = gameObject;
        }

        public void Enable(TimeSpan timer)
        {
            _gameObject.SetActive(true);
            _timerText.text = timer.ToString();
            _restartButton.onClick.AddListener(Reset);
            _exitButton.onClick.AddListener(_entryPoint.Exit);
        }

        public void Disable()
        {
            _gameObject.SetActive(false);
            _restartButton.onClick.RemoveListener(Reset);
            _exitButton.onClick.RemoveListener(_entryPoint.Exit);
        }

        private void Reset()
        {
            gameObject.SetActive(false);
            _entryPoint.Reset();
        }
    }

    public class WinPanel : Panel
    {
        
    }
}