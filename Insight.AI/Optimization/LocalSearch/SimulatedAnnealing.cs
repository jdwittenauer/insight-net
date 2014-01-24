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
using Insight.AI.Optimization.Interfaces;

namespace Insight.AI.Optimization.LocalSearch
{
    /// <summary>
    /// Class that encapsulates the simulated annealing algorithm.
    /// </summary>
    /// <typeparam name="T">Type used to encode a solution</typeparam>
    /// <remarks>
    /// Simulated annealing is an iterative local search algorithm that is modeled
    /// after the therodynamic process of annealing.  The algorithm probabilistically
    /// decides to either move to a neighboring state of potentially lower energy (a
    /// better solution to the problem) or remain in its current state.  The probability
    /// changes as a function of the number of iterations.  Relies on a target function 
    /// or heuristic to determine the quality of a solution.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Simulated_annealing"/>
    public class SimulatedAnnealing<T> : ILocalSearch<T>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SimulatedAnnealing() { }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using simulated annealing.
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
            return PerformSimulatedAnnealing(initialValue, transforms, evaluate, null);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using simulated annealing.
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
            return PerformSimulatedAnnealing(initialValue, transforms, evaluate, iterations);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using simulated annealing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate)
        {
            return PerformSimulatedAnnealing(initialValue, transforms, evaluate, null);
        }

        /// <summary>
        /// Attempts to find the best solution to the given optimization problem using simulated annealing.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        public ILocalSearchResults<T> FindMaxima(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int iterations)
        {
            return PerformSimulatedAnnealing(initialValue, transforms, evaluate, iterations);
        }

        /// <summary>
        /// Performs the simulated annealing algorithm on the starting solution using the
        /// provided transforms and evaluation function.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        private SimulatedAnnealingResults<T> PerformSimulatedAnnealing(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int? iterations)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
