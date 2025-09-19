using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infra.Helpers;

namespace TEST_FCG._Tests_.Helpers
{
    [TestClass]
    public class CriptografiaHelperTests
    {
        private CriptografiaHelper _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new CriptografiaHelper();
        }

        [TestMethod]
        public void ReturnsEmptyStringWhenInputIsNull()
        {
            var result = _helper.Criptografar(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReturnsEmptyStringWhenInputIsEmpty()
        {
            var result = _helper.Criptografar(string.Empty);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReturnsBase64StringWhenInputIsValid()
        {
            var senha = "password123";
            var result = _helper.Criptografar(senha);

            Assert.IsFalse(string.IsNullOrEmpty(result));

            try
            {
                Convert.FromBase64String(result);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("Result is not a valid Base64 string.");
            }
        }

        [TestMethod]
        public void ReturnsEmptyStringWhenDescriptografarInputIsNull()
        {
            var result = _helper.Descriptografar(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReturnsEmptyStringWhenDescriptografarInputIsEmpty()
        {
            var result = _helper.Descriptografar(string.Empty);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReturnsOriginalStringWhenInputIsEncrypted()
        {
            var senha = "mySecretPassword!";
            var encrypted = _helper.Criptografar(senha);
            var decrypted = _helper.Descriptografar(encrypted);

            Assert.AreEqual(senha, decrypted);
        }

        [TestMethod]
        public void ThrowsFormatExceptionWhenInputIsNotBase64()
        {
            Assert.ThrowsException<FormatException>(() =>
            {
                _helper.Descriptografar("not_base64_string");
            });
        }
    }
}
