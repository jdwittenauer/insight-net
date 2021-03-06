﻿// Copyright (c) 2013 John Wittenauer (Insight.NET)

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

using System.Collections.Generic;
using Insight.AI.DataStructures;

namespace Insight.AI.Prediction.Interfaces
{
    /// <summary>
    /// Interface for a regression model.
    /// </summary>
    public interface IRegression
    {
        /// <summary>
        /// Trains the model using the supplied data.  Uses the default training
        /// parameters for the model.
        /// </summary>
        /// <param name="data">Training data</param>
        void Train(InsightMatrix data);

        /// <summary>
        /// Predicts the target for a new instance of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instance">New instance</param>
        /// <returns>Prediction</returns>
        double Predict(InsightVector instance);

        /// <summary>
        /// Predicts the target for a new batch of instances of the data using the algorithm's trained model.
        /// </summary>
        /// <param name="instances">New instances</param>
        /// <returns>Predictions</returns>
        List<double> Predict(InsightMatrix instances);
    }
}
