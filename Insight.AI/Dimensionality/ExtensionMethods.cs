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
    /// <summary>
    /// Extension methods for dimension reduction on a data set.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extracts the most important features from a data set.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="extractionMethod">Algorithm to use for feature extraction</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public static InsightMatrix ExtractFeatures(this InsightMatrix matrix, ExtractionMethod extractionMethod)
        {
            switch (extractionMethod)
            {
                case ExtractionMethod.LinearDiscriminantAnalysis:
                    return new LinearDiscriminantAnalysis().ExtractFeatures(matrix);
                case ExtractionMethod.PrincipalComponentAnalysis:
                    return new PrincipalComponentsAnalysis().ExtractFeatures(matrix);
                default:
                    return new SingularValueDecomposition().ExtractFeatures(matrix);
            }
        }

        /// <summary>
        /// Extracts the most important features from a data set.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="extractionMethod">Algorithm to use for feature extraction</param>
        /// <param name="featureLimit">Maximum number of features in the new data set</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public static InsightMatrix ExtractFeatures(this InsightMatrix matrix, 
            ExtractionMethod extractionMethod, int featureLimit)
        {
            switch (extractionMethod)
            {
                case ExtractionMethod.LinearDiscriminantAnalysis:
                    return new LinearDiscriminantAnalysis().ExtractFeatures(matrix, featureLimit);
                case ExtractionMethod.PrincipalComponentAnalysis:
                    return new PrincipalComponentsAnalysis().ExtractFeatures(matrix, featureLimit);
                default:
                    return new SingularValueDecomposition().ExtractFeatures(matrix, featureLimit);
            }
        }

        /// <summary>
        /// Extracts the most important features from a data set.
        /// </summary>
        /// <param name="matrix">Input matrix</param>
        /// <param name="extractionMethod">Algorithm to use for feature extraction</param>
        /// <param name="percentThreshold">Specifies the percent of the concept variance to use
        /// in limiting the number of features selected for the new data set (range 0-1)</param>
        /// <returns>Transformed matrix with reduced number of dimensions</returns>
        public static InsightMatrix ExtractFeatures(this InsightMatrix matrix, 
            ExtractionMethod extractionMethod, double percentThreshold)
        {
            switch (extractionMethod)
            {
                case ExtractionMethod.LinearDiscriminantAnalysis:
                    return new LinearDiscriminantAnalysis().ExtractFeatures(matrix, percentThreshold);
                case ExtractionMethod.PrincipalComponentAnalysis:
                    return new PrincipalComponentsAnalysis().ExtractFeatures(matrix, percentThreshold);
                default:
                    return new SingularValueDecomposition().ExtractFeatures(matrix, percentThreshold);
            }
        }
    }
}
