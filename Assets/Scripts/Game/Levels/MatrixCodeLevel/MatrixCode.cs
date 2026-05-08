using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Levels.MatrixGame
{
    public class MatrixCode
    {
        private string[,] _matrix;

        public MatrixCode()
        {
            Width = 4;
            Height = 4;
            _matrix = new string[Height, Width];
            List<string> keys = new List<string>()
            {
                "1C",
                "55",
                "3f",
                "4A",
                "8R",
                "2U",
                "0J"
            };

            GenerateMatrix(keys);
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string GetItem(int row, int column) => _matrix[row, column];

        private void GenerateMatrix(List<string> codeItems)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int randomIndex = Random.Range(0, codeItems.Count);
                    _matrix[i, j] = codeItems[randomIndex];
                }
            }
        }
    }
}