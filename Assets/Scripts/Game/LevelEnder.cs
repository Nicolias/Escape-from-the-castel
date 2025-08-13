using DG.Tweening;
using UnityEngine;

namespace Asset.GameScene
{
    public class LevelEnder : MonoBehaviour
    {
        [SerializeField] private Transform _safeboxDoor;
        [SerializeField] private Vector3 _safeboxDoorOpenRotation;
        [SerializeField] private int _openDoorDuration;

        [SerializeField] private GameObject _canvas;

        private void OnEnable()
        {
            EndGame();
        }

        public void Initialize()
        {

        }

        public void EndGame()
        {
            _canvas.SetActive(false);
            _safeboxDoor.DORotate(_safeboxDoorOpenRotation, _openDoorDuration);
        }
    }
}