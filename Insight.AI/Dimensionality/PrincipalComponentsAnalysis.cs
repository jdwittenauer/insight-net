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
using Insight.AI.DataStructures;
using Insight.AI.Dimensionality.Interfaces;

namespace Insight.AI.Dimensionality
{
    /// <summary>
    /// Class that encapsulates the principal components analysis algorithm.
    /// </summary>
    /// <remarks>
    /// Principal components analysis uses an orthogonal transform to convert a set
    /// of possibly correlated features into a set of linearly uncorrelated features
    /// called the principal components.  It reveals the internal structure of the
    /// data in a way that best explains the variance in the data.  Therefore, the
    /// objective of PCA is to perform dimension reduction while preserving as much
    /// of the randomness in the data as possible.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Principal_component_analysis"/>
    public sealed class PrincipalComponentsAnalysis : IFeatureExtraction
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PrincipalComponentsAnalysis() { }

        /// <summary>
        /// Extracts the most important features from a data set using PCA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix)
        {
            return PerformPCA(matrix, null, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using PCA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, int featureLimit)
        {
            return PerformPCA(matrix, featureLimit, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using PCA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, double percentThreshold)
        {
            return PerformPCA(matrix, null, percentThreshold);
        }

        /// <summary>
        /// Performs principal component analysis on the input data set.  Extra parameters 
        /// are used to specify the critera or methodology used to limit the number of features 
        /// in the transformed data set.  Only one extra parameter must be specified.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        private InsightMatrix PerformPCA(InsightMatrix matrix, int? featureLimit, double? percentThreshold)
        {
            // Center each feature and calculate the covariance matrix
            InsightMatrix covariance = matrix.Center().CovarianceMatrix(true);

            // Perform eigenvalue decomposition on the covariance matrix
            MatrixFactorization evd = covariance.EigenvalueDecomposition();
            int rank = evd.Eigenvalues.Where(x => x > 0.001).Count();

            // Determine the number of features to keep for the final data set
            if (featureLimit != null)
            {
                // Enforce a raw numeric feature limit
                if (rank > featureLimit)
                    rank = featureLimit.Value;
            }
            else if (percentThreshold != null)
            {
                // Limit to a percent of the variance in the data set
                // (represented by the sum of the eigenvalues)
                double totalVariance = evd.Eigenvalues.Sum() * percentThreshold.Value;
                double accumulatedVariance = 0;
                rank = 0;
                while (accumulatedVariance < totalVariance)
                {
                    accumulatedVariance += evd.Eigenvalues[rank];
                    rank++;
                }
            }

            // Extract the principal components (in order by eigenvalue size)
            InsightMatrix featureVectors = new InsightMatrix(evd.Eigenvalues.Count, rank);
            for (int i = 0; i < rank; i++)
            {
                // Find the largest remaining eigenvalue
                int index = evd.Eigenvalues.MaxIndex();
                featureVectors.SetColumn(i, evd.Eigenvectors.Column(index));

                // Set this position to zero so the next iteration captures the
                // next-largest eigenvalue
                evd.Eigenvalues[index] = 0;
            }

            // Calculate and return the reduced data set
            InsightMatrix result = (featureVectors.Transpose() * matrix.Transpose()).Transpose();

            return result;
        }
    }
}
