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
using Insight.AI.Optimization.Interfaces;

namespace Insight.AI.Optimization
{
    /// <summary>
    /// Class that encapsulates the hill climbing algorithm.
    /// </summary>
    /// <typeparam name="T">Type used to encode a solution</typeparam>
    /// <remarks>
    /// Hill climbing is an iterative local search algorithm that begins with an
    /// arbitrary solution and incrementally changes part of the solution in an
    /// attempt to find a better solution.  The process repeats until no further
    /// improvement is found.  Relies on a target function or heuristic to determine
    /// the quality of a solution.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Hill_climbing"/>
    public abstract class HillClimbing<T> : ILocalSearch<T>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected HillClimbing() { }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using hill climbing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transform">Function describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, Func<T, T> transform, Func<T, double> evaluate)
        {
            var transforms = new List<Func<T, T>>();
            transforms.Add(transform);
            return PerformHillClimbing(initialValue, transforms, evaluate, null);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using hill climbing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transform">Function describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, Func<T, T> transform, Func<T, double> evaluate, int iterations)
        {
            var transforms = new List<Func<T, T>>();
            transforms.Add(transform);
            return PerformHillClimbing(initialValue, transforms, evaluate, iterations);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using hill climbing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate)
        {
            return PerformHillClimbing(initialValue, transforms, evaluate, null);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using hill climbing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int iterations)
        {
            return PerformHillClimbing(initialValue, transforms, evaluate, iterations);
        }

        /// <summary>
        /// Performs the hill climbing algorithm on the starting solution using the
        /// provided transforms and evaluation function.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        protected abstract HillClimbingResults<T> PerformHillClimbing(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int? iterations);
    }
}
