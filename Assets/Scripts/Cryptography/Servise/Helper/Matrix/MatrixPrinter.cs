using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Servis
{
    public abstract class MatrixPrinter : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private RectTransform _canvas;

        [SerializeField] private CharFactory _charFactory;
        [SerializeField] private float _spacing;
        [SerializeField] private Vector3 _firstSpawnPosition;

        [SerializeField] private Locolizer _locolizer;

        private void Awake()
        {
            _gridLayoutGroup.constraintCount = _locolizer.CurrentAlphabet.Length;
            PrintMatrix(BuildMatrix(_locolizer.CurrentAlphabet));
        }

        private void FixedUpdate()
        {
            _gridLayoutGroup.cellSize = new Vector2(_canvas.rect.width / _locolizer.CurrentAlphabet.Length, _canvas.rect.height / _locolizer.CurrentAlphabet.Length);
        }

        protected abstract char[,] BuildMatrix(string alphabet);

        private void PrintMatrix(char[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            Vector3 spawnPosition = _firstSpawnPosition;

            for (int i = 0; i < rows; i++)
            {
                for (int n = 0; n < columns; n++)
                {
                    spawnPosition.x += _spacing;
                    spawnPosition.z = 0;

                    _charFactory.Create(spawnPosition, Quaternion.identity, matrix[i, n].ToString());
                }

                spawnPosition.x = _firstSpawnPosition.x;
                spawnPosition.y -= _spacing;
            }
        }
    }
}