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
using Insight.AI.DataStructures;
using Insight.AI.Metrics.Interfaces;

namespace Insight.AI.Metrics
{
    /// <summary>
    /// Class that encapsulates the Euclidean Distance algorithm.
    /// </summary>
    /// <remarks>
    /// Euclidean distance (also called ruler or "ordinary" distance) calculates 
    /// the distance between two vectors as the length of the line segment separating
    /// the vectors in n-dimensional space, where n is the number of features in the
    /// vector.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Euclidean_distance"/>
    public sealed class EuclideanDistance : IDistance
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EuclideanDistance() { }

        /// <summary>
        /// Calculates the distance between two vectors using Euclidean distance.
        /// </summary>
        /// <remarks>Range is 0 to infinity</remarks>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>Distance between the two vectors</returns>
        public double CalculateDistance(InsightVector u, InsightVector v)
        {
            if (u.Count != v.Count)
                throw new Exception("Vector lengths must be equal.");

            int length = u.Count;
            double sumOfSquares = 0;
            for (int i = 0; i < length; i++)
            {
                sumOfSquares += (u[i] - v[i]) * (u[i] - v[i]);
            }

            return Math.Sqrt(sumOfSquares);
        }
    }
}
