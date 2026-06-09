using System;
using UnityEngine;
using UnityEngine.UI;

namespace Asset.Menu
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private GameObject _gameObject;

        public void Initialize()
        {
            _gameObject = gameObject;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Open()
        {
            _gameObject.SetActive(true);
        }

        private void Close()
        {
            _gameObject.SetActive(false);
        }
    }
}