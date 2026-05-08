using DG.Tweening;
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

        [SerializeField] private Animator _cameraAnimator;
        [SerializeField] private GameObject _canvas;

        public void Awake()
        {
            _cameraAnimator.SetTrigger(Consts.StartLevel);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_canvas.transform.DOScale(0f, 0f));
            sequence.AppendInterval(2f);
            sequence.Append(_canvas.transform.DOScale(0.34f, 0.5f));
            sequence.AppendCallback(() => _cameraAnimator.enabled = false);

            YG2.saves.CurrentLevelName = _currentSceneName;
            YG2.SaveProgress();
            _level.Init();
        }

        private void OnEnable()
        {
            _level.Complete += _ender.EndGame;
        }

        private void OnDisable()
        {
            _level.Complete -= _ender.EndGame;
        }
    }
}