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
using Insight.AI.Clustering;
using Insight.AI.DataStructures;
using Insight.AI.Dimensionality;
using Insight.AI.Metrics;
using Insight.AI.Optimization.LocalSearch;
using Insight.AI.Prediction;
using Insight.AI.Preprocessing;

namespace Insight.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Similarity & Distance Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //InsightVector u = new InsightVector(new double[] { 1, 2, 3, 4, 5 });
            //Console.WriteLine("Vector u:");
            //Console.WriteLine(u.ToString());
            //Console.WriteLine(Environment.NewLine);

            //InsightVector v = new InsightVector(new double[] { 5, 4, 3, 2, 1 });
            //Console.WriteLine("Vector v:");
            //Console.WriteLine(v.ToString());
            //Console.WriteLine(Environment.NewLine);

            //double distance = u.DistanceFrom(v);
            //Console.WriteLine("Euclidean distance (u, v) = {0}", distance.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //distance = u.DistanceFrom(v, DistanceMethod.HammingDistance);
            //Console.WriteLine("Hamming distance (u, v) = {0}", distance.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //distance = u.DistanceFrom(v, DistanceMethod.ManhattanDistance);
            //Console.WriteLine("Manhattan distance (u, v) = {0}", distance.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //double similarity = u.SimilarityTo(v);
            //Console.WriteLine("Cosine similarity (u, v) = {0}", similarity.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //similarity = u.SimilarityTo(v, SimilarityMethod.JaccardCoefficient);
            //Console.WriteLine("Jaccard coefficient (u, v) = {0}", similarity.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //similarity = u.SimilarityTo(v, SimilarityMethod.PearsonCorrelation);
            //Console.WriteLine("Pearson correlation (u, v) = {0}", similarity.ToString("F4"));
            //Console.WriteLine(Environment.NewLine);

            //Console.ReadKey();

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Covariance & Correlation Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //InsightMatrix matrix = new InsightMatrix(new double[,] { 
            //    { 2.1, 8 }, { 2.5, 12 }, { 4.0, 14 }, { 3.6, 10 }
            //});

            //Console.WriteLine("Example matrix:");
            //Console.WriteLine(matrix.ToString());
            //Console.WriteLine(Environment.NewLine);

            //var cov = matrix.CovarianceMatrix();
            //Console.WriteLine("Covariance matrix:");
            //Console.WriteLine(cov.ToString());
            //Console.WriteLine(Environment.NewLine);

            //var cor = matrix.CorrelationMatrix();
            //Console.WriteLine("Correlation matrix:");
            //Console.WriteLine(cor.ToString());
            //Console.WriteLine(Environment.NewLine);

            //Console.ReadKey();

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Feature Extraction Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //InsightMatrix matrix2 = new InsightMatrix(new double[,] { 
            //    { 2.5, 2.4 }, { 0.5, 0.7 }, { 2.2, 2.9 }, { 1.9, 2.2 }, { 3.1, 3.0 }, { 2.3, 2.7 }, 
            //    { 2.0, 1.6 }, { 1.0, 1.1 }, { 1.5, 1.6 }, { 1.1, 0.9 } 
            //});
            //Console.WriteLine("First test matrix:");
            //Console.WriteLine(matrix2.ToString());
            //Console.WriteLine(Environment.NewLine);

            //var pca = matrix2.ExtractFeatures(ExtractionMethod.PrincipalComponentAnalysis, 1);
            //Console.WriteLine("Result of principal components analysis:");
            //Console.WriteLine(pca.ToString());
            //Console.WriteLine(Environment.NewLine);

            //var svd = matrix2.ExtractFeatures(ExtractionMethod.SingularValueDecomposition, 1);
            //Console.WriteLine("Result of singular value decomposition:");
            //Console.WriteLine(svd.ToString());
            //Console.WriteLine(Environment.NewLine);

            //InsightMatrix matrix3 = new InsightMatrix(new double[,] { 
            //    { 1, 4, 2 }, { 1, 2, 4 }, { 1, 2, 3 }, { 1, 3, 6 }, { 1, 4, 4 }, 
            //    { 2, 9, 10 }, { 2, 6, 8 }, { 2, 9, 5 }, { 2, 8, 7 }, { 2, 10, 8 } 
            //});
            //Console.WriteLine("Second test matrix:");
            //Console.WriteLine(matrix3.ToString());
            //Console.WriteLine(Environment.NewLine);

            //var lda = matrix3.ExtractFeatures(ExtractionMethod.LinearDiscriminantAnalysis);
            //Console.WriteLine("Result of linear discriminant analysis:");
            //Console.WriteLine(lda.ToString());
            //Console.WriteLine(Environment.NewLine);

            //Console.ReadKey();

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Optimization Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //var simpleHillClimber = new SimpleHillClimbing<double>();
            //var transforms = new List<Func<double, double>>();
            //transforms.Add(x => x + 1);
            //transforms.Add(x => x - 1);
            //transforms.Add(x => x + 0.3);
            //transforms.Add(x => x - 0.3);
            //transforms.Add(x => x + 0.1);
            //transforms.Add(x => x - 0.1);
            //var solution = simpleHillClimber.FindMaxima(
            //    0,
            //    transforms,
            //    x => Math.Abs(x) - (x * x));
            //Console.WriteLine("Simple hill climber solution = {0}", solution.Solution);
            //Console.WriteLine("Simple hill climber score = {0}", solution.Score);
            //Console.WriteLine(Environment.NewLine);

            //var steepestAscentHillClimber = new SteepestAscentHillClimbing<double>();
            //var solution2 = steepestAscentHillClimber.FindMaxima(
            //    0,
            //    transforms,
            //    x => Math.Abs(x) - (x * x));
            //Console.WriteLine("Steepest ascent hill climber solution = {0}", solution2.Solution);
            //Console.WriteLine("Steepest ascent hill climber score = {0}", solution2.Score);
            //Console.WriteLine(Environment.NewLine);

            //var stocasticHillClimber = new StochasticHillClimbing<double>();
            //var solution3 = stocasticHillClimber.FindMaxima(
            //    0,
            //    transforms,
            //    x => Math.Abs(x) - (x * x));
            //Console.WriteLine("Stocastic hill climber solution = {0}", solution3.Solution);
            //Console.WriteLine("Stocastic hill climber score = {0}", solution3.Score);
            //Console.WriteLine(Environment.NewLine);

            //Console.ReadKey();

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Data Loading Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //InsightMatrix iris = DataLoader.ImportFromCSV("../../../data/iris.data", ',', false, 4, true);

            //Console.WriteLine("Iris data set:");
            //Console.WriteLine(iris.ToString());

            //Console.ReadKey();

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Clustering Examples");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine(Environment.NewLine);

            //var clusterResults = iris.Cluster(ClusteringMethod.KMeans);
            //Console.WriteLine("K-Means");
            //Console.WriteLine("Distortion = {0}", clusterResults.Distortion);
            //Console.WriteLine("Centroids:");
            //Console.WriteLine(clusterResults.Centroids.ToString());

            //Console.ReadKey();

            //var clusterResults2 = iris.Cluster(ClusteringMethod.KMeans, DistanceMethod.EuclideanDistance, 3, 10);
            //Console.WriteLine("K-Means (best of 10)");
            //Console.WriteLine("Distortion = {0}", clusterResults.Distortion);
            //Console.WriteLine("Centroids:");
            //Console.WriteLine(clusterResults.Centroids.ToString());

            //Console.ReadKey();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Linear Regression");
            Console.WriteLine("------------------------------");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Loading regression sample data...");
            InsightMatrix data1 = DataLoader.ImportFromCSV("../../../data/ML/ex1data1.txt", ',', false, 1, false);

            Console.WriteLine("Training model...");
            var regression = new LinearRegression();
            regression.Train(data1);
            Console.WriteLine("Model training complete.  Parameters:");
            Console.WriteLine(regression.Theta.ToString());

            Console.WriteLine("Predicting output for first data point...");
            data1 = data1.RemoveColumn(data1.Label);
            var prediction = regression.Predict(data1.Row(0));

            // TODO - Not getting correct answer, need to debug computation steps
            Console.WriteLine("Prediction = {0}", prediction);

            Console.ReadKey();
        }
    }
}
