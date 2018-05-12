using GestaoDeUsuarios.Shared.Base;

namespace GestaoDeUsuarios.Shared
{
    public class UserDTO : DTOBase
    {
        public UserDTO()
        {

        }

        public UserDTO(string nome, string sobrenome, string cpf, string telefone, string id = "") : base(id)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.cpf = cpf;
            this.telefone = telefone;
        }

        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public string telefone { get; set; }
    }
}