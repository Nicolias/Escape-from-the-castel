using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asset.GameScene
{
    public class LevelEnder : MonoBehaviour
    {
        [SerializeField] private Animator _safeBoxAnimator;
        [SerializeField] private float _openDoorDuration;

        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _camera;

        [SerializeField, Scene] private string _nextScene;

        public void EndGame()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_canvas.transform.DOScale(0, 0.5f))
                .AppendCallback(() => _safeBoxAnimator.SetTrigger(Consts.OpenSafe))
                .AppendInterval(_openDoorDuration)
                .Append(_camera.transform.DOMoveZ(0.3f, 0.8f).SetEase(Ease.InQuad))
                .AppendCallback(() => SceneManager.LoadScene(_nextScene));
            sequence.Play();
        }
    }
}