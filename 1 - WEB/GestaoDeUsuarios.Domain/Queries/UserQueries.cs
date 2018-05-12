using GestaoDeUsuarios.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace GestaoDeUsuarios.Domain.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> GetByCPF(string cpf) => x => x.CPF.Value == cpf;

        public static Expression<Func<User, bool>> GetByName(string nome) => x => x.Name.ToString() == nome;
    }
}
