using System;
using TMPro;
using Unity.VisualScripting;
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

        [SerializeField] private GameObject _gameObject;


        public void Enable(TimeSpan timer)
        {
            _gameObject.SetActive(true);
            _timerText.text = timer.ToString();
            _restartButton.onClick.AddListener(Reset);
            _exitButton.onClick.AddListener(_entryPoint.Exit);
        }

        private void Reset()
        {
            _entryPoint.Reset();
            _restartButton.onClick.RemoveListener(Reset);
            _exitButton.onClick.RemoveListener(_entryPoint.Exit);
            _gameObject.SetActive(false);
        }
    }

    public class WinPanel : Panel
    {
        
    }
}