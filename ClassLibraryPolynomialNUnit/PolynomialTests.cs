using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLogicPolynomial;
using NUnit.Framework;
using System.Collections;

namespace ClassLibraryPolynomialNUnit
{
    [TestFixture]
    public class PolynomialTests
    {
        #region TestCases

        private static IEnumerable TestCasePolynomialToString
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[] { 1, 3, 4, 7 }, new[] { 0.5, 1.7, 2.8, 4.7 })).Returns("Polynomial degree -> 7, degrees -> 1, 3, 4, 7, coefficients -> 0,5, 1,7, 2,8, 4,7");
            }
        }

        private static IEnumerable TestCaseIndexer
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[] { 1, 3, 4, 7 }, new[] { 0.5, 1.7, 2.8, 4.7 })).Returns(2.8);
                yield return new TestCaseData(new Polynomial(new[] { 1, 3 }, new[] { 0.5, 1.7 })).Throws(typeof(ArgumentOutOfRangeException));
            }
        }

        private static IEnumerable TestCasePolynomialSum
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[] { 1, 2, 3, 6 }, new[] { 0.5, 1.4, 8, 4 }), 
                    new Polynomial(new[] { 4, 7, 1, 2 }, new[] { 0.3, 0.7, 1, 3 })).Returns("Polynomial degree -> 7, degrees -> 1, 2, 3, 4, 6, 7, coefficients -> 1,5, 4,4, 8, 0,3, 4, 0,7");                

            }
        }

        private static IEnumerable TestCasePolynomialMinus
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[] { 1, 2, 3, 6 }, new[] { 0.5, 1.4, 8, 4 }),
                    new Polynomial(new[] { 4, 7, 1, 2 }, new[] { 0.3, 0.7, 1, 3 }));
            }
        }

        private static IEnumerable TestCasePolynomialMultiply
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[] { 2, 3, 4 }, new[] { 3, 5, 0.5 }),
                    new Polynomial(new[] { 5, 7 }, new[] { 5, 1.5 }));
            }
        }

        #endregion

        [Test, TestCaseSource("TestCasePolynomialToString")]
        public string PolynomialTostringTest_PolynomialToString_ReturnToString(Polynomial pFirst)
        {
            string actual = pFirst.ToString();
            return actual;
        }

        [Test, TestCaseSource("TestCaseIndexer")]
        public double PolynomialIndexerTest_PolynomialIndexer_ReturnCoefficient(Polynomial pFirst)
        {
            double coefficient = pFirst[2];
            
            return coefficient;
        }

        [Test, TestCaseSource("TestCasePolynomialSum")]
        public string PolynomialSumTest_PolynomialSum_ReturnSum(Polynomial lhs, Polynomial rhs)
        {
            Polynomial expected = new Polynomial(new[] { 1, 2, 3, 4, 6, 7 }, new[] { 1.5, 4.4, 8, 0.3, 4, 0.7});
            Polynomial actual = lhs + rhs;

            return expected.ToString();
        }

        [Test, TestCaseSource("TestCasePolynomialMinus")]
        public void PolynomialMinusTest_PolynomialMinus_ReturnMinus(Polynomial lhs, Polynomial rhs)
        {
            Polynomial expected = new Polynomial(new[] { 1, 2, 3, 4, 6, 7 }, new[] {-0.5, -1.6, 8, -0.3, 4, -0.7 });

            Polynomial actual = lhs - rhs;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }

        [Test, TestCaseSource("TestCasePolynomialMultiply")]
        public void PolynomialMultiplyTest_PolynomialMultiplyOnPolynomial_ReturnMultiply(Polynomial lhs, Polynomial rhs)
        {
            Polynomial expected = new Polynomial(new[] { 7, 8, 9, 10, 11 }, new[] { 15, 25, 7, 7.5, 0.75});

            Polynomial actual = lhs * rhs;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }
    }
}
