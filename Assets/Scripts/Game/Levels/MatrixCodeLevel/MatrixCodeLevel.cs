using System;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public class MatrixCodeLevel : Level
    {
        [SerializeField] private MatrixGame _game;

        public override event Action Complete;

        public override void Init()
        {
            _game.Init();
        }

        private void OnEnable()
        {
            _game.Won += OnGameWon;
        }

        private void OnDisable()
        {
            _game.Won -= OnGameWon;
        }

        private void OnGameWon()
        {
            Complete?.Invoke();
        }
    }
}
