using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Asset.GameScene
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private EntryPoint _entryPoint;
        [SerializeField] private LevelEnder _levelEnder;

        [SerializeField] private List<Level> _levels;

        [SerializeField, Scene] private string _menuScene;

        private int _currentLevelIndex = 0;

        private void Awake()
        {
            _currentLevelIndex = YG2.saves.CurrentLevelIndex;
            _entryPoint.StartNextLevel(_levels[_currentLevelIndex]);
        }

        private void OnEnable()
        {
            _levelEnder.Ended += StartNextLevel;
        }

        private void OnDisable()
        {
            _levelEnder.Ended -= StartNextLevel;
        }

        private void StartNextLevel()
        {
            if (_currentLevelIndex + 1 < _levels.Count)
            {
                _levels[_currentLevelIndex].Close();
                _currentLevelIndex++;
                YG2.saves.CurrentLevelIndex = _currentLevelIndex;
                _entryPoint.StartNextLevel(_levels[_currentLevelIndex]);
            }
            else
            {
                SceneManager.LoadScene(_menuScene);
            }
        }
    }
}