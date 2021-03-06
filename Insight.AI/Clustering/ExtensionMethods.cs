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
using Insight.AI.Clustering;
using Insight.AI.Clustering.Interfaces;
using Insight.AI.DataStructures;
using Insight.AI.Metrics;

namespace Insight.AI.Clustering
{
    /// <summary>
    /// Extension methods for clustering a data set.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="clusteringMethod">Algorithm to use for clustering</param>
        /// <returns>Results of the cluster analysis</returns>
        public static IClusteringResults Cluster(this InsightMatrix matrix, ClusteringMethod clusteringMethod)
        {
            switch(clusteringMethod)
            {
                case ClusteringMethod.KMeans:
                    return new KMeansClustering().Cluster(matrix);
                default:
                    return new MetaKMeansClustering().Cluster(matrix);
            }
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="clusteringMethod">Algorithm to use for clustering</param>
        /// <param name="distanceMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Results of the cluster analysis</returns>
        public static IClusteringResults Cluster(this InsightMatrix matrix, ClusteringMethod clusteringMethod,
            DistanceMethod comparisonMethod, int clusters)
        {
            switch (clusteringMethod)
            {
                case ClusteringMethod.KMeans:
                    return new KMeansClustering().Cluster(matrix, comparisonMethod, clusters);
                default:
                    return new MetaKMeansClustering().Cluster(matrix, comparisonMethod, clusters);
            }
        }

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="clusteringMethod">Algorithm to use for clustering</param>
        /// <param name="distanceMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <param name="iterations">Number of times to run the algorithm</param>
        /// <returns>Results of the cluster analysis</returns>
        public static IClusteringResults Cluster(this InsightMatrix matrix, ClusteringMethod clusteringMethod,
            DistanceMethod comparisonMethod, int clusters, int iterations)
        {
            return new MetaKMeansClustering().Cluster(matrix, comparisonMethod, clusters, iterations);
        }
    }
}
