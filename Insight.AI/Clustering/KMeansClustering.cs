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
using System.Text;
using Insight.AI.Clustering.Interfaces;
using Insight.AI.DataStructures;
using Insight.AI.Metrics;

namespace Insight.AI.Clustering
{
    /// <summary>
    /// Class that encapsulates the K-Means clustering algorithm.
    /// </summary>
    /// <remarks>
    /// K-Means clustering is a form of vector quantization that aims to partition
    /// n instances into k clusters in which each instance belongs to the cluster with
    /// the nearest mean.  The algorithm initializes k centroids using randomly selected
    /// instances of the data.  It then assigns each instance to the nearest centroid,
    /// and computes the new centroid to the cluster.  This process is repeated until
    /// convergence is reached.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/K-means_clustering"/>
    public sealed class KMeansClustering : IClusteringMethod
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public KMeansClustering() { }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Result set that includes the clusters defined by the algorithm</returns>
        public IClusteringResults Cluster(InsightMatrix matrix)
        {
            return PerformKMeansClustering(matrix, null, null, null);
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Similarity measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes the clusters defined by the algorithm</returns>
        public IClusteringResults Cluster(InsightMatrix matrix, SimilarityMethod comparisonMethod, int clusters)
        {
            return PerformKMeansClustering(matrix, comparisonMethod, null, clusters);
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes the clusters defined by the algorithm</returns>
        public IClusteringResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters)
        {
            return PerformKMeansClustering(matrix, null, comparisonMethod, clusters);
        }

        /// <summary>
        /// Performs the K-Means clustering algorithm on the data set using the provided parameters.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="similarityMethod">Similarity measure used to compare instances</param>
        /// <param name="distanceMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes the clusters defined by the algorithm</returns>
        private IClusteringResults PerformKMeansClustering(InsightMatrix matrix, SimilarityMethod? similarityMethod,
            DistanceMethod? distanceMethod, int? clusters)
        {
            if (matrix == null || matrix.Data == null)
                throw new Exception("Matrix must be instantiated.");

            bool useSimilarity;
            if (similarityMethod != null)
            {
                useSimilarity = true;
            }
            else if (distanceMethod != null)
            {
                useSimilarity = false;
            }
            else
            {
                similarityMethod = SimilarityMethod.PearsonCorrelation;
                useSimilarity = true;
            }

            if (clusters == null)
            {
                // Need to add some type of intelligent way to figure out a good number
                // of clusters to use based on an analysis of the data
                clusters = 3;
            }

            var assignments = new InsightVector(matrix.Data.RowCount);
            var centroids = new InsightMatrix(clusters.Value, matrix.Data.ColumnCount);

            // Initialize means via random selection
            for (int i = 0; i < clusters; i++)
            {
                var random = new Random();
                centroids.Data.SetRow(i, matrix.Data.Row(random.Next(0, matrix.Data.RowCount - 1)));
            }

            // Keep going until convergence point is reached
            while (true)
            {
                // Assign each point to the nearest mean
                // TODO - There's a bug in here somewhere that's causing a cluster to end up with 0 assignment
                for (int i = 0; i < matrix.Data.RowCount; i++)
                {
                    // Compute the proximity to each centroid to find the closest match
                    double closestCentroid = -1;
                    for (int j = 0; j < clusters; j++)
                    {
                        double proximity;
                        if (useSimilarity)
                            proximity = matrix.Row(i).SimilarityTo(centroids.Row(j));
                        else
                            proximity = matrix.Row(i).DistanceFrom(centroids.Row(j));

                        if (j == 0)
                        {
                            closestCentroid = proximity;
                            assignments.Data[i] = j;
                        }
                        else if (proximity < closestCentroid)
                        {
                            closestCentroid = proximity;
                            assignments.Data[i] = j;
                        }
                    }
                }

                // Calculate the new means for each centroid
                var newCentroids = new InsightMatrix(clusters.Value, matrix.Data.ColumnCount);
                bool converged = true;

                for (int i = 0; i < clusters; i++)
                {
                    int instanceCount = assignments.Data.Where(x => x == i).Count();

                    // Compute the means for each instance assigned to the current cluster
                    for (int j = 0; j < newCentroids.Data.ColumnCount; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < matrix.Data.RowCount; k++)
                        {
                            if (assignments.Data[k] == i) sum += matrix.Data[k, j];
                        }

                        if (instanceCount > 0)
                            newCentroids.Data[i, j] = sum / instanceCount;
                        else
                            newCentroids.Data[i, j] = centroids.Data[i, j];
                    }

                    if (centroids.Data.Row(i) != newCentroids.Data.Row(i))
                        converged = false;
                }

                // If the new centroid means did not change then we've reached the final result
                if (converged) break;
            }

            // Add the cluster assignments as a new column on the data set
            matrix.Data.Append(assignments.Data.ToColumnMatrix());

            return new ClusteringResults(centroids, matrix);
        }
    }
}
