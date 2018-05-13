using GestaoDeUsuarios.Shared;
using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Base.ValueObjects;
using static GestaoDeUsuarios.Web.Util.ClientWebApi;

namespace GestaoDeUsuarios.Site.Models
{
    public class Home
    {
        public List<UserDTO> Users { get; set; }        
        public UserDTO UserDTO { get; set; }

        public Home()
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>>("http://localhost:53669/api/user/getall");
            Users = retorno.ListDto;
        }

        public CommandResult<UserDTO> AdicionarUsuario(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/create", userDto);
            return retorno;
        }

        public CommandResult<UserDTO> AtualizarDados(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/update", userDto);
            return retorno;
        }

        public CommandResult<UserDTO> Excluir(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/delete", userDto);
            return retorno;
        }

        public CommandResult<UserDTO> ObterPorNome(Name name)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, Name>("http://localhost:53669/api/user/getbyname", name);
            return retorno;
        }

        public CommandResult<UserDTO> ObterPorCPF(CPF cpf)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, CPF>("http://localhost:53669/api/user/getbycpf", cpf);
            return retorno;
        }
    }
}