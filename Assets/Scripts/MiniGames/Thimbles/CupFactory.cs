using UnityEngine;

namespace Assets.Scripts.MiniGames.Factories
{
    public class CupFactory : MonoBehaviour
    {
        [SerializeField] private Cup _cupPrefab;

        public Cup Create(Vector3 position)
        {
            return Instantiate(_cupPrefab, position, Quaternion.identity);
        }
    }
}