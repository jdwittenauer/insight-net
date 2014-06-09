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

using Insight.AI.DataStructures;

namespace Insight.AI.Clustering.Interfaces
{
    /// <summary>
    /// Interface for returning the results of a clustering operation.
    /// </summary>
    public interface IClusteringResults
    {
        /// <summary>
        /// Gets a matrix of the centroids for each cluster.
        /// </summary>
        InsightMatrix Centroids { get; }

        /// <summary>
        /// Gets the original input data set with a new column appended that indicates
        /// the cluster that each instance belongs in.
        /// </summary>
        InsightMatrix Clusters { get; }
    }
}
