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
    /// the nearest mean.
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

            // Until convergence point is reached (i.e. means stop changing by a significant amount)
            while (false)
            {
                // Assign each point to the nearest mean
                for (int i = 0; i < matrix.Data.RowCount; i++)
                {
                    // Compute the proximity to each centroid to find the closest match
                    double closestCentroid;
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
                // TODO
            }

            // Add the cluster assignments as a new column on the data set
            matrix.Data.Append(assignments.Data.ToColumnMatrix());

            return new ClusteringResults(centroids, matrix);
        }
    }
}
