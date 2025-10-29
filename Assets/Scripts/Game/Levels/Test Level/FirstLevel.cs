using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Levels
{
    public class FirstLevel : Level
    {
        [SerializeField] private Button _endLevelButton;

        public override event Action Complet;

        private void OnDisable()
        {
            _endLevelButton.onClick.RemoveListener(CompleteLevel);
        }

        public override void Init()
        {
            _endLevelButton.onClick.AddListener(CompleteLevel);
        }

        private void CompleteLevel()
        {
            Complet?.Invoke();
        }
    }
}