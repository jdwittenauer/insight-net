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

namespace Insight.AI.DataStructures
{
    /// <summary>
    /// This class encapsulates the matrix factorization data structure used by Insight.NET.
    /// </summary>
    public class MatrixFactorization
    {
        /// <summary>
        /// Indicates the type of factorization that was performed.
        /// </summary>
        public string FactorizationType { get; private set; }

        /// <summary>
        /// Gets the effective numerical matrix rank.
        /// </summary>
        public int Rank { get; private set; }

        /// <summary>
        /// Gets the determinant of the matrix.
        /// </summary>
        public double Determinant { get; private set; }

        /// <summary>
        /// Gets the L2 norm of the matrix.
        /// </summary>
        public double L2Norm { get; private set; }

        /// <summary>
        /// Gets the eigenvalues of the matrix in ascending order.
        /// </summary>
        public InsightVector Eigenvalues { get; private set; }

        /// <summary>
        /// Gets the eigenvectors of the matrix.
        /// </summary>
        public InsightMatrix Eigenvectors { get; private set; }

        /// <summary>
        /// Gets the block diagonal eigenvalue matrix.
        /// </summary>
        public InsightMatrix EigenvaluesDiagonal { get; private set; }

        /// <summary>
        /// Gets the singluar values of the matrix in ascending order.
        /// </summary>
        public InsightVector SingularValues { get; private set; }

        /// <summary>
        /// Gets the left singular vectors of the matrix.
        /// </summary>
        public InsightMatrix LeftSingularVectors { get; private set; }

        /// <summary>
        /// Gets the transpose right singular vectors of the matrix.
        /// </summary>
        public InsightMatrix RightSingularVectors { get; private set; }

        /// <summary>
        /// Returns the singular values as a diagonal matrix.
        /// </summary>
        public InsightMatrix SingularValuesDiagonal { get; private set; }

        /// <summary>
        /// Matrix factorization constructor for eigenvalue decomposition.
        /// </summary>
        /// <param name="factorizationType">Factorization type</param>
        /// <param name="rank">Rank</param>
        /// <param name="determinant">Determinant</param>
        /// <param name="eigenvalues">Eigenvalues</param>
        /// <param name="eigenvectors">Eigenvectors</param>
        /// <param name="D">Diagonal eigenvalues matrix</param>
        public MatrixFactorization(string factorizationType, int rank, double determinant, 
            InsightVector eigenvalues, InsightMatrix eigenvectors, InsightMatrix D)
        {
            this.FactorizationType = factorizationType;
            this.Rank = rank;
            this.Determinant = determinant;
            this.Eigenvalues = eigenvalues;
            this.Eigenvectors = eigenvectors;
            this.EigenvaluesDiagonal = D;
        }

        /// <summary>
        /// Matrix factorization constructor for singular value decomposition.
        /// </summary>
        /// <param name="factorizationType">Factorization type</param>
        /// <param name="rank">Rank</param>
        /// <param name="l2Norm">L2 norm</param>
        /// <param name="S">Singular values</param>
        /// <param name="U">Left singular vectors</param>
        /// <param name="VT">Right singular vectors</param>
        /// <param name="W">Diagonal singular values matrix</param>
        public MatrixFactorization(string factorizationType, int rank, double l2Norm, 
            InsightVector S, InsightMatrix U, InsightMatrix VT, InsightMatrix W)
        {
            this.FactorizationType = factorizationType;
            this.Rank = rank;
            this.L2Norm = l2Norm;
            this.SingularValues = S;
            this.LeftSingularVectors = U;
            this.RightSingularVectors = VT;
            this.SingularValuesDiagonal = W;
        }
    }
}
