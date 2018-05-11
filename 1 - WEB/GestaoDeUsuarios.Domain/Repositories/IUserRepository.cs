using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using System;

namespace GestaoDeUsuarios.Domain.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetByCPF(CPF cpf);
        User GetByName(Name name);
        User GetById(Guid id);

        bool CPFExists(CPF cpf);

        void Create(User user);
        void Update(User user);
        void Delete(User user);

        void Save();
    }
}