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

namespace Insight.AI.Optimization.Interfaces
{
    /// <summary>
    /// Interface for performing a local search on an opimization problem.
    /// </summary>
    /// <typeparam name="T">Type used to encode a solution</typeparam>
    public interface ILocalSearch<T>
    {
        /// <summary>
        /// Attempts to find the best solution to the given optimization problem.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transform">Function describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        ILocalSearchResults<T> FindMaxima(
            T initialValue, Func<T, T> transform, Func<T, double> evaluate);

        /// Attempts to find the best solution to the given optimization problem.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transform">Function describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        ILocalSearchResults<T> FindMaxima(
            T initialValue, Func<T, T> transform, Func<T, double> evaluate, int iterations);

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate);

        /// Attempts to find the best solution to the given optimization problem.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int iterations);
    }
}
