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
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;

namespace Insight.AI.DataStructures
{
    /// <summary>
    /// Will eventually become the primary data structure once I get the design worked out.
    /// </summary>
    public class DataFrame
    {
        /// <summary>
        /// Collection indicating the name of each attribute in the data set.
        /// </summary>
        private List<string> Names;

        /// <summary>
        /// Collection indicating the role for each attribute in the data set
        /// (id, regular, label, prediction, cluster, weight).
        /// </summary>
        private List<string> Roles;

        /// <summary>
        /// Collection indicating the relative weight of each attribute in the data set.
        /// </summary>
        private List<double> Weights;

        /// <summary>
        /// Collection indicating if the equivalent position in the data set is a misssing value.
        /// </summary>
        private List<List<bool>> MissingValues;

        /// <summary>
        /// Unlabeled data set.
        /// </summary>
        public InsightMatrix Data { get; private set; }

        /// <summary>
        /// Target prediction variable.
        /// </summary>
        public InsightVector Target { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataFrame () { }
    }
}
