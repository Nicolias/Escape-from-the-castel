using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MiniGames
{
    public interface IShuffleAlgorithm
    {
        public IEnumerable<(Transform, Transform)> GetShufflePairs(List<Transform> items, int targetItemIndex);
    }
}