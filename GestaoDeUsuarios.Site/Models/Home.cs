using GestaoDeUsuarios.Shared;
using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Commands;
using static GestaoDeUsuarios.Web.Util.ClientWebApi;

namespace GestaoDeUsuarios.Site.Models
{
    public class Home
    {
        private List<User> Users { get; set; }        
        public UserDTO UserDTO { get; set; }

        public CommandResult<UserDTO> AdicionarUsuario(UserDTO userDto)
        {
            var retorno = RealizarRequisicao<CommandResult<UserDTO>, UserDTO>("http://localhost:53669/api/user/create", userDto);
            return retorno;
        }
    }
}