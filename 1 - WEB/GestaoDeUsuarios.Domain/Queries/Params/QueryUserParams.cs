using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.Domain.Queries.Params
{
    public class QueryUserParams
    {
        public QueryUserParams(string nome, string sobrenome, string cpf)
        {
            Nome = new Name(nome, sobrenome);
            Cpf = new CPF(cpf);
        }

        public Name Nome { get; set; }
        public CPF Cpf { get; set; }
    }
}
