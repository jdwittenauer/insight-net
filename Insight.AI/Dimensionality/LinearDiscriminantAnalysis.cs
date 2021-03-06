﻿// Copyright (c) 2013 John Wittenauer (Insight.NET)

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
    /// Class that encapsulates the linear discriminant analysis algorithm.
    /// </summary>
    /// <remarks>
    /// Linear discriminant analysis projects the data set onto a hyperplane with c-1 dimensions 
    /// (where c is the number of distinct data classifications) that seeks to maximimize
    /// separability between classes.  Therefore, the objective of LDA is to perform dimension 
    /// reduction while preserving as much class discriminatory information as possible.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Linear_discriminant_analysis"/>
    public sealed class LinearDiscriminantAnalysis : IFeatureExtraction
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LinearDiscriminantAnalysis() { }

        /// <summary>
        /// Extracts the most important features from a data set using LDA.
        /// </summary>
        /// <remarks>Class information should be located in the first column of the data set</remarks>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix)
        {
            return PerformLDA(matrix, null, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using LDA.
        /// </summary>
        /// <remarks>Class information should be located in the first column of the data set</remarks>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, int featureLimit)
        {
            return PerformLDA(matrix, featureLimit, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using SVD.
        /// </summary>
        /// <remarks>Class information should be located in the first column of the data set</remarks>
        /// <param name="matrix">Input matrix</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, double percentThreshold)
        {
            return PerformLDA(matrix, null, percentThreshold);
        }

        /// <summary>
        /// Performs linear discriminant analysis on the input data set.  Extra parameters 
        /// are used to specify the critera or methodology used to limit the number of features 
        /// in the transformed data set.  Only one extra parameter must be specified.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        private InsightMatrix PerformLDA(InsightMatrix matrix, int? featureLimit, double? percentThreshold)
        {
            // Calculate the mean vector for the entire data set (skipping the class column)
            int columnCount = matrix.ColumnCount - 1;
            InsightVector totalMean = new InsightVector(columnCount);
            for (int i = 0; i < columnCount; i++)
            {
                totalMean[i] = matrix.Column(i).Mean();
            }

            // Derive a sub-matrix for each class in the data set
            List<InsightMatrix> classes = matrix.Decompose(columnCount);

            // Calculate the mean and covariance matrix for each class
            var meanVectors = new List<KeyValuePair<int, InsightVector>>();
            var covariances = new List<InsightMatrix>();
            foreach (var classMatrix in classes)
            {
                InsightVector means = new InsightVector(columnCount);
                for (int i = 0; i < columnCount; i++)
                {
                    means[i] = classMatrix.Column(i).Mean();
                }

                // Using a dictionary to keep the number of samples in the class in 
                // addition to the mean vector - we'll need both later on
                meanVectors.Add(new KeyValuePair<int, InsightVector>(classMatrix.RowCount, means));

                // Drop the class column then compute the covariance matrix for this class
                InsightMatrix covariance = classMatrix.SubMatrix(0, classMatrix.RowCount, 0, classMatrix.ColumnCount - 1);
                covariance = covariance.Center().CovarianceMatrix(true);
                covariances.Add(covariance);
            }

            // Calculate the within-class scatter matrix
            InsightMatrix withinClassScatter = covariances.Aggregate((x, y) => new InsightMatrix((x + y)));

            // Calculate the between-class scatter matrix
            InsightMatrix betweenClassScatter = meanVectors.Aggregate(
                new InsightMatrix(totalMean.Count), (x, y) => 
                    x + (y.Key * (y.Value - totalMean).ToColumnMatrix() * 
                    (y.Value - totalMean).ToColumnMatrix().Transpose()));

            // Compute the LDA projection and perform eigenvalue decomposition on the projected matrix
            InsightMatrix projection = new InsightMatrix(
                (withinClassScatter.Inverse() * betweenClassScatter));
            MatrixFactorization evd = projection.EigenvalueDecomposition();
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
                // Limit to a percent of the variance in the data set (represented by the sum of the eigenvalues)
                double totalVariance = evd.Eigenvalues.Sum() * percentThreshold.Value;
                double accumulatedVariance = 0;
                rank = 0;
                while (accumulatedVariance < totalVariance)
                {
                    accumulatedVariance += evd.Eigenvalues[rank];
                    rank++;
                }
            }

            // Extract the most important vectors (in order by eigenvalue size)
            InsightMatrix projectionVectors = new InsightMatrix(evd.Eigenvalues.Count, rank);
            for (int i = 0; i < rank; i++)
            {
                // Find the largest remaining eigenvalue
                int index = evd.Eigenvalues.MaxIndex();
                projectionVectors.SetColumn(i, evd.Eigenvectors.Column(index));

                // Set this position to zero so the next iteration captures the next-largest eigenvalue
                evd.Eigenvalues[index] = 0;
            }

            // Multiply each class matrix by the projection vectors
            for (int i = 0; i < classes.Count; i++)
            {
                // Save the class vector
                InsightVector classVector = classes[i].Column(0);

                // Create a new class matrix using the projection vectors
                classes[i] = (projectionVectors.Transpose() *
                    classes[i].SubMatrix(0, classes[i].RowCount, 1, classes[i].ColumnCount - 1)
                    .Transpose()).Transpose();
                
                // Insert the class vector back into the matrix
                classes[i] = classes[i].InsertColumn(0, classVector);
            }

            // Concatenate back into a single matrix
            InsightMatrix result = classes.Aggregate((x, y) => x.Stack(y));

            return result;
        }
    }
}
