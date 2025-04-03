using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MiniGames
{
    public class ShuffleAlgorithm : IShuffleAlgorithm
    {
        public IEnumerable<(Transform, Transform)> GetShufflePairs(List<Transform> items, int targetItemIndex)
        {
            int minBlendsCount = 5;
            int maxBlendsCount = 10;
            int blendingsCount = Random.Range(minBlendsCount, maxBlendsCount);
            int minCupsIndex = 0;
            int centerCupsIndex = items.Count / 2;
            Transform joint = new GameObject().GetComponent<Transform>();

            for (int i = 0; i < blendingsCount; i++)
            {
                int firstCupIndex = Random.Range(minCupsIndex, centerCupsIndex + 1);
                int secondCupIndex = Random.Range(centerCupsIndex + 1, items.Count);
                Transform temporaryCup = items[firstCupIndex];
                items[firstCupIndex] = items[secondCupIndex];
                items[secondCupIndex] = temporaryCup;

                yield return (items[firstCupIndex], items[secondCupIndex]);
            }
        }
    }
}