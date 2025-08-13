using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asset.GameScene
{
    public class LevelEnder : MonoBehaviour
    {
        [SerializeField] private Transform _safeboxDoor;
        [SerializeField] private Vector3 _safeboxDoorOpenRotation;
        [SerializeField] private int _openDoorDuration;

        [SerializeField] private GameObject _canvas;

        [SerializeField, Scene] private string _nextScene;

        private void OnEnable()
        {
            EndGame();
        }

        public void EndGame()
        {
            _canvas.SetActive(false);

            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_safeboxDoor.DORotate(_safeboxDoorOpenRotation, _openDoorDuration))
                .AppendCallback(() => SceneManager.LoadScene(_nextScene));
            sequence.Play();
        }
    }
}