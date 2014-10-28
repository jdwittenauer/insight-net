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
using System.Linq;
using System.Text;
using Insight.AI.DataStructures;
using Insight.AI.Prediction.Interfaces;

namespace Insight.AI.Prediction
{
    /// <summary>
    /// Class that encapsulates the linear regression algorithm.
    /// </summary>
    /// <remarks>
    /// Linear regression is an approach to modeling the relationship between a scalar
    /// dependent variable (what you want to predict) and one or more independent 
    /// variables (features).  Linear regression works by minimizing the L2 loss function,
    /// or the squared error, between the target and the estimated value predicted by the
    /// model.
    /// 
    /// There are multiple training algorithms that can be used to build a linear regression
    /// model.  This implementation uses batch gradient descent to learn the model parameters.
    /// A regularization term can optionally be included to encourage smaller parameter values.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Linear_regression"/>
    public sealed class LinearRegression : IRegression
    {
        /// <summary>
        /// The learning rate for the algorithm.
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// Regularization term for the algorithm.  Defaults to zero (no regularization).
        /// </summary>
        public double Lambda { get; set; }

        /// <summary>
        /// The number of training iterations to run.
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// The parameter vector for the algorithm.
        /// </summary>
        public InsightVector Theta { get; private set; }

        /// <summary>
        /// A vector of the total error at each training iteration.
        /// </summary>
        public InsightVector Error { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LinearRegression() 
        {
            // Default to reasonable starting values
            Alpha = 0.01;
            Lambda = 0;
            Iterations = 1000;
        }

        /// <summary>
        /// Trains the model using the supplied data.  Uses the default training
        /// parameters for the model.
        /// </summary>
        /// <param name="data">Training data</param>
        public void Train(InsightMatrix data)
        {
            var results = PerformLinearRegression(data, Alpha, Lambda, Iterations);
            Theta = results.Item1;
            Error = results.Item2;
        }

        /// <summary>
        /// Trains the model using the supplied data.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="alpha">The learning rate for the algorithm</param>
        /// <param name="lambda">The regularization weight for the algorithm</param>
        /// <param name="iters">The number of training iterations to run</param>
        public void Train(InsightMatrix data, double? alpha, double? lambda, int? iters)
        {
            if (alpha != null) Alpha = alpha.Value;
            if (lambda != null) Lambda = lambda.Value;
            if (iters != null) Iterations = iters.Value;

            Train(data);
        }

        /// <summary>
        /// Predicts the target for a new instance of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instance">New instance</param>
        /// <returns>Prediction</returns>
        public double Predict(InsightVector instance)
        {
            return Predict(instance.ToRowMatrix())[0];
        }

        /// <summary>
        /// Predicts the target for a new batch of instances of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instances">New instances</param>
        /// <returns>Predictions</returns>
        public List<double> Predict(InsightMatrix instances)
        {
            instances = instances.InsertColumn(0, 1);
            return (instances * Theta.ToColumnMatrix()).Column(0).ToList();
        }

        /// <summary>
        /// Performs linear regression on the input data.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="alpha">The learning rate for the algorithm</param>
        /// <param name="lambda">The regularization weight for the algorithm</param>
        /// <param name="iters">The number of training iterations to run</param>
        /// <returns>Tuple containing the parameter and error vectors</returns>
        private Tuple<InsightVector, InsightVector> PerformLinearRegression(InsightMatrix data, double alpha,
            double lambda, int iters)
        {
            // First add a ones column for the intercept term
            data = data.InsertColumn(0, 1);

            // Split the data into training data and the target variable
            var X = data.RemoveColumn(data.ColumnCount - 1);
            var y = data.Column(data.ColumnCount - 1);

            // Initialize several variables needed for the computation
            var theta = new InsightVector(X.ColumnCount);
            var temp = new InsightVector(X.ColumnCount);
            var error = new InsightVector(iters);

            // Perform gradient descent on the parameters theta
            for (int i = 0; i < iters; i++)
            {
                var delta = (X * theta.ToColumnMatrix()) - y.ToColumnMatrix();

                for (int j = 0; j < theta.Count; j++)
                {
                    var inner = delta.Multiply(X.SubMatrix(0, X.RowCount, j, 1));

                    if (j == 0)
                    {
                        temp[j] = theta[j] - ((alpha / X.RowCount) * inner.Column(0).Sum());
                    }
                    else
                    {
                        var reg = (2 * lambda) * theta[i];
                        temp[j] = theta[j] - ((alpha / X.RowCount) * inner.Column(0).Sum()) + reg;
                    }
                }

                theta = temp.Clone();
                error[i] = ComputeError(X, y, theta, lambda);
            }

            return new Tuple<InsightVector, InsightVector>(theta, error);
        }

        /// <summary>
        /// Computes the total error of the solution with parameters theta.
        /// </summary>
        /// <param name="X">Training data</param>
        /// <param name="y">Target variable</param>
        /// <param name="theta">Model parameters</param>
        /// <param name="lambda">Regularization weight</param
        /// <returns>Solution error</returns>
        private double ComputeError(InsightMatrix X, InsightVector y, InsightVector theta, double lambda)
        {
            var inner = ((X * theta.ToColumnMatrix()) - y.ToColumnMatrix()).Power(2);
            var thetaSub = theta.SubVector(1, theta.Count - 1);
            var reg = lambda * thetaSub.Multiply(thetaSub).Sum();
            return (inner.Column(0).Sum() / (2 * X.RowCount)) + reg;
        }
    }
}
