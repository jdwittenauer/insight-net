// Copyright (c) 2013 John Wittenauer (Insight.NET)

// This file is part of Insight.NET.

// Insight.NET is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Insight.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with Insight.NET.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;

namespace Insight.AI.DataStructures
{
    /// <summary>
    /// This class encapsulates the matrix data structure used by Insight.NET.
    /// </summary>
    public class InsightMatrix
    {
        /// <summary>
        /// Math.NET matrix implementation.  Used for all matrix calculations.
        /// </summary>
        public Matrix Data { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InsightMatrix () { }

        /// <summary>
        /// Create a new matrix with the given order.  Cells will be initalized to zero.
        /// </summary>
        /// <param name="order">Matrix order</param>
        public InsightMatrix(int order)
        {
            Data = new DenseMatrix(order);
        }

        /// <summary>
        /// Create a new matrix as a copy of the given data array.
        /// </summary>
        /// <param name="array">Raw data array</param>
        public InsightMatrix(double[,] array)
        {
            Data = DenseMatrix.OfArray(array);
        }

        /// <summary>
        /// Create a new matrix with the given number of rows and columns.
        /// Cells will be initalized to zero.
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        public InsightMatrix(int rows, int columns)
        {
            Data = new DenseMatrix(rows, columns);
        }

        /// <summary>
        /// Create a new matrix as a copy of the given enumerable of enumerable columns.
        /// </summary>
        /// <param name="rows">Rows in the matrix</param>
        /// <param name="columns">Columns in the matrix</param>
        /// <param name="data">Enumerable data</param>
        public InsightMatrix(int rows, int columns, IEnumerable<IEnumerable<double>> data)
        {
            Data = DenseMatrix.OfColumns(rows, columns, data);
        }

        /// <summary>
        /// Create a new matrix as a copy of the given matrix.
        /// </summary>
        /// <param name="matrix">Matrix to copy</param>
        public InsightMatrix(InsightMatrix matrix)
        {
            Data = DenseMatrix.OfMatrix(matrix.Data);
        }

        /// <summary>
        /// Create a new matrix as a copy of the given Math.NET matrix.
        /// </summary>
        /// <param name="matrix">Matrix to copy</param>
        public InsightMatrix(Matrix matrix)
        {
            Data = matrix;
        }

        /// <summary>
        /// Centers each column in the matrix by subtracting each value in the column by the mean.
        /// </summary>
        /// <returns>Column-centered matrix</returns>
        public InsightMatrix Center()
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            int colLength = this.Data.ColumnCount;

            for (int i = 0; i < colLength; i++)
            {
                int length = this.Data.RowCount;
                double mean = this.Data.Column(i).Mean();

                for (int j = 0; j < length; j++)
                {
                    this.Data[j, i] = this.Data[j, i] - mean;
                }
            }

            return this;
        }

        /// <summary>
        /// Centers each column in the matrix by subtracting each value in the column by the mean.
        /// </summary>
        /// <param name="meanVector">Vector with pre-computed column means</param>
        /// <returns>Column-centered matrix</returns>
        public InsightMatrix Center(InsightVector meanVector)
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            int colLength = this.Data.ColumnCount;

            for (int i = 0; i < colLength; i++)
            {
                int length = this.Data.RowCount;

                for (int j = 0; j < length; j++)
                {
                    this.Data[j, i] = this.Data[j, i] - meanVector.Data[i];
                }
            }

            return this;
        }

        /// <summary>
        /// Scales each column in the matrix by dividing each value in the column by the square 
        /// root of the squared sum.  For a column that is already centered, this is equivalent 
        /// to the standard deviation.
        /// </summary>
        /// <returns>Column-scaled matrix</returns>
        public InsightMatrix Scale()
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            int colLength = this.Data.ColumnCount;

            for (int i = 0; i < colLength; i++)
            {
                int length = this.Data.RowCount;
                double ss = 0;

                for (int j = 0; j < length; j++)
                {
                    ss += this.Data[j, i] * this.Data[j, i];
                }

                double ss2 = Math.Sqrt(ss);

                for (int j = 0; j < length; j++)
                {
                    this.Data[j, i] = this.Data[j, i] / ss2;
                }
            }

            return this;
        }

        /// <summary>
        /// Normalizes each column in the matrix by centering and then scaling each column
        /// in the original matrix.
        /// </summary>
        /// <returns>Column-normalized matrix</returns>
        public InsightMatrix Normalize()
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            return this.Center().Scale();
        }

        /// <summary>
        /// Normalizes each column in the matrix by centering and then scaling each column
        /// in the original matrix.
        /// </summary>
        /// <param name="meanVector">Vector with pre-computed column means</param>
        /// <returns>Column-normalized matrix</returns>
        public InsightMatrix Normalize(InsightVector meanVector)
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            return this.Center(meanVector).Scale();
        }

        /// <summary>
        /// Calculates the covariance matrix.
        /// </summary>
        /// <param name="isCentered">Indicates if the data set is already centered</param>
        /// <returns>Covariance matrix</returns>
        public InsightMatrix CovarianceMatrix(bool isCentered)
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            int columns = this.Data.ColumnCount, rows = this.Data.RowCount;
            InsightMatrix cov = new InsightMatrix(columns);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (isCentered)
                    {
                        // Since each dimension is already centered, the numerator
                        // is simply the dot product of the 2 vectors
                        cov.Data[i, j] = (this.Data.Column(i) * this.Data.Column(j)) / (rows - 1);
                    }
                    else
                    {
                        double covariance = 0;
                        double imean = this.Data.Column(i).Mean(), jmean = this.Data.Column(j).Mean();
                        for (int k = 0; k < rows; k++)
                        {
                            covariance += (this.Data[i, k] - imean) * (this.Data[j, k] - jmean);
                        }

                        cov.Data[i, j] = covariance / (rows - 1);
                    }
                }
            }

            return cov;
        }

        /// <summary>
        /// Decomposes a matrix into n sub-matrices, where n is the number of distinct
        /// values in the column specified by column index.  Useful for breaking apart
        /// a matrix with classification label into individual class matrices.
        /// </summary>
        /// <param name="columnIndex">Column to use for decomposition</param>
        /// <returns>List of component matrices</returns>
        public List<InsightMatrix> Decompose(int columnIndex)
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");
            if (this.Data.ColumnCount < columnIndex)
                throw new Exception("Matrix size does not match specified column index.");

            // Sort the input matrix
            InsightMatrix sortedMatrix = this.Sort(columnIndex);

            // Get the distinct set of values
            int rowCount = sortedMatrix.Data.RowCount;
            double[] values = sortedMatrix.Data.SubMatrix(0, rowCount, columnIndex, 1)
                .ToRowWiseArray()
                .Distinct()
                .ToArray();

            List<InsightMatrix> components = new List<InsightMatrix>();
            int rowIndex = 0;
            for (int i = 0; i < values.Length; i++)
            {
                int startingIndex = rowIndex;
                while (rowIndex < rowCount && sortedMatrix.Data[rowIndex, columnIndex] == values[i])
                {
                    rowIndex++;
                }

                int rows = rowIndex - startingIndex;
                components.Add(new InsightMatrix(
                    (MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)sortedMatrix.Data.SubMatrix(
                    startingIndex, rows, 0, sortedMatrix.Data.ColumnCount)));
            }

            return components;
        }

        /// <summary>
        /// Sorts the rows of a matrix using the values in the designated column for comparison.
        /// </summary>
        /// <param name="columnIndex">Column to use for sorting</param>
        /// <returns>Row-sorted matrix</returns>
        private InsightMatrix Sort(int columnIndex)
        {
            if (this == null || this.Data == null)
                throw new Exception("Matrix must be instantiated.");

            var sortKeys = Enumerable.Range(0, this.Data.RowCount)
                .Select(x => new
                {
                    Index = x,
                    Value = this.Data[x, columnIndex]
                })
                .OrderBy(x => x.Value)
                .ToList();

            var sortKeys2 = sortKeys
                .Select((x, i) => new
                {
                    NewIndex = i,
                    OldIndex = x.Index,
                })
                .OrderBy(x => x.NewIndex)
                .ToList();

            var sortKeys3 = sortKeys2
                .Select(x => x.OldIndex)
                .ToList();

            for (int i = 0; i < this.Data.RowCount; i++)
            {
                // Save the row at the current index
                InsightVector temp = new InsightVector(this.Data.Row(i));

                // Copy the row from the new index to the current index
                this.Data.SetRow(i, this.Data.Row(sortKeys3[i]));

                // Copy the saved row to the new index
                this.Data.SetRow(sortKeys3[i], temp.Data);

                // Update the index to show row at position i is now at sortkeys[i]
                int position = sortKeys3.IndexOf(i, i);
                sortKeys3[position] = sortKeys3[i];
            }

            return this;
        }
    }
}
