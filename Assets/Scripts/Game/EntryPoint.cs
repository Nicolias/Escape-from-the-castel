using NaughtyAttributes;
using UnityEngine;
using YG;

namespace Asset.GameScene
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField, Scene] private string _currentSceneName;

        [SerializeField] private Animator _safeBoxAnimator;
        [SerializeField] private LevelEnder _ender;
        [SerializeField] private GameObject _canvas;

        private Level _currentLevel;

        public void StartNextLevel(Level level)
        {
            if(_currentLevel != null)
                _currentLevel.Complete -= _ender.EndGame;

            _currentLevel = level;
            _currentLevel.Complete += _ender.EndGame;
            _currentLevel.Open();

            _safeBoxAnimator.SetTrigger(Consts.OpenSafe);

            YG2.saves.CurrentLevelName = _currentSceneName;
            YG2.SaveProgress();
            _currentLevel.Init();
        }
    }
}