using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using System;

namespace GestaoDeUsuarios.Domain.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        List<User> GetByCPF(CPF cpf);
        List<User> GetByName(Name name);
        User GetById(Guid id);

        bool CPFExists(CPF cpf);

        void Create(User user);
        bool Update(User user);
        bool Update(User user, Guid id);
        void Delete(User user);

        void Save();
    }
}