using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogicPolynomial
{
    public class Polynomial
    {
        #region Fields

        private readonly int degree;
        private readonly int[] coefficients;
        
        #endregion

        #region Properties

        public int Degree
        {
            get { return degree; }
        }

        public int[] Coefficients
        {
            get { return coefficients; }
        }
        
        #endregion

        #region Ctor

        public Polynomial(int degree)
        {
            this.degree = degree;
            coefficients = new int[degree + 1];

            for (int i = 0; i <= degree; i++)
            {
                coefficients[i] = 0;
            }
        }

        public Polynomial(int[] coefficients)
        {
            degree = coefficients.Length - 1;
            this.coefficients = new int[degree+1];

            for (int i = 0; i <= degree; i++)
            {
                this.coefficients[i] = coefficients[i];
            }
        }

        #endregion

        #region Overloading operators such as "+ | - | * | == | != "

        /// <summary>
        /// Overloading operator "+" to add two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>The sum of two polynomials</returns>
        public static Polynomial operator +(Polynomial fPolynom, Polynomial sPolynom)
        {
            if (fPolynom.degree != sPolynom.degree)
                throw new ArgumentException();

            Polynomial sumPolynomial = new Polynomial(fPolynom.degree);

            for (int i = 0; i <= sumPolynomial.degree; i++)
            {
                sumPolynomial.coefficients[i] = fPolynom.coefficients[i] + sPolynom.coefficients[i];
            }

            return sumPolynomial;
        }

        /// <summary>
        /// Overloading operator "-" to subtraction two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>The subtraction of two polynomials</returns>
        public static Polynomial operator -(Polynomial fPolynom, Polynomial sPolynom)
        {
            if (fPolynom.degree != sPolynom.degree)
                throw new ArgumentException();

            Polynomial resPolynomial = new Polynomial(fPolynom.degree);

            for (int i = 0; i <= resPolynomial.degree; i++)
            {
                resPolynomial.coefficients[i] = fPolynom.coefficients[i] - sPolynom.coefficients[i];
            }

            return resPolynomial;
        }

        /// <summary>
        /// Overloading operator "*" to multiply polynomial on number
        /// </summary>
        /// <returns>Multiply polynomial on number</returns>
        public static Polynomial operator *(Polynomial fPolynom, int number)
        {
            Polynomial resPolynomial = new Polynomial(fPolynom.degree);

            for (int i = 0; i <= resPolynomial.degree; i++)
                resPolynomial.coefficients[i] = fPolynom.coefficients[i] * number;

            return resPolynomial;
        }

        /// <summary>
        /// Overloading operator "*" to multiply two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>Multiply of two polynomial</returns>
        public static Polynomial operator *(Polynomial fPolynom, Polynomial sPolynom)
        {
            if (fPolynom.degree != sPolynom.degree)
                throw new ArgumentException();

            Polynomial resPolynomial = new Polynomial(fPolynom.degree * 2);

            int[] fCoef = new int[fPolynom.degree * 2 + 1];
            int[] sCoef = new int[sPolynom.degree * 2 + 1];
            fPolynom.coefficients.CopyTo(fCoef, 0);
            sPolynom.coefficients.CopyTo(sCoef, 0);

            for (int i = 0; i <= resPolynomial.degree; i++)
                for (int j = 0; j <= i; j++)
                {
                    resPolynomial.coefficients[i] += fCoef[j] * sCoef[i - j];
                }

            return resPolynomial;
        }

        /// <summary>
        /// Overloading operator "==" to subtraction two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>True/false if polynomials are equal/not equal</returns>
        public static bool operator ==(Polynomial fPolynom, Polynomial sPolynom)
        {
            if (fPolynom.degree != sPolynom.degree)
                throw new ArgumentException();
            
            for (int i = 0; i < fPolynom.degree + 1; i++)
            {
                if (fPolynom.coefficients[i] != sPolynom.coefficients[i])
                    return false;
            }
            return fPolynom.degree == sPolynom.degree;
        }

        /// <summary>
        /// Overloading operator "!=" to subtraction two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>True/false if polynomials are equal/not equal</returns>
        public static bool operator !=(Polynomial fPolynom, Polynomial sPolynom)
        {
            return !(fPolynom == sPolynom);
        }
        #endregion

        #region Override Object virtual method Equals | GetHashCode | ToString

        /// <summary>
        /// Override method Equals
        /// </summary>
        /// <returns>True/false if polynomials are equal/not equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType()!= obj.GetType())
                return false;

            Polynomial polynomial = (Polynomial)obj;
            
            return this == (Polynomial)obj;
        }

        /// <summary>
        /// Override method GetHashCode()
        /// </summary>
        public override int GetHashCode()
        {
            return degree.GetHashCode()^coefficients.GetHashCode();
        }

        /// <summary>
        /// Override method ToString()
        /// </summary>
        public override string ToString()
        {
            string coef = String.Empty;

            for (int i = 0; i < coefficients.Length; i++)
            {
                if (i != coefficients.Length - 1)
                    coef += coefficients[i] + ", ";
                else
                    coef += coefficients[i];
            }

            return String.Format("Polynomial degree -> {0}, coefficients -> {1}", degree, coef);
        }

        #endregion
    }
}
