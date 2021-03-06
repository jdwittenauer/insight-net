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

using Insight.AI.DataStructures;
using Insight.AI.Metrics;

namespace Insight.AI.Clustering.Interfaces
{
    /// <summary>
    /// Interface for clustering a data set.
    /// </summary>
    public interface IClusteringMethod
    {
        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        IClusteringResults Cluster(InsightMatrix matrix);

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        IClusteringResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters);

        /// <summary>
        /// Cluster the data set into groups of similar instances.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="comparisonMethod">Distance measure used to compare instances</param>
        /// <param name="clusters">Number of desired clusters</param>
        /// <param name="iterations">Number of times to run the algorithm</param>
        /// <returns>Result set that includes cluster centroids, cluster assignments, and total distortion</returns>
        IClusteringResults Cluster(InsightMatrix matrix, DistanceMethod comparisonMethod, int clusters, int iterations);
    }
}
