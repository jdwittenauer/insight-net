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
    /// Class that encapsulates the Pearson Correlation algorithm.
    /// </summary>
    /// <remarks>
    /// Pearson correlation calculates the similarity between two vectors as the
    /// covariance of the two vectors divided by the product of their standard
    /// deviations.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Pearson_product-moment_correlation_coefficient"/>
    public sealed class PearsonCorrelation : ISimilarity
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PearsonCorrelation() { }

        /// <summary>
        /// Calculates the similarity between two vectors using Pearson correlation.
        /// </summary>
        /// <remarks>Range is -1 to 1</remarks>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>Similarity between the two vectors</returns>
        public double CalculateSimilarity(InsightVector u, InsightVector v)
        {
            if (u.Count != v.Count)
                throw new Exception("Vector lengths must be equal.");

            int length = u.Count;
            double uSum = 0, vSum = 0, uSumSquared = 0, vSumSquared = 0, productSum = 0;
            for (int i = 0; i < length; i++)
            {
                uSum += u[i];
                vSum += v[i];
                uSumSquared += u[i] * u[i];
                vSumSquared += v[i] * v[i];
                productSum += u[i] * v[i];
            }

            double numerator = productSum - ((uSum * vSum) / (double)length);
            double denominator = Math.Sqrt(
                (uSumSquared - ((uSum * uSum) / (double)length)) * 
                (vSumSquared - ((vSum * vSum) / (double)length)));

            return denominator == 0 ? 0 : numerator / denominator;
        }
    }
}
