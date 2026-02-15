using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Levels.SapperLevel
{
    public class SapperField
    {
        private List<Vector2Int> _bombMap;
        private Dictionary<Vector2Int, int> _digitsMap;

        public IReadOnlyCollection<Vector2Int> BombMap => _bombMap;

        public IReadOnlyDictionary<Vector2Int, int> DigitsMap => _digitsMap;

        public SapperField(int height, int width, int bombCount)
        {
            Height = height;
            Width = width;
            _bombMap = new List<Vector2Int>();
            _digitsMap = new Dictionary<Vector2Int, int>();

            GenerateField(bombCount);
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public bool CheckBomb(Vector2Int position) => _bombMap.Contains(position);

        public bool ContainsPosition(Vector2Int position)
        {
            if (position.x < 0 || position.x >= Height)
            {
                return false;
            }

            if (position.y < 0 || position.y >= Width)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Vector2Int> GetNeightbors(Vector2Int position)
        {
            List<Vector2Int> neightbors = new List<Vector2Int>()
            {
                position.GetLeft(),
                position.GetDown(),
                position.GetUp(),
                position.GetRight(),
                new Vector2Int(position.x - 1, position.y - 1),
                new Vector2Int(position.x - 1, position.y + 1),
                new Vector2Int(position.x + 1, position.y - 1),
                new Vector2Int(position.x + 1, position.y + 1),
            };

            return neightbors.Where(position => ContainsPosition(position));
        }

        private void GenerateField(int bombCount)
        {
            List<Vector2Int> map = new List<Vector2Int> ();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    map.Add(new Vector2Int(i, j));
                }
            }

            _bombMap = GenerateBombPositions(map, bombCount);
            _digitsMap = GenerateDigits();
        }

        private Dictionary<Vector2Int, int> GenerateDigits()
        {
            Dictionary<Vector2Int, int> digits = new Dictionary<Vector2Int, int> ();

            foreach (Vector2Int position in _bombMap)
            {
                foreach (Vector2Int neightbor in GetNeightbors(position).Where(position => !_bombMap.Contains(position)))
                {
                    if (digits.ContainsKey(neightbor))
                    {
                        digits[neightbor]++;
                    }
                    else
                    {
                        digits[neightbor] = 1;
                    }
                }
            }

            return digits;
        }

        private List<Vector2Int> GenerateBombPositions(List<Vector2Int> map, int bombCount)
        {
            int randomIndex = 0;
            List<Vector2Int> bombPositions = new List<Vector2Int>();

            for (int i = 0; i < bombCount; i++)
            {
                randomIndex = Random.Range(0, map.Count);

                bombPositions.Add(map[randomIndex]);
                map.RemoveAt(randomIndex);
            }

            return bombPositions;
        }
    }
}
