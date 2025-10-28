using System;

namespace Cryptography.Servis.Matrix
{
    public class ViginorMatrixPrinter : MatrixPrinter
    {
        protected override char[,] BuildMatrix(string alphabet)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentException("Алфавит не может быть пустым.", nameof(alphabet));

            int lenght = alphabet.Length;
            var matrix = new char[lenght, lenght];

            for (int row = 0; row < lenght; row++)
            {
                for (int column = 0; column < lenght; column++)
                {
                    int index = (column + row) % lenght;
                    matrix[row, column] = alphabet[index];
                }
            }

            return matrix;
        }
    }
}