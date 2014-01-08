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
    /// Extension methods for similary & distance metrics.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Calculates the similarity between two vectors.  Uses cosine similarity by default.
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>Similarity between the two vectors</returns>
        public static double SimilarityTo(this InsightVector u, InsightVector v)
        {
            return new CosineSimilarity().CalculateSimilarity(u, v);
        }

        /// <summary>
        /// Calculates the similarity between two vectors.
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <param name="similarityMethod">Algorithm to use for the similarity calculation</param>
        /// <returns>Similarity between the two vectors</returns>
        public static double SimilarityTo(this InsightVector u, InsightVector v, SimilarityMethod similarityMethod)
        {
            switch (similarityMethod)
            {
                case SimilarityMethod.CosineSimilarity:
                    return new CosineSimilarity().CalculateSimilarity(u, v);
                case SimilarityMethod.JaccardCoefficient:
                    return new JaccardCoefficient().CalculateSimilarity(u, v);
                default:
                    return new PearsonCorrelation().CalculateSimilarity(u, v);
            }
        }

        /// <summary>
        /// Calculates the distance between two vectors.  Uses euclidean distance by default.
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <returns>Distance between the two vectors</returns>
        public static double DistanceFrom(this InsightVector u, InsightVector v)
        {
            return new EuclideanDistance().CalculateDistance(u, v);
        }

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        /// <param name="u">1st vector</param>
        /// <param name="v">2nd vector</param>
        /// <param name="distanceMethod">Algorithm to use for the distance calculation</param>
        /// <returns>Distance between the two vectors</returns>
        public static double DistanceFrom(this InsightVector u, InsightVector v, DistanceMethod distanceMethod)
        {
            switch (distanceMethod)
            {
                case DistanceMethod.EuclideanDistance:
                    return new EuclideanDistance().CalculateDistance(u, v);
                case DistanceMethod.HammingDistance:
                    return new HammingDistance().CalculateDistance(u, v);
                default:
                    return new ManhattanDistance().CalculateDistance(u, v);
            }
        }
    }
}
