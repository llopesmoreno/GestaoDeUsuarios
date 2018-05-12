using System.Linq;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using static GestaoDeUsuarios.Domain.Entities.Convert.ConvertUser;

namespace GestaoDeUsuarios.Domain.Handlers
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
            var user = userRepository.GetByCPF(cpf).ToDTO();
                      
            //todo criar resource
            var message = user == null ? $"CPF Não encontrado" : "";

            return new CommandResult<UserDTO>(true, message, user);
        }

        public CommandResult<UserDTO> GetByName(Name nome)
        {
            var user = userRepository.GetByName(nome).ToDTO();

            //todo criar resource
            var message = user == null ? $"Nome Não encontrado" : "";

            return new CommandResult<UserDTO>(true, message, user);
        }
    }
}
