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

namespace Insight.AI.DataStructures
{
    public enum ClassificationMethod
    {
        DecisionTree,
        KNearestNeighbor,
        LogisticRegression,
        NaiveBayes,
        NeuralNetwork,
        StochasticGradientDescent,
        SupportVectorMachine
    }

    public enum ClusteringMethod
    {
        Agglomerative,
        KMeans
    }

    public enum ExtractionMethod
    {
        IndependentComponentAnalysis,
        LinearDiscriminantAnalysis,
        MultiDimensionalScaling,
        PrincipalComponentAnalysis,
        SingularValueDecomposition
    }

    public enum DistanceMethod
    {
        EuclideanDistance,
        HammingDistance,
        ManhattanDistance
    }

    public enum SimilarityMethod
    {
        CosineSimilarity,
        JaccardCoefficient,
        PearsonCorrelation
    }

    public enum RecommendationMethod
    {
        ContentBasedRecommendation,
        HybridCollaborativeFiltering,
        ModelCollaborativeFiltering,
        NeighborhoodCollaborativeFiltering
    }

    public enum RegressionMethod
    {
        LinearRegression,
        NeuralNetwork,
        RidgeRegression,
        StochasticGradientDescent,
        SupportVectorMachine
    }
}
