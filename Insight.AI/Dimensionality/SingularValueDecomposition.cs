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
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using Insight.AI.DataStructures;
using Insight.AI.Dimensionality.Interfaces;

namespace Insight.AI.Dimensionality
{
    /// <summary>
    /// Class that encapsulates the singular value decomposition algorithm.
    /// </summary>
    /// <remarks>
    /// Singular value decomposition factorizes a data set in a way that reveals
    /// the strength of the relationship between features (also called terms), samples
    /// (also called documents), and concepts in a data set.  Dimension reduction can
    /// be performed by taking the strongest concepts (represented as singular values)
    /// and composing a new data set with less features.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Singular_value_decomposition"/>
    public sealed class SingularValueDecomposition : IFeatureExtraction
    {
        /// <summary>
        /// Gets the number of features kept after performing SVD.
        /// </summary>
        public int Rank { get; private set; }

        /// <summary>
        /// Gets the singular values of the input data set.
        /// </summary>
        public InsightVector SingularValues { get; private set; }

        /// <summary>
        /// Gets the left singular vectors of the input data set.
        /// </summary>
        public InsightMatrix LeftSingularVectors { get; private set; }

        /// <summary>
        /// Gets the right singular vectors of the input data set.
        /// </summary>
        public InsightMatrix RightSingularVectors { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SingularValueDecomposition() { }

        /// <summary>
        /// Extracts the most important features from a data set using SVD.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix)
        {
            return PerformSVD(matrix, null, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using SVD.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, int featureLimit)
        {
            return PerformSVD(matrix, featureLimit, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using SVD.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, double percentThreshold)
        {
            return PerformSVD(matrix, null, percentThreshold);
        }

        /// <summary>
        /// Performs singular value decomposition on the input data set.  Extra parameters 
        /// are used to specify the critera or methodology used to limit the number of features 
        /// in the transformed data set.  Only one extra parameter must be specified.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        private InsightMatrix PerformSVD(InsightMatrix matrix, int? featureLimit, double? percentThreshold)
        {
            if (matrix == null || matrix.Data == null)
                throw new Exception("Matrix must be instantiated.");

            // Perform singlular value decomposition on the matrix
            // and retrieve the rank (number of singular values)
            Svd svd = matrix.Data.Svd(true);
            int rows = matrix.Data.RowCount, columns = matrix.Data.ColumnCount;
            Rank = svd.Rank;
            SingularValues = new InsightVector(svd.S());
            LeftSingularVectors = new InsightMatrix((DenseMatrix)svd.W());
            RightSingularVectors = new InsightMatrix((DenseMatrix)svd.VT());

            // Determine the number of features to keep for the final data set
            // (default will use all available singular values)
            if (featureLimit != null)
            {
                // Enforce a raw numeric feature limit
                if (Rank > featureLimit)
                    Rank = featureLimit.Value;
            }
            else if (percentThreshold != null)
            {
                // Limit to a percent of the variance in the data set
                // (represented by the sum of the singular values)
                double totalVariance = SingularValues.Data.Sum() * percentThreshold.Value;
                double accumulatedVariance = 0;
                Rank = 0;
                while (accumulatedVariance < totalVariance)
                {
                    accumulatedVariance += SingularValues.Data[Rank];
                    Rank++;
                }
            }

            // Re-compose the original matrix using a sub-set of the features
            InsightMatrix result = new InsightMatrix((DenseMatrix)
                (svd.U().SubMatrix(0, rows, 0, rows) *
                LeftSingularVectors.Data.SubMatrix(0, rows, 0, Rank) * 
                RightSingularVectors.Data.SubMatrix(0, Rank, 0, Rank)));

            return result;
        }
    }
}
