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
using Insight.AI.Dimensionality.Interfaces;

namespace Insight.AI.Dimensionality
{
    public class IndependentComponentAnalysis : IFeatureExtraction
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public IndependentComponentAnalysis() { }

        /// <summary>
        /// Extracts the most important features from a data set using ICA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix)
        {
            return PerformICA(matrix, null, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using ICA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, int featureLimit)
        {
            return PerformICA(matrix, featureLimit, null);
        }

        /// <summary>
        /// Extracts the most important features from a data set using ICA.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public InsightMatrix ExtractFeatures(InsightMatrix matrix, double percentThreshold)
        {
            return PerformICA(matrix, null, percentThreshold);
        }

        private InsightMatrix PerformICA(InsightMatrix matrix, int? featureLimit, double? percentThreshold)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
