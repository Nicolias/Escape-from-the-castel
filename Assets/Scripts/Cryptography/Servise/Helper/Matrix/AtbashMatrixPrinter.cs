using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Servis
{
    public class AtbashMatrixPrinter : MatrixPrinter
    {
        protected override char[,] BuildMatrix(string alphabet)
        {
            if (string.IsNullOrEmpty(alphabet))
                throw new ArgumentException("Алфавит не может быть пустым.", nameof(alphabet));

            int lenght = alphabet.Length;
            var matrix = new char[2, lenght];

            for (int i = 0; i < lenght; i++)
                matrix[0, i] = alphabet[i];

            List<char> reversAlphabet = alphabet.Reverse().ToList();

            for (int i = lenght; i > 0; i--)
                matrix[1, i - 1] = reversAlphabet[i - 1];

            return matrix;
        }
    }
}