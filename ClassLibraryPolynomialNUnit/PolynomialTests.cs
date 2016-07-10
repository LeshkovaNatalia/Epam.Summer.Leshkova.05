using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLogicPolynomial;
using NUnit.Framework;

namespace ClassLibraryPolynomialNUnit
{
    [TestFixture]
    public class PolynomialTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void PolynomialExceptionTest_PolynomialSumException_ReturnException()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3});
            Polynomial pSecond = new Polynomial(new[] { 8, 9, 4, 4 });

            Polynomial actual = pFirst + pSecond;
        }

        [Test]
        public void PolynomialSumTest_PolynomialSum_ReturnSum()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3, 6});
            Polynomial pSecond = new Polynomial(new[] { 8, 9, 4, 4 });

            Polynomial expected = new Polynomial(new []{9, 11, 7, 10});

            Polynomial actual = pFirst + pSecond;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }

        [Test]
        public void PolynomialMinusTest_PolynomialMinus_ReturnMinus()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3, 4 });
            Polynomial pSecond = new Polynomial(new[] { 8, 9, 4, 6 });

            Polynomial expected = new Polynomial(new[] { -7, -7, -1, -2 });

            Polynomial actual = pFirst - pSecond;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }

        [Test]
        public void PolynomialMultiplyTest_PolynomialMultiplyOnNumber_ReturnMultiply()
        {
            Polynomial pFirst = new Polynomial(new[] { 7, 4, 6, 1 });

            Polynomial expected = new Polynomial(new[] { 49, 28, 42, 7 });

            Polynomial actual = pFirst * 7;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }

        [Test]
        public void PolynomialMultiplyTest_PolynomialMultiplyOnPolynomial_ReturnMultiply()
        {
            Polynomial pFirst = new Polynomial(new[] { 7, 4, 6, 1 });
            Polynomial pSecond = new Polynomial(new[] { 9, 3, 1, 8 });

            Polynomial expected = new Polynomial(new[] { 63, 57, 73, 87, 41, 49, 8 });

            Polynomial actual = pFirst * pSecond;

            Assert.AreEqual(expected.Degree, actual.Degree, "{0} != {1}", expected.Degree, actual.Degree);
            Assert.AreEqual(expected.Coefficients, actual.Coefficients, "{0} != {1}", expected.Coefficients, actual.Coefficients);
        }

        [Test]
        public void PolynomialEquallyTest_PolynomialEqually_ReturnTrue()
        {
            Polynomial pFirst = new Polynomial(new[] { -8, 12, 7, 4 });
            Polynomial pSecond = new Polynomial(new[] { -8, 12, 7, 4 });

            bool expected = true;

            bool actual = (pFirst == pSecond);

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }

        [Test]
        public void PolynomialNotEqualsTest_PolynomialNotEqually_ReturnFalse()
        {
            Polynomial pFirst = new Polynomial(new[] { -8, 38, 7, 4 });
            Polynomial pSecond = new Polynomial(new[] { -8, 12, 7, 4 });

            bool expected = true;

            bool actual = (pFirst != pSecond);

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }

        [Test]
        public void PolynomialEqualsTest_PolynomialEquals_ReturnTrue()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3, 4 });
            Polynomial pSecond = new Polynomial(new[] { 1, 2, 3, 4 });

            bool expected = true;

            bool actual = pFirst.Equals(pSecond);

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }

        [Test]
        public void PolynomialGetHashCode_PolynomialGetHashCode_ReturnFalse()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3, 4 });
            Polynomial pSecond = new Polynomial(new[] { 1, 2, 3, 4 });

            bool expected = true;

            bool actual = pFirst.GetHashCode() != pSecond.GetHashCode();

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }

        [Test]
        public void PolynomialNotEqualGetHashCodeTest_PolynomialGetHashCode_ReturnFalse()
        {
            Polynomial pFirst = new Polynomial(new[] { 1, 2, 3, 4 });
            Polynomial pSecond = new Polynomial(new[] { 1, 8, 3, 4 });

            bool expected = false;

            bool actual = pFirst.GetHashCode() == pSecond.GetHashCode();

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }

        [Test]
        public void PolynomialTostringTest_PolynomialToString_ReturnToString()
        {
            Polynomial pFirst = new Polynomial(new[] { 6, 1, 26, 4 });

            string expected = "Polynomial degree -> 3, coefficients -> 6, 1, 26, 4";

            string actual = pFirst.ToString();

            Assert.AreEqual(expected, actual, "{0} != {1}", expected, actual);
        }
    }
}
