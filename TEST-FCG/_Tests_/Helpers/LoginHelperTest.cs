using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infra.Helpers;

namespace TEST_FCG._Tests_.Helpers
{
    [TestClass]
    public class LoginHelperTest
    {
        private LoginHelper _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new LoginHelper();
        }

        [TestMethod]
        public void ReturnsTrueWhenEmailsMatch()
        {
            var emailLogin = "user@example.com";
            var emailBanco = "user@example.com";

            var result = _helper.VerificarCadastroEmail(emailLogin, emailBanco);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnsFalseWhenEmailsDoNotMatch()
        {
            var emailLogin = "user@example.com";
            var emailBanco = "other@example.com";

            var result = _helper.VerificarCadastroEmail(emailLogin, emailBanco);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseWhenOneEmailIsNull()
        {
            var emailLogin = null as string;
            var emailBanco = "user@example.com";

            var result = _helper.VerificarCadastroEmail(emailLogin, emailBanco);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsTrueWhenBothEmailsAreNull()
        {
            var emailLogin = null as string;
            var emailBanco = null as string;

            var result = _helper.VerificarCadastroEmail(emailLogin, emailBanco);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnsTrueWhenPasswordsMatch()
        {
            var senhaLogin = "password123";
            var senhaBanco = "password123";

            var result = _helper.VerificarCadastroSenha(senhaLogin, senhaBanco);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnsFalseWhenPasswordsDoNotMatch()
        {
            var senhaLogin = "password123";
            var senhaBanco = "otherpassword";

            var result = _helper.VerificarCadastroSenha(senhaLogin, senhaBanco);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseWhenOnePasswordIsNull()
        {
            var senhaLogin = null as string;
            var senhaBanco = "password123";

            var result = _helper.VerificarCadastroSenha(senhaLogin, senhaBanco);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsTrueWhenBothPasswordsAreNull()
        {
            var senhaLogin = null as string;
            var senhaBanco = null as string;

            var result = _helper.VerificarCadastroSenha(senhaLogin, senhaBanco);

            Assert.IsTrue(result);
        }
    }
}
