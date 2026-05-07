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
        [SerializeField] private Animator _nameAnimator;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(ShowIntro);
            _text.DOFade(0.3f, 0.8f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _playButton.onClick.AddListener(ShowIntro);
        }

        private void ShowIntro()
        {
            _cameraAnimator.SetTrigger("Start Level");
            _nameAnimator.SetTrigger("Start Level");

            Sequence sequence = DOTween.Sequence();

            sequence.Append(_canvas.transform.DOMoveX(-0.35f, 3.5f)).SetEase(Ease.OutBounce);

            sequence.Play();
        }
    }
}