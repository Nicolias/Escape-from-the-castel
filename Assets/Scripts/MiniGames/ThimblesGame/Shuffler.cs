using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MiniGames
{
    public class Shuffler
    {
        public async UniTask Blend(List<Transform> cups, int targetIndex)
        {
            Transform joint = new GameObject().GetComponent<Transform>();
            int blendingsCount = 7;
            int minCupsIndex = 0;
            int centerCupsIndex = cups.Count / 2;

            for (int i = 0; i < blendingsCount; i++)
            {
                int firstCupIndex = Random.Range(minCupsIndex, centerCupsIndex + 1);
                int secondCupIndex = Random.Range(centerCupsIndex + 1, cups.Count);
                Transform temporaryCup = cups[firstCupIndex];
                cups[firstCupIndex] = cups[secondCupIndex];
                cups[secondCupIndex] = temporaryCup;

                await SwitchCups(cups[firstCupIndex], cups[secondCupIndex], joint);
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