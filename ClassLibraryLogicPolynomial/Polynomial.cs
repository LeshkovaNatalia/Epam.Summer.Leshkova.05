using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogicPolynomial
{
    public sealed class Polynomial
    {
        #region Fields
        private double[] _coefficients = { };
        private int[] _degrees = { };
        private Dictionary<int, double> _polynomialDictionary;        
        #endregion

        #region Properties

        public static double epsilon;

        public int Degree
        {
            get
            {
                if (_degrees.Length == 0)
                    throw new ArgumentException();           
                return _degrees.Last();
            }

        }

        public double[] Coefficients
        {
            get { return _coefficients; }

            private set { _coefficients = new double[value.Length]; }
        }

        public int[] Degrees
        {
            get { return _degrees; }

            private set { _degrees = new int[value.Length]; }
        }

        #endregion

        #region Ctors

        static Polynomial()
        {
            try
            {
                epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"], CultureInfo.InvariantCulture);
            }
            catch(ConfigurationErrorsException)
            {
                throw new ArgumentNullException(nameof(epsilon));
            }
        }

        public Polynomial()
        {
            _polynomialDictionary = new Dictionary<int, double>();
        }

        public Polynomial(int[] degrees, double[] coefficients)
        {
            if (_degrees.Length != _coefficients.Length)
                throw new ArgumentException(nameof(coefficients));

            InitializationDictionary(degrees, coefficients);
            InitializationDegreesCoeffitiants();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indexers for polynomial.
        /// </summary>
        public double this[int number]
        {
            get
            {
                if (number > Degree)
                    throw new ArgumentOutOfRangeException();

                return _coefficients[number];
            }
            private set
            {
                if (number >= 0 || number < _coefficients.Length)
                {
                    this.Coefficients[number] = value;
                }
                throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        #region Overloading operators such as "+ | - | * | == | != "

        /// <summary>
        /// Overloading operator "+" to add two polynomials.
        /// </summary>
        /// <param name="lhs">The first polynomial.</param>
        /// <param name="rhs">The second polynomial.</param>
        /// <returns>The sum of two polynomials.</returns>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            Polynomial sumPolynomial = new Polynomial();

            if (rhs.Degrees.Length >= rhs.Degrees.Length)
                GetDegreesCoefficientsForSum(sumPolynomial, lhs, rhs);
            else
                GetDegreesCoefficientsForSum(sumPolynomial, rhs, lhs);                           

            return GetResultPolynomial(sumPolynomial);
        }

        /// <summary>
        /// Overloading operator "-" to subtraction two polynomials.
        /// </summary>
        /// <param name="lhs">The first polynomial.</param>
        /// <param name="rhs">The second polynomial.</param>
        /// <returns>The subtraction of two polynomials.</returns>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            for (int i = 0; i < rhs._polynomialDictionary.Count; i++)
                rhs.Coefficients[i] = (-1) * rhs.Coefficients[i];

            return lhs + rhs;
        }

        /// <summary>
        /// Overloading operator "*" to multiply polynomial on number
        /// </summary>
        /// <returns>Multiply polynomial on number</returns>
        public static Polynomial operator *(Polynomial lhs, int number)
        {
            Polynomial resultPolynomial = new Polynomial();

            for (int i = 0; i <= lhs.Degrees.Length; i++)
                resultPolynomial._polynomialDictionary.Add(lhs.Degrees[i], lhs.Coefficients[i] * number);

            return GetResultPolynomial(resultPolynomial);
        }        

        /// <summary>
        /// Overloading operator "*" to multiply two polynomials
        /// </summary>
        /// <param name="lhs">The first polynomial</param>
        /// <param name="rhs">The second polynomial</param>
        /// <returns>Multiply of two polynomial</returns>
        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            Polynomial myltiplyPolynomial = new Polynomial();

            for(int i = 0; i < lhs.Degrees.Length; i++)
                for(int j = 0; j < rhs.Degrees.Length; j++)
                    if(myltiplyPolynomial._polynomialDictionary.ContainsKey(lhs.Degrees[i] + rhs.Degrees[j]))
                        myltiplyPolynomial._polynomialDictionary[lhs.Degrees[i] + rhs.Degrees[j]] += lhs.Coefficients[i]*rhs.Coefficients[j];
                    else
                        myltiplyPolynomial._polynomialDictionary.Add(lhs.Degrees[i] + rhs.Degrees[j], lhs.Coefficients[i]*rhs.Coefficients[j]);
                      
            return GetResultPolynomial(myltiplyPolynomial);
        }

        /// <summary>
        /// Overloading operator "==" to subtraction two polynomials
        /// </summary>
        /// <param name="fPolynom">The first polynomial</param>
        /// <param name="sPolynom">The second polynomial</param>
        /// <returns>True/false if polynomials are equal/not equal</returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;

            return lhs.Equals(rhs);
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
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Polynomial) obj);
        }

        /// <summary>
        /// Override method GetHashCode()
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Coefficients != null ? Coefficients.GetHashCode() : 0)*397)^ Degree;
            }            
        }

        /// <summary>
        /// Override method ToString()
        /// </summary>
        public override string ToString()
        {
            string coefficients = String.Empty;
            string degrees = String.Empty;

            coefficients = GetStringCoefficients(coefficients);
            degrees = GetStringDegrees(degrees);
           
            return String.Format("Polynomial degree -> {0}, degrees -> {1}, coefficients -> {2}", this.Degree, degrees, coefficients);
        }

        #endregion

        /// <summary>
        /// Instance Method Equals return true if 2 polynomials are equal. 
        /// </summary>
        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return CheckPolynomialEquals(other);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method InitializationDictionary fills dictionary with degrees and coefficients.
        /// </summary>
        /// <param name="degrees">Array of degrees of polynomial.</param>
        /// <param name="coefficients">Array of coefficients of polynomial.</param>
        private void InitializationDictionary(int[] degrees, double[] coefficients)
        {
            _polynomialDictionary = new Dictionary<int, double>();

            for (int i = 0; i < degrees.Length; i++)
                _polynomialDictionary.Add(degrees[i], coefficients[i]);
        }

        /// <summary>
        /// Method InitializationDegreeCoeffitiants fills private fields _degrees and _coefficients.
        /// </summary>
        private void InitializationDegreesCoeffitiants()
        {
            var list = _polynomialDictionary.Keys.ToList();
            list.Sort();

            _degrees = new int[list.Count];
            _coefficients = new double[list.Count];

            int j = 0;
            foreach (var key in list)
            {
                _degrees[j] = key;

                if (Math.Abs(_polynomialDictionary[key]) < epsilon)
                    throw new ArgumentException(nameof(epsilon));
                _coefficients[j] = _polynomialDictionary[key];

                j++;
            }
        }

        /// <summary>
        /// Method CheckPolynomialEquals equals degrees and coefficicents of 2 polynomials. 
        /// </summary>
        /// <param name="other">Polynomial.</param>
        /// <returns>True if data are equal and false in other case.</returns>
        private bool CheckPolynomialEquals(Polynomial other)
        {
            bool isEqualsCoefficients = CheckEqualsCoefficients(other);
            bool isEqualsDegrees = CheckEqualsDegrees(other);    
            
            if(isEqualsCoefficients && isEqualsDegrees)
                return true;

            return false;
        }

        /// <summary>
        /// Method CheckEqualsCoefficients equals coefficients of 2 polynomials. 
        /// </summary>
        /// <param name="other">Polynomial.</param>
        /// <returns>True if coefficients are equal and false in other case.</returns>
        private bool CheckEqualsCoefficients(Polynomial other)
        {
            if (this.Coefficients.Length != other.Coefficients.Length)
                return false;

            for (int i = 0; i < this.Coefficients.Length; i++)
                if (!this.Coefficients[i].Equals(other.Coefficients[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Method CheckEqualsDegrees equals degrees of 2 polynomials. 
        /// </summary>
        /// <param name="other">Polynomial.</param>
        /// <returns>True if degrees are equal and false in other case.</returns>
        private bool CheckEqualsDegrees(Polynomial other)
        {
            if (this.Degrees.Length != other.Degrees.Length)
                return false;

            for (int i = 0; i < this.Degrees.Length; i++)
                if (!this.Degrees[i].Equals(other[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Method GetDictionaryForSum fills dictionary with <key, value> such <degree, coefficient>
        /// </summary>
        /// <param name="polynomial">Polynomial with result of sum 2 polynomials.</param>
        /// <param name="lhs">First addendum</param>
        /// <param name="rhs">Second addendum</param>
        private static void GetDegreesCoefficientsForSum(Polynomial polynomial, Polynomial lhs, Polynomial rhs)
        {
            for (int i = 0; i < lhs.Degrees.Length; i++)
            {
                if (i < rhs.Degrees.Length)
                {
                    if (lhs.Degrees[i] == rhs.Degrees[i])
                        polynomial._polynomialDictionary.Add(lhs.Degrees[i], lhs.Coefficients[i] + rhs.Coefficients[i]);
                    else
                    {
                        polynomial._polynomialDictionary.Add(lhs.Degrees[i], lhs.Coefficients[i]);
                        polynomial._polynomialDictionary.Add(rhs.Degrees[i], rhs.Coefficients[i]);
                    }
                }
                else
                    polynomial._polynomialDictionary.Add(lhs.Degrees[i], lhs.Coefficients[i]);
            }
        }

        /// <summary>
        /// Method GetSumPolynom return result of sum two polynomials.
        /// </summary>
        /// <param name="polynomial">Summary polynomial.</param>
        /// <returns>Result of sum two polynomials.</returns>
        private static Polynomial GetResultPolynomial(Polynomial polynomial)
        {
            var list = polynomial._polynomialDictionary.Keys.ToList();
            list.Sort();
            polynomial.Degrees = new int[polynomial._polynomialDictionary.Count];
            polynomial.Coefficients = new double[polynomial._polynomialDictionary.Count];
            
            return GetDegreesCoefficients(polynomial, list);
        }

        /// <summary>
        /// Method GetDegreesCoefficients fills arrays of degrees and coefficients from dictionary.
        /// </summary>
        /// <param name="polynomial">Summary polynomial.</param>
        /// <param name="list">List with keys.</param>
        private static Polynomial GetDegreesCoefficients(Polynomial polynomial, List<int> list)
        {
            int j = 0;
            foreach (var key in list)
            {
                polynomial.Degrees[j] = key;
                polynomial.Coefficients[j] = polynomial._polynomialDictionary[key];
                j++;
            }

            return polynomial;
        }

        /// <summary>
        /// Method GetStringDegrees return string with degrees of polynomial.
        /// </summary>
        private string GetStringDegrees(string degr)
        {
            for (int i = 0; i < Degrees.Length; i++)
            {
                if (i != Degrees.Length - 1)
                    degr += Degrees[i] + ", ";
                else
                    degr += Degrees[i];
            }

            return degr;
        }

        // <summary>
        /// Method GetStringCoefficients return string with degrees of polynomial.
        /// </summary>
        private string GetStringCoefficients(string coef)
        {
            for (int i = 0; i < Coefficients.Length; i++)
            {
                if (i != Coefficients.Length - 1)
                    coef += Coefficients[i] + ", ";
                else
                    coef += Coefficients[i];
            }

            return coef;
        }

        #endregion
    }
}
