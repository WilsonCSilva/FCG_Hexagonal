using Infra.Helpers;

namespace TEST_FCG._Tests_.Helpers
{
    [TestClass]
    public class TextoHelperTest
    {
        private TextoHelper? _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new TextoHelper();
        }

        [TestMethod]
        public void ReturnsTrueForValidEmail()
        {
            var email = "user@example.com";
            var result = _helper!.EmailValido(email);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnsFalseForInvalidEmail_NoAt()
        {
            var email = "userexample.com";
            var result = _helper!.EmailValido(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseForInvalidEmail_NoDomain()
        {
            var email = "user@";
            var result = _helper!.EmailValido(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseForEmptyString()
        {
            var email = "";
            var result = _helper!.EmailValido(email);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseForNull()
        {
            string? email = null;
            var result = _helper!.EmailValido(email!);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsTrueForValidPassword()
        {
            var senha = "Abcdef1!";
            var result = _helper!.SenhaValida(senha);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnsFalseIfTooShort()
        {
            var senha = "A1b!";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseIfNoUppercase()
        {
            var senha = "abc123!";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseIfNoLowercase()
        {
            var senha = "ABC123!";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseIfNoNumber()
        {
            var senha = "Abcdef!";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseIfNoSpecialChar()
        {
            var senha = "Abc1234";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseForSenhaEmptyString()
        {
            var senha = "";
            var result = _helper!.SenhaValida(senha);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnsFalseForSenhaNull()
        {
            string? senha = null;
            var result = _helper!.SenhaValida(senha!);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseReturnsTrueForTipo1()
        {
            short tipo = 1;
            var result = _helper!.TipoUsuarioValido(tipo);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FalseReturnsTrueForTipo2()
        {
            short tipo = 2;
            var result = _helper!.TipoUsuarioValido(tipo);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FalseReturnsFalseForTipo0()
        {
            short tipo = 0;
            var result = _helper!.TipoUsuarioValido(tipo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseReturnsFalseForTipo3()
        {
            short tipo = 3;
            var result = _helper!.TipoUsuarioValido(tipo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FalseReturnsFalseForNegativeTipo()
        {
            short tipo = -1;
            var result = _helper!.TipoUsuarioValido(tipo);
            Assert.IsFalse(result);
        }
    }
}
