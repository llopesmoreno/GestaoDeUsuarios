using System;
using System.Linq;
using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuariosFakeInfra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users;

        public UserRepository() => Users = FakeDB.Users;        

        public bool CPFExists(CPF cpf) => Users.Where(u => u.CPF.Value == cpf.Value).Any();

        public void Create(User user) => Users.Add(user);

        public void Delete(User user) => Users.Remove(user);        

        public ICollection<User> GetAll() => Users;

        public User GetByCPF(CPF cpf) => Users.FirstOrDefault(u => u.CPF == cpf);

        public User GetById(Guid id) => Users.FirstOrDefault(u => u.Id == id);

        public User GetByName(Name name) => Users.FirstOrDefault(u => u.Name == name);

        public bool Update(User user)
        {
            var userToUpdate = GetById(user.Id);

            Delete(userToUpdate);

            if (CPFExists(user.CPF))
            {
                Create(userToUpdate);
                return false;
            }

            userToUpdate.Update(user.Name, user.CPF, user.Telefone);
            Create(userToUpdate);

            return true;
        }

        public bool Update(User user, Guid id)
        {
            var userToUpdate = GetById(id);

            Delete(userToUpdate);

            if (CPFExists(user.CPF))
            {
                Create(userToUpdate);
                return false;
            }

            userToUpdate.Update(user.Name, user.CPF, user.Telefone);

            Create(userToUpdate);

            return true;
        }

        public void Save() => FakeDB.SaveChanges(Users);
    }
}
