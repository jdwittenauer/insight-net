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
    /// Class that encapsulates the simple hill climbing algorithm.
    /// </summary>
    /// <typeparam name="T">Type used to encode a solution</typeparam>
    /// <remarks>
    /// Hill climbing is an iterative local search algorithm that begins with an
    /// arbitrary solution and incrementally changes part of the solution in an
    /// attempt to find a better solution.  The process repeats until no further
    /// improvement is found.  Relies on a target function or heuristic to determine
    /// the quality of a solution.
    /// 
    /// Simple hill climbing is a variation of the algorithm that takes the first
    /// available solution that is considered "better" than the best solution that
    /// had previously been found.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Hill_climbing"/>
    public class SimpleHillClimbing<T> : HillClimbing<T>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SimpleHillClimbing() : base() { }

        /// <summary>
        /// Performs the simple hill climbing algorithm on the starting solution using the
        /// provided transforms and evaluation function.
        /// </summary>
        /// <param name="initialValue">The starting solution</param>
        /// <param name="transforms">List of functions describing how to generate new solutions</param>
        /// <param name="evaluate">Function describing how to score a solution</param>
        /// <param name="iterations">The maximum number of iterations to allow the algorithm to perform</param>
        /// <returns>Result set that includes the best solution found and the score of that solution</returns>
        protected override HillClimbingResults<T> PerformHillClimbing(
            T initialValue, List<Func<T, T>> transforms, Func<T, double> evaluate, int? iterations)
        {
            if (transforms.Count == 0 || transforms[0] == null)
                throw new Exception("Must provide at least 1 valid transform.");

            int iter = 0, maxIterations = int.MaxValue;
            if (iterations != null)
                maxIterations = iterations.Value;

            T bestSolution = initialValue, currentSolution = initialValue;
            double bestScore = double.MinValue, currentScore = double.MinValue;

            do
            {
                iter++;
                bestSolution = currentSolution;
                bestScore = currentScore;

                foreach (var transform in transforms)
                {
                    currentSolution = transform(bestSolution);
                    currentScore = evaluate(currentSolution);

                    if (currentScore > bestScore)
                        break;
                }
            } while (currentScore > bestScore && iter < maxIterations);

            return new HillClimbingResults<T>(bestSolution, bestScore);
        }
    }
}
