using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoDeUsuarios.Tests.Entities
{
    [TestClass]
    public class UserTests
    {
        private Name _nameValid = new Name("Lucas", "Lopes Moreno");
        private CPF _cpfValid = new CPF("41040245897");
        private Name _nameInvalid = new Name("", "Lopes Moreno");
        private CPF _cpfInvalid = new CPF("1346546");

        [TestMethod]
        public void RetornaSucessoQuandoCriaUmUsuarioValido()
        {
            var user = new User(_nameValid, _cpfValid, "45763826");
            Assert.IsTrue(user.Valid);
        }

        [TestMethod]
        public void RetornaSucessoQuandoCriaUsuarioInvalido()
        {
            var user = new User(_nameInvalid, _cpfInvalid, "45763826");
            Assert.IsTrue(user.Invalid);
        }
    }
}
