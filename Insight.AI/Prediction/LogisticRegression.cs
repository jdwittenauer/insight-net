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
    /// Class that encapsulates the logistic regression algorithm.
    /// </summary>
    /// <remarks>
    /// Logistic regression is a type of probablistic statistical classification model.
    /// The technique is similar to linear regression except that the loss function being
    /// minimized is based on the sigmoid (logit) of the hypothesis.  This has the effect
    /// of optimizing for the probability that an instance of the data belongs to one of
    /// the possible classes.
    /// 
    /// Note that the current implementation can only handle binary classification.
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Logistic_regression"/>
    public sealed class LogisticRegression : IClassifier
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
        public LogisticRegression() 
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
            var results = PerformLogisticRegression(data, Alpha, Lambda, Iterations);
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
        /// Classifies a new instance of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instance">New instance</param>
        /// <returns>Classification</returns>
        public int Classify(InsightVector instance)
        {
            return Classify(instance.ToRowMatrix())[0];
        }

        /// <summary>
        /// Classifies a new batch of instances of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instances">New instances</param>
        /// <returns>Classifications</returns>
        public List<int> Classify(InsightMatrix instances)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs logistic regression on the input data.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="alpha">The learning rate for the algorithm</param>
        /// <param name="lambda">The regularization weight for the algorithm</param>
        /// <param name="iters">The number of training iterations to run</param>
        /// <returns>Tuple containing the parameter and error vectors</returns>
        private Tuple<InsightVector, InsightVector> PerformLogisticRegression(InsightMatrix data, double alpha,
            double lambda, int iters)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the sigmoid (logit) of the original value.
        /// </summary>
        /// <param name="value">Original valud</param>
        /// <returns>Sigmoid value</returns>
        private double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
    }
}
