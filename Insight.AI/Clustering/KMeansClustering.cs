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
        public IClusterResults Cluster(InsightMatrix matrix)
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
        public IClusterResults Cluster(InsightMatrix matrix, SimilarityMethod comparisonMethod, int clusters)
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
        public IClusterResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters)
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
        private IClusterResults PerformKMeansClustering(InsightMatrix matrix, SimilarityMethod? similarityMethod,
            DistanceMethod? distanceMethod, int? clusters)
        {
            throw new NotImplementedException();
        }
    }
}
