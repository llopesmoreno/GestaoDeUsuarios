using GestaoDeUsuarios.Shared;
using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Queries.Params;
using static GestaoDeUsuarios.Web.Util.ClientWebApi;

namespace GestaoDeUsuarios.Site.Models
{
    public class Home
    {
        public List<UserDTO> Users { get; set; }        
        public UserDTO UserDTO { get; set; }

        public Home()
        {
            ObterTodos();
        }

        public void ObterTodos()
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>>("http://localhost:53669/api/user/ObterTodos");
            Users = retorno.ListDto;
        }

        public CommandResult<UserDTO> AdicionarUsuario(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/CadastrarUsuario", userDto);

            ObterTodos();

            return retorno;
        }

        public CommandResult<UserDTO> AtualizarDados(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/AtualizarUsuario", userDto);

            ObterTodos();

            return retorno;
        }

        public CommandResult<UserDTO> Excluir(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/DeletarUsuario", userDto);

            ObterTodos();

            return retorno;
        }

        public CommandResult<UserDTO> ObterPor(QueryUserParams parametros)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, QueryUserParams>("http://localhost:53669/api/user/ConsultarUsuario", parametros);

            Users = retorno.ListDto;

            return retorno;
        }
    }
}