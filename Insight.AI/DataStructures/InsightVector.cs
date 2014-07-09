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
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;

namespace Insight.AI.DataStructures
{
    /// <summary>
    /// This class encapsulates the vector data structure used by Insight.NET.
    /// </summary>
    public class InsightVector
    {
        /// <summary>
        /// Math.NET vector implementation.  Used for all vector calculations.
        /// </summary>
        public Vector Data { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InsightVector() { }

        /// <summary>
        /// Create a new vector with the given length.  Cells will be initalized to zero.
        /// </summary>
        /// <param name="length">Vector length</param>
        public InsightVector(int length)
        {
            Data = new DenseVector(length);
        }

        /// <summary>
        /// Create a new vector that directly binds to the raw array passed in.
        /// </summary>
        /// <param name="storage">Raw data array</param>
        public InsightVector(double[] storage)
        {
            Data = new DenseVector(storage);
        }

        /// <summary>
        /// Create a new vector as a copy of the given enumerable.
        /// </summary>
        /// <param name="other">Enumerable data</param>
        public InsightVector(IEnumerable<double> other)
        {
            Data = DenseVector.OfEnumerable(other);
        }

        /// <summary>
        /// Create a new vector as a copy of the given Math.NET vector.
        /// </summary>
        /// <param name="vector">Vector to copy</param>
        public InsightVector(Vector vector)
        {
            Data = vector;
        }

        /// <summary>
        /// Create a new vector as a copy of the given Math.NET vector.
        /// </summary>
        /// <param name="vector">Vector to copy</param>
        public InsightVector(Vector<double> vector)
        {
            Data = (DenseVector)vector;
        }

        /// <summary>
        /// Create a new vector as a copy of the given vector.
        /// </summary>
        /// <param name="vector">Vector to copy</param>
        public InsightVector(InsightVector vector)
        {
            Data = DenseVector.OfVector(vector.Data);
        }

        /// <summary>
        /// Returns a formatted string representation of the vector.
        /// </summary>
        /// <returns>String representation of the vector</returns>
        public override string ToString()
        {
            return this.Data.ToString("F4", null);
        }

        /// <summary>
        /// Centers the vector by subtracting each value by the mean.
        /// </summary>
        /// <returns>Centered vector</returns>
        public InsightVector Center()
        {
            if (this == null || this.Data == null)
                throw new Exception("Vector must be instantiated.");

            int length = this.Data.Count;
            double mean = this.Data.Mean();

            for (int i = 0; i < length; i++)
            {
                this.Data[i] = this.Data[i] - mean;
            }

            return this;
        }

        /// <summary>
        /// Centers the vector by subtracting each value by the mean.
        /// </summary>
        /// <param name="mean">Pre-computed mean for this vector</param>
        /// <returns>Centered vector</returns>
        public InsightVector Center(double mean)
        {
            if (this == null || this.Data == null)
                throw new Exception("Vector must be instantiated.");

            int length = this.Data.Count;

            for (int i = 0; i < length; i++)
            {
                this.Data[i] = this.Data[i] - mean;
            }

            return this;
        }

        /// <summary>
        /// Scales the vector by dividing each value by the square root of the squared sum.
        /// For a vector that is already centered, this is equivalent to the standard deviation.
        /// </summary>
        /// <returns>Scaled vector</returns>
        public InsightVector Scale()
        {
            if (this == null || this.Data == null)
                throw new Exception("Vector must be instantiated.");

            int length = this.Data.Count;
            double ss = 0;

            for (int i = 0; i < length; i++)
            {
                ss += this.Data[i] * this.Data[i];
            }

            double ss2 = Math.Sqrt(ss);

            for (int i = 0; i < length; i++)
            {
                this.Data[i] = this.Data[i] / ss2;
            }

            return this;
        }

        /// <summary>
        /// Normalizes the vector by centering and then scaling the original vector.
        /// </summary>
        /// <returns>Normalized vector</returns>
        public InsightVector Normalize()
        {
            if (this == null || this.Data == null)
                throw new Exception("Vector must be instantiated.");

            return this.Center().Scale();
        }

        /// <summary>
        /// Normalizes the vector by centering and then scaling the original vector.
        /// </summary>
        /// <param name="mean">Pre-computed mean for this vector</param>
        /// <returns>Normalized vector</returns>
        public InsightVector Normalize(double mean)
        {
            if (this == null || this.Data == null)
                throw new Exception("Vector must be instantiated.");

            return this.Center(mean).Scale();
        }

        /// <summary>
        /// Implement + operator for two vectors.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator +(InsightVector v1, InsightVector v2)
        {
            return new InsightVector(v1.Data + v2.Data);
        }

        /// <summary>
        /// Implement - operator for two vectors.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator -(InsightVector v1, InsightVector v2)
        {
            return new InsightVector(v1.Data - v2.Data);
        }

        /// <summary>
        /// Implement * operator for two vectors.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result vector</returns>
        public static double operator *(InsightVector v1, InsightVector v2)
        {
            return v1.Data * v2.Data;
        }
    }
}
