using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MiniGames
{
    public class Shuffler
    {
        private readonly IShuffleAlgorithm _shuffleAlgorithm;

        public Shuffler(IShuffleAlgorithm shuffleAlgorithm)
        {
            _shuffleAlgorithm = shuffleAlgorithm;
        }

        public async UniTask Blend(List<Transform> cups, int targetIndex)
        {
            Transform joint = new GameObject().GetComponent<Transform>();

            foreach ((Transform, Transform) item in _shuffleAlgorithm.GetShufflePairs(cups, targetIndex))
            {
                await SwitchCups(item.Item1, item.Item2, joint);
            }

            GameObject.Destroy(joint.gameObject);
        }

        private async UniTask SwitchCups(Transform firstCup, Transform secondCup, Transform joint)
        {
            float percentDistance = 0.5f;
            joint.position = Vector3.Lerp(firstCup.position, secondCup.position, percentDistance);

            firstCup.SetParent(joint);
            secondCup.SetParent(joint);

            await RotateJoint(joint);

            joint.DetachChildren();
        }

        private async UniTask RotateJoint(Transform joint)
        {
            float rotationY = 180f;
            Quaternion targetRotation = joint.rotation * Quaternion.Euler(0f, rotationY, 0f);
            float animationTime = 0.5f;

            while (joint.rotation != targetRotation)
            {
                joint.rotation = Quaternion.RotateTowards(joint.rotation, targetRotation, rotationY / animationTime * Time.deltaTime);
                await UniTask.Yield();
            }
        }
    }
}