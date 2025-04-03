using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Scripts.MiniGames
{
    public class Cup : MonoBehaviour
    {
        [SerializeField] private Transform _cupContainerTransform;
        [SerializeField] private Transform _cupTransform;
        [SerializeField] private Transform _spawnPointTransform;

        public event Action<Cup> Interacted;

        public Transform Transform => _cupContainerTransform;

        public async UniTask ShowInside()
        {
            float offset = 0.5f;
            float startPosition = _cupTransform.position.y;
            float endPosition = _cupTransform.position.y + offset;

            await MoveVertical(endPosition);
            await UniTask.Delay(1000);
            await MoveVertical(startPosition);
        }

        [ContextMenu("Interact")]
        public void Interact() => Interacted?.Invoke(this);

        public async UniTask InsertItem(Transform child)
        {
            child.position = _spawnPointTransform.position;

            await ShowInside();

            child.SetParent(Transform);
        }

        private async UniTask MoveVertical(float targetYPosition)
        {
            while (Mathf.Approximately(_cupTransform.position.y, targetYPosition) == false)
            {
                _cupTransform.position = Vector3.MoveTowards(_cupTransform.position, new Vector3(_cupTransform.position.x, targetYPosition, _cupTransform.position.z), Time.deltaTime);

                 await UniTask.Yield();
            }
        }
    }
}