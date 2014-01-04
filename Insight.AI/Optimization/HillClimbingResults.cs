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

using Insight.AI.Optimization.Interfaces;

namespace Insight.AI.Optimization
{
    /// <summary>
    /// Class that encapsulates the results the hill climbing algorithm.
    /// </summary>
    /// <typeparam name="T">Type used to encode a solution</typeparam>
    public class HillClimbingResults<T> : ILocalSearchResults<T>
    {
        /// <summary>
        /// Gets the best solution found by the optimizer.
        /// </summary>
        public T Solution { get; private set; }

        /// <summary>
        /// Gets the score of the best solution.
        /// </summary>
        public double Score { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="solution">Best solution found by the optimizer</param>
        /// <param name="score">Score of the best solution</param>
        public HillClimbingResults(T solution, double score)
        {
            Solution = solution;
            Score = score;
        }
    }
}
