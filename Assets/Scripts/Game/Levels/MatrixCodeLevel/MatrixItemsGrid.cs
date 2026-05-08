using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public class MatrixItemsGrid
    {
        private Dictionary<Vector2Int, MatrixItem> _allItems;
        private Dictionary<Vector2Int, MatrixItem> _activeItems;

        public IReadOnlyDictionary<Vector2Int, MatrixItem> AllItems => _allItems;

        public HashSet<Vector2Int> SelectedPositions { get; private set; }

        public Vector2Int PointerPosition { get; set; }

        public MatrixItemsGrid(IEnumerable<MatrixItem> items)
        {
            _allItems = items.ToDictionary(item => CalculateMatrixPosition(item.Position), item => item);
            _activeItems = new Dictionary<Vector2Int, MatrixItem>();
            SelectedPositions = new HashSet<Vector2Int>();
            PointerPosition = _allItems.First().Key;
        }

        public void ActivateColumn(int value) => _activeItems = GetColumnItems(value).ToDictionary(position => position, position => _allItems[position]);

        public void ActivateRaw(int value) => _activeItems = GetRawItems(value).ToDictionary(position => position, position => _allItems[position]);

        public bool TrySetPointerPosition(Vector2Int position)
        {
            if (SelectedPositions.Contains(position))
            {
                return false;
            }

            if (_activeItems.ContainsKey(position) == false)
            {
                return false;
            }

            PointerPosition = position;

            return true;
        }

        public Vector2Int CalculateMatrixPosition(Vector2 position)
        {
            float scale = 0.25f;
            float halfScale = 0.125f;
            float minYposition = -1f;
            float maxYposition = 0f;
            int minMatrixPosition = 0;
            int maxMatrixPosition = 3;
            position.x = Mathf.Clamp01(position.x);
            position.y = Mathf.Clamp(position.y, minYposition, maxYposition);
            Vector2Int resullt = new Vector2Int(Mathf.RoundToInt((position.x - halfScale) / scale), Mathf.RoundToInt(Mathf.Abs((position.y + halfScale) / scale)));
            resullt.x = Mathf.Clamp(resullt.x, minMatrixPosition, maxMatrixPosition);
            resullt.y = Mathf.Clamp(resullt.y, minMatrixPosition, maxMatrixPosition);

            return resullt;
        }

        private IEnumerable<Vector2Int> GetRawItems(int value) => _allItems.Keys.Where(position => position.y == value);

        private IEnumerable<Vector2Int> GetColumnItems(int value) => _allItems.Keys.Where(position => position.x == value);
    }
}