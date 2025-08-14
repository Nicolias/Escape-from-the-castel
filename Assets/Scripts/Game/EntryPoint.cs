using NaughtyAttributes;
using UnityEngine;
using YG;

namespace Asset.GameScene
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField, Scene] private string _currentSceneName;

        [SerializeField] private Level _level;
        [SerializeField] private LevelEnder _ender;

        public void Awake()
        {
            YG2.saves.CurrentLevelName = _currentSceneName;
            YG2.SaveProgress();
            _level.Init();
        }

        private void OnEnable()
        {
            _level.Complet += _ender.EndGame;
        }

        private void OnDisable()
        {
            _level.Complet -= _ender.EndGame;
        }
    }
}