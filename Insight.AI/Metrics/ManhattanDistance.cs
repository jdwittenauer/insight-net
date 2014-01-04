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
using Insight.AI.DataStructures;
using Insight.AI.Metrics.Interfaces;

namespace Insight.AI.Metrics
{
    /// <summary>
    /// Class that encapsulates the Manhattan Distance algorithm.
    /// </summary>
    /// <remarks>
    /// Manhattan distance (also called "city block" distance) calculates
    /// the distance between two vectors as the sum of the absolute differences
    /// of their values.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public sealed class ManhattanDistance : IDistance
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManhattanDistance() { }

        /// <summary>
        /// Calculates the distance between two vectors using Manhattan distance.
        /// </summary>
        /// <remarks>Range is 0 to infinity</remarks>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>Distance between the two vectors</returns>
        public double CalculateDistance(InsightVector u, InsightVector v)
        {
            if (u == null || u.Data == null)
                throw new Exception("Vector u must be instantiated.");
            if (v == null || u.Data == null)
                throw new Exception("Vector v must be instantiated.");
            if (u.Data.Count != v.Data.Count)
                throw new Exception("Vector lengths must be equal.");

            int length = u.Data.Count;
            double distance = 0;
            for (int i = 0; i < length; i++)
            {
                distance += Math.Abs(u.Data[i] - v.Data[i]);
            }

            return distance;
        }
    }
}
