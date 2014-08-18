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
    public class LinearRegression : IRegression
    {
        /// <summary>
        /// The learning rate for the algorithm.
        /// </summary>
        public double Alpha { get; set; }

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
            Iterations = 1000;
        }

        /// <summary>
        /// Trains the model using the supplied data.  Uses the default training
        /// parameters for the model.
        /// </summary>
        /// <param name="data">Training data</param>
        public void Train(InsightMatrix data)
        {
            var results = PerformLinearRegression(data, Alpha, Iterations);
            Theta = results.Item1;
            Error = results.Item2;
        }

        /// <summary>
        /// Trains the model using the supplied data.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="alpha">The learning rate for the algorithm</param>
        /// <param name="iters">The number of training iterations to run</param>
        public void Train(InsightMatrix data, double? alpha, int? iters)
        {
            if (alpha != null) Alpha = alpha.Value;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs linear regression on the input data.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="alpha">The learning rate for the algorithm</param>
        /// <param name="iters">The number of training iterations to run</param>
        /// <returns>Tuple containing the parameter and error vectors</returns>
        private Tuple<InsightVector, InsightVector> PerformLinearRegression(InsightMatrix data, double alpha, int iters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the total error of the solution with parameters theta.
        /// </summary>
        /// <param name="data">Training data</param>
        /// <param name="theta">Model parameters</param>
        /// <returns>Solution error</returns>
        private double ComputeError(InsightMatrix data, InsightVector theta)
        {
            throw new NotImplementedException();
        }
    }
}
