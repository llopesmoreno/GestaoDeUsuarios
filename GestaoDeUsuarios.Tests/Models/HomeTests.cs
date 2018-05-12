using GestaoDeUsuarios.Domain.Base.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestaoDeUsuarios.Site.Models;
using GestaoDeUsuarios.Shared;

namespace GestaoDeUsuarios.Tests.Models
{
    [TestClass]
    public class HomeTeste
    {
        private string nome = "lucas";
        private string sobrenome = "lopes";
        private string cpf = "41040245897";
        private Name _nameValid = new Name("Lucas", "Lopes Moreno");
        private CPF _cpfValid = new CPF("41040245897");
        private Name _nameInvalid = new Name("", "Lopes Moreno");
        private CPF _cpfInvalid = new CPF("1346546");

        [TestMethod]
        public void RetornaSucessoQuandoCriaUmUsuarioValido()
        {
            var home = new Home();

            var userDTO = new UserDTO(nome, sobrenome, cpf, "1111651656");

            home.AdicionarUsuario(userDTO);
        }

       
    }
}
