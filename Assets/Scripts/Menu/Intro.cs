using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asset.Menu
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _canvas;

        [SerializeField] private Animator _cameraAnimator;

        [SerializeField] private Image _playButtonBackground;

        private Sequence _buttonSequence;
        private Tweener _yoYoText;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(ShowIntro);
            _yoYoText = _text.DOFade(0.3f, 0.8f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _playButton.onClick.AddListener(ShowIntro);
        }

        public void Kill()
        {
            _buttonSequence.Kill();
        }

        private void ShowIntro()
        {
            _yoYoText.Kill();

            _cameraAnimator.SetTrigger(Consts.StartLevel);

            _playButtonBackground.DOFade(0, 1);
            _text.DOFade(0, 1).SetLoops(0);

            _buttonSequence = DOTween.Sequence();

            _buttonSequence
                .AppendInterval(1)
                .Append(_canvas.transform.DOMoveX(-0.35f, 3.5f)).SetEase(Ease.OutBounce)
                .AppendCallback(() => _cameraAnimator.enabled = false);

            _buttonSequence.Play();
        }
    }
}