using System.Linq;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using static GestaoDeUsuarios.Domain.Entities.Convert.ConvertUser;
using System.Collections.Generic;

namespace GestaoDeUsuarios.Domain.Queries
{
    public class UserQueries
    {
        public UserQueries(IUserRepository repository) => userRepository = repository;

        private readonly IUserRepository userRepository;

        public CommandResult<UserDTO> GetAllUsers()
        {
            var users = userRepository.GetAll().ToList().ToDTO();

            //todo criar resource
            var message = users.Any() ? $"Total: {users.Count()}" : "Não há nenhum usuário cadastrado!";

            return new CommandResult<UserDTO>(true, message, listDto: users);
        }

        public CommandResult<UserDTO> GetByCPF(CPF cpf)
        {
            var user = userRepository.GetByCPF(cpf);

            var retorno = ConverterUsuario(user);

            return retorno;
        }

        public CommandResult<UserDTO> GetByName(Name nome)
        {
            var user = userRepository.GetByName(nome);

            var retorno = ConverterUsuario(user);

            return retorno;
        }

        private CommandResult<UserDTO> ConverterUsuario(List<User> users)
        {
            var message = string.Empty;
            if (users.Any())
                return new CommandResult<UserDTO>(true, message, listDto: users.ToDTO());

            message = "Usuário não encontrado";
            return new CommandResult<UserDTO>(true, message);


        }
    }
}
