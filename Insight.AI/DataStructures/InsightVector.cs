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
using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;

namespace Insight.AI.DataStructures
{
    /// <summary>
    /// This class encapsulates the vector data structure used by Insight.NET.
    /// </summary>
    public class InsightVector : IEnumerable<double>
    {
        /// <summary>
        /// Returns the number of items in the vector.
        /// </summary>
        public int Count { get { return Data.Count; } }

        /// <summary>
        /// Math.NET vector implementation.  Used for all vector calculations.
        /// </summary>
        public Vector Data { get; private set; }

        /// <summary>
        /// Create a new vector with the given length.  Cells will be initalized to zero.
        /// </summary>
        /// <param name="length">Vector length</param>
        public InsightVector(int length)
        {
            Data = new DenseVector(length);
        }

        /// <summary>
        /// Create a new vector with the given length.  Cells will be initalized
        /// to the provided initial value.
        /// </summary>
        /// <param name="length">Vector length</param>
        /// <param name="value">Initial value</param>
        public InsightVector(int length, int value)
        {
            Data = new DenseVector(length);

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i] = value;
            }
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
        /// Returns the sum of the vector's values.
        /// </summary>
        /// <returns>Sum</returns>
        public double Sum()
        {
            return this.Data.Sum();
        }

        /// <summary>
        /// Returns the mean of the vector's values.
        /// </summary>
        /// <returns>Mean</returns>
        public double Mean()
        {
            return this.Data.Mean();
        }

        /// <summary>
        /// Returns the standard deviation of the vector's values.
        /// </summary>
        /// <returns>Standard deviation</returns>
        public double Std()
        {
            return this.Data.StandardDeviation();
        }

        /// <summary>
        /// Return the minimum value in the vector.
        /// </summary>
        /// <returns>Minimum value</returns>
        public double Min()
        {
            return this.Data.Minimum();
        }

        /// <summary>
        /// Return the maximum value in the vector.
        /// </summary>
        /// <returns>Maximum value</returns>
        public double Max()
        {
            return this.Data.Maximum();
        }

        /// <summary>
        /// Returns the index of the minimum value in the vector.
        /// </summary>
        /// <returns>Index of minimum value</returns>
        public int MinIndex()
        {
            return this.Data.MinimumIndex();
        }

        /// <summary>
        /// Returns the index of the maximum value in the vector.
        /// </summary>
        /// <returns>index of maximum value</returns>
        public int MaxIndex()
        {
            return this.Data.MaximumIndex();
        }

        /// <summary>
        /// Returns a deep copy clone of the vector.
        /// </summary>
        /// <returns></returns>
        public InsightVector Clone()
        {
            return new InsightVector(this.Data.Clone());
        }

        /// <summary>
        /// Element-wise multiplies two vectors together.
        /// </summary>
        /// <param name="vector">2nd vector</param>
        /// <returns>Result vector</returns>
        public InsightVector Multiply(InsightVector vector)
        {
            return new InsightVector(this.Data.PointwiseMultiply(vector.Data));
        }

        /// <summary>
        /// Element-wise divides the original vector by the provided vector.
        /// </summary>
        /// <param name="vector">2nd vector</param>
        /// <returns>Result vector</returns>
        public InsightVector Divide(InsightVector vector)
        {
            return new InsightVector(this.Data.PointwiseDivide(vector.Data));
        }

        /// <summary>
        /// Element-wise raises the vector to the given exponent.
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>Result vector</returns>
        public InsightVector Power(int exponent)
        {
            return new InsightVector(this.Data.PointwisePower(exponent));
        }

        /// <summary>
        /// Element-wise computes the natural logarithm.
        /// </summary>
        /// <returns>Result vector</returns>
        public InsightVector Log()
        {
            return new InsightVector(this.Data.PointwiseLog());
        }

        /// <summary>
        /// Returns a sub-vector of the original using the provided start index and count.
        /// </summary>
        /// <param name="index">Start index</param>
        /// <param name="count">Number of elements</param>
        /// <returns>Result vector</returns>
        public InsightVector SubVector(int index, int count)
        {
            return new InsightVector(this.Data.SubVector(index, count));
        }

        /// <summary>
        /// Converts the vector to a column matrix.
        /// </summary>
        /// <returns>Column matrix</returns>
        public InsightMatrix ToColumnMatrix()
        {
            return new InsightMatrix(this.Data.ToColumnMatrix());
        }

        /// <summary>
        /// Converts the vector to a row matrix.
        /// </summary>
        /// <returns>Row matrix</returns>
        public InsightMatrix ToRowMatrix()
        {
            return new InsightMatrix(this.Data.ToRowMatrix());
        }

        /// <summary>
        /// Centers the vector by subtracting each value by the mean.
        /// </summary>
        /// <returns>Centered vector</returns>
        public InsightVector Center()
        {
            int length = this.Count;
            double mean = this.Mean();

            for (int i = 0; i < length; i++)
            {
                this[i] = this[i] - mean;
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
            int length = this.Count;

            for (int i = 0; i < length; i++)
            {
                this[i] = this[i] - mean;
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
            int length = this.Data.Count;
            double ss = 0;

            for (int i = 0; i < length; i++)
            {
                ss += this[i] * this[i];
            }

            double ss2 = Math.Sqrt(ss);

            for (int i = 0; i < length; i++)
            {
                this[i] = this[i] / ss2;
            }

            return this;
        }

        /// <summary>
        /// Normalizes the vector by centering and then scaling the original vector.
        /// </summary>
        /// <returns>Normalized vector</returns>
        public InsightVector Normalize()
        {
            return this.Center().Scale();
        }

        /// <summary>
        /// Normalizes the vector by centering and then scaling the original vector.
        /// </summary>
        /// <param name="mean">Pre-computed mean for this vector</param>
        /// <returns>Normalized vector</returns>
        public InsightVector Normalize(double mean)
        {
            return this.Center(mean).Scale();
        }

        /// <summary>
        /// Add a scalar value and a vector.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="scalar">Scalar number</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator +(InsightVector v, double scalar)
        {
            return new InsightVector(v.Data + scalar);
        }

        /// <summary>
        /// Subtract a scalar value from a vector.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="scalar">Scalar number</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator -(InsightVector v, double scalar)
        {
            return new InsightVector(v.Data - scalar);
        }

        /// <summary>
        /// Multiply a vector by a scalar value.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="scalar">Scalar number</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator *(InsightVector v, double scalar)
        {
            return new InsightVector(v.Data * scalar);
        }

        /// <summary>
        /// Divide a vector by a scalar value.
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="scalar">Scalar number</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator /(InsightVector v, double scalar)
        {
            return new InsightVector(v.Data / scalar);
        }

        /// <summary>
        /// Add a vector to a scalar value.
        /// </summary>
        /// <param name="scalar">Scalar number</param>
        /// <param name="v">Vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator +(double scalar, InsightVector v)
        {
            return new InsightVector(scalar + v.Data);
        }

        /// <summary>
        /// Subtract a vector from a scalar value.
        /// </summary>
        /// <param name="scalar">Scalar number</param>
        /// <param name="v">Vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator -(double scalar, InsightVector v)
        {
            return new InsightVector(scalar - v.Data);
        }

        /// <summary>
        /// Multiply a scalar value by a vector.
        /// </summary>
        /// <param name="scalar">Scalar number</param>
        /// <param name="v">Vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator *(double scalar, InsightVector v)
        {
            return new InsightVector(scalar * v.Data);
        }

        /// <summary>
        /// Divide a scalar value by a vector.
        /// </summary>
        /// <param name="scalar">Scalar number</param>
        /// <param name="v">Vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator /(double scalar, InsightVector v)
        {
            return new InsightVector(scalar / v.Data);
        }

        /// <summary>
        /// Add two vectors together.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator +(InsightVector v1, InsightVector v2)
        {
            return new InsightVector(v1.Data + v2.Data);
        }

        /// <summary>
        /// Subtract a vector from another vector.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result vector</returns>
        public static InsightVector operator -(InsightVector v1, InsightVector v2)
        {
            return new InsightVector(v1.Data - v2.Data);
        }

        /// <summary>
        /// Compute the dot product of two vectors.
        /// </summary>
        /// <param name="v1">1st vector</param>
        /// <param name="v2">2nd vector</param>
        /// <returns>Result</returns>
        public static double operator *(InsightVector v1, InsightVector v2)
        {
            return v1.Data * v2.Data;
        }

        /// <summary>
        /// Returns the value at the given index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Value</returns>
        public double this[int index]
        {
            get { return this.Data[index]; }
            set { this.Data[index] = value; }
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
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Collection enumerator</returns>
        public IEnumerator<double> GetEnumerator()
        {
            return this.Data.Enumerate().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Collection enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
