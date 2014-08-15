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
    /// Class that encapsulates the meta K-means clustering algorithm.
    /// </summary>
    /// <remarks>
    /// This algorithm is a wrapper around the K-means clustering algorithm that aims
    /// to solve the bad convergence problem by running K-means with random initialization
    /// some set number of times and selecting the solution that produces the lowest
    /// amount of distortion.
    /// </remarks>
    public sealed class MetaKMeansClustering : IClusteringMethod
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MetaKMeansClustering() { }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        public IClusteringResults Cluster(InsightMatrix matrix)
        {
            return PerformMetaKMeansClustering(matrix, null, null, null);
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        public IClusteringResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters)
        {
            return PerformMetaKMeansClustering(matrix, comparisonMethod, clusters, null);
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <param name="iterations">Number of times to run the algorithm</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        public IClusteringResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters, int iterations)
        {
            return PerformMetaKMeansClustering(matrix, comparisonMethod, clusters, iterations);
        }

        /// <summary>
        /// Performs the meta K-Means clustering algorithm on the data set using the provided parameters.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="similarityMethod">Similarity measure used to compare instances</param>
        /// <param name="distanceMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        private IClusteringResults PerformMetaKMeansClustering(InsightMatrix matrix, 
            DistanceMethod? distanceMethod, int? clusters, int? iterations)
        {
            if (distanceMethod == null)
            {
                // Default to sum of squared error (equivalent to Euclidean distance)
                distanceMethod = DistanceMethod.EuclideanDistance;
            }

            if (clusters == null)
            {
                // Need to add some type of intelligent way to figure out a good number
                // of clusters to use based on an analysis of the data
                clusters = 3;
            }

            if (iterations == null)
            {
                // Default to 10 iterations
                iterations = 10;
            }

            // Use the first iteration to initialize the results
            var bestResults = new KMeansClustering().Cluster(matrix, distanceMethod.Value, clusters.Value);

            for (int i = 1; i < iterations; i++)
            {
                var results = new KMeansClustering().Cluster(matrix, distanceMethod.Value, clusters.Value);

                if (results.Distortion < bestResults.Distortion)
                {
                    bestResults = results;
                }
            }

            return bestResults;
        }
    }
}
