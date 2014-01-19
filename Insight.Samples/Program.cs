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
using System.Globalization;
using Insight.AI.DataStructures;
using Insight.AI.Metrics;
using Insight.AI.Dimensionality;
using Insight.AI.Optimization;

namespace Insight.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Similarity & Distance Examples");
            Console.WriteLine("------------------------------");
            Console.WriteLine(Environment.NewLine);

            InsightVector u = new InsightVector(new double[] { 1, 2, 3, 4, 5 });
            Console.WriteLine("Vector u:");
            Console.WriteLine(u.Data.ToString("F4", null));
            Console.WriteLine(Environment.NewLine);

            InsightVector v = new InsightVector(new double[] { 5, 4, 3, 2, 1 });
            Console.WriteLine("Vector v:");
            Console.WriteLine(v.Data.ToString("F4", null));
            Console.WriteLine(Environment.NewLine);

            double distance = u.DistanceFrom(v);
            Console.WriteLine("Euclidean distance (u, v) = {0}", distance.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            distance = u.DistanceFrom(v, DistanceMethod.HammingDistance);
            Console.WriteLine("Hamming distance (u, v) = {0}", distance.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            distance = u.DistanceFrom(v, DistanceMethod.ManhattanDistance);
            Console.WriteLine("Manhattan distance (u, v) = {0}", distance.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            double similarity = u.SimilarityTo(v);
            Console.WriteLine("Cosine similarity (u, v) = {0}", similarity.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            similarity = u.SimilarityTo(v, SimilarityMethod.JaccardCoefficient);
            Console.WriteLine("Jaccard coefficient (u, v) = {0}", similarity.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            similarity = u.SimilarityTo(v, SimilarityMethod.PearsonCorrelation);
            Console.WriteLine("Pearson correlation (u, v) = {0}", similarity.ToString("F4"));
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Feature Extraction Examples");
            Console.WriteLine("------------------------------");
            Console.WriteLine(Environment.NewLine);

            InsightMatrix matrix = new InsightMatrix(new double[,] { 
                { 2.5, 2.4 }, { 0.5, 0.7 }, { 2.2, 2.9 }, { 1.9, 2.2 }, { 3.1, 3.0 }, { 2.3, 2.7 }, 
                { 2.0, 1.6 }, { 1.0, 1.1 }, { 1.5, 1.6 }, { 1.1, 0.9 } 
            });
            Console.WriteLine("First test matrix:");
            Console.WriteLine(matrix.Data.ToString("F4", NumberFormatInfo.InvariantInfo));
            Console.WriteLine(Environment.NewLine);

            var pca = matrix.ExtractFeatures(ExtractionMethod.PrincipalComponentAnalysis, 1);
            Console.WriteLine("Result of principal components analysis:");
            Console.WriteLine(pca.Data.ToString("F4", NumberFormatInfo.InvariantInfo));
            Console.WriteLine(Environment.NewLine);

            var svd = matrix.ExtractFeatures(ExtractionMethod.SingularValueDecomposition, 1);
            Console.WriteLine("Result of singular value decomposition:");
            Console.WriteLine(svd.Data.ToString("F4", NumberFormatInfo.InvariantInfo));
            Console.WriteLine(Environment.NewLine);

            InsightMatrix matrix2 = new InsightMatrix(new double[,] { 
                { 1, 4, 2 }, { 1, 2, 4 }, { 1, 2, 3 }, { 1, 3, 6 }, { 1, 4, 4 }, 
                { 2, 9, 10 }, { 2, 6, 8 }, { 2, 9, 5 }, { 2, 8, 7 }, { 2, 10, 8 } 
            });
            Console.WriteLine("Second test matrix:");
            Console.WriteLine(matrix2.Data.ToString("F4", NumberFormatInfo.InvariantInfo));
            Console.WriteLine(Environment.NewLine);

            var lda = matrix2.ExtractFeatures(ExtractionMethod.LinearDiscriminantAnalysis);
            Console.WriteLine("Result of linear discriminant analysis:");
            Console.WriteLine(lda.Data.ToString("F4", NumberFormatInfo.InvariantInfo));
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Optimization Examples");
            Console.WriteLine("------------------------------");
            Console.WriteLine(Environment.NewLine);

            var hillClimber = new SimpleHillClimbing<double>();
            var transforms = new List<Func<double, double>>();
            transforms.Add(x => x + 1);
            transforms.Add(x => x - 1);
            transforms.Add(x => x + 0.3);
            transforms.Add(x => x - 0.3);
            transforms.Add(x => x + 0.1);
            transforms.Add(x => x - 0.1);
            var solution = hillClimber.FindMaxima(
                0, 
                transforms, 
                x => Math.Abs(x) - (x * x));
            Console.WriteLine("Solution = {0}", solution.Solution);
            Console.WriteLine("Score = {0}", solution.Score);
            Console.WriteLine(Environment.NewLine);
        }
    }
}
