using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal class Matrix
    {
        public decimal[,] Values { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        /// <summary>
        /// Creates an identity matrix of the given size
        /// </summary>
        /// <param name="size"></param>
        public Matrix(int size)
        {
            Values = new decimal[size, size];

            for (int i = 0; i < size; i++)
                Values[i, i] = 1;

            Rows = size;
            Columns = size;
        }
        /// <summary>
        /// Creates a zero matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Matrix(int rows, int columns)
        {
            Values = new decimal[rows, columns];

            Rows = rows;
            Columns = columns;
        }
        /// <summary>
        /// Creates a matrix with all values set to value
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="value"></param>
        public Matrix (int rows, int columns, decimal value) : this(rows, columns)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    Values[i, j] = value;
        }
        /// <summary>
        /// Creates a matrix with the given values
        /// </summary>
        /// <param name="values"></param>
        public Matrix(decimal[,] values) : this(values.GetLength(0), values.GetLength(1))
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    Values[i,j] = values[i,j];
        }

        public decimal this[int row, int column]
        {
            get { return Values[row, column]; }
            set { Values[row, column] = value; }
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Columns != right.Rows)
                throw new ArgumentException("Matrix dimensions do not allow multiplication");

            Matrix product = new(left.Rows, right.Columns);

            int nVal = left.Columns;

            for (int r = 0; r < product.Rows; r++)
            {
                for (int c = 0; c < product.Columns; c++)
                {
                    decimal val = 0;

                    for (int n = 0; n < nVal; n++)
                        val += left[r, n] * right[n, c];

                    product[r, c] = val;
                }
            }

            return product;
        }

        public static Matrix operator *(Matrix matrix, decimal scalar)
        {
            Matrix product = new(matrix.Rows, matrix.Columns);

            for (int r = 0; r < product.Rows; r++)
                for (int c = 0; c < product.Columns; c++)
                    product[r, c] = matrix[r, c] * scalar;

            return product;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left.Columns != right.Columns || left.Rows != right.Rows)
                throw new ArgumentException("Matrix dimensions must match");

            Matrix sum = new(left.Rows, left.Columns);

            for (int r = 0; r < sum.Rows; r++)
                for (int c = 0; c < sum.Columns; c++)
                    sum[r, c] = left[r, c] + right[r, c];

            return sum;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Columns != right.Columns || left.Rows != right.Rows)
                throw new ArgumentException("Matrix dimensions must match");

            Matrix sum = new(left.Rows, left.Columns);

            for (int r = 0; r < sum.Rows; r++)
                for (int c = 0; c < sum.Columns; c++)
                    sum[r, c] = left[r, c] - right[r, c];

            return sum;
        }

        public static Matrix Transpose(Matrix matrix)
        {
            Matrix transpose = new(matrix.Columns, matrix.Rows);

            for (int r = 0; r < transpose.Rows; r++)
                for (int c = 0; c < transpose.Columns; c++)
                    transpose[r, c] = matrix[c, r];

            return transpose;
        }

        public void SwapRows(int row1, int row2)
        {
            for (int i = 0; i < Columns; i++)
            {
                (Values[row2, i], Values[row1, i]) = (Values[row1, i], Values[row2, i]);
            }
        }

        public void ScaleRow(int row, decimal scalor)
        {
            for (int i = 0; i < Columns; ++i)
            {
                Values[row, i] *= scalor;
            }
        }

        public void AddRow(int row1, int row2, decimal scalor = 1)
        {
            for (int i = 0; i < Columns; i++)
            {
                Values[row1, i] += Values[row2, i] * scalor;
            }
        }

        public bool TryInvert(out Matrix inverse)
        {
            inverse = new(Rows);

            if (Rows != Columns)
                return false;

            var gauss = new Matrix(Rows, Columns * 2);

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    gauss[i, j] = Values[i, j];
                }
                gauss[i, i + Columns] = 1;
            }

            for (int c = 0; c < Columns; c++)
            {
                if (gauss[c, c] == 0)
                    return false;

                // Set row lead to 1
                gauss.ScaleRow(c, 1 / gauss[c, c]);
                gauss[c, c] = 1;

                // Set rest of column to 0
                for (int i = 0; i < Rows; i++)
                {
                    if (i == c)
                        continue;

                    gauss.AddRow(c, i, -1 / gauss[i, c]);
                }
            }

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    inverse[i, j] = gauss[i, j + Columns];
                }
            }

            return true;
        }
    }
}
