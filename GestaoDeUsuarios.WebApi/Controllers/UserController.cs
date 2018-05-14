using System;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Queries.Params;

namespace GestaoDeUsuarios.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserApplicationService<UserDTO> _service;

        public UserController(IUserApplicationService<UserDTO> userService) => _service = userService;

        [Route("ObterTodos")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAllUsers()
        {
            var result = _service.GetAll();
             
            var response =  CreateResponse(HttpStatusCode.OK, result);

            return response;
        }

        [Route("ConsultarUsuario")]
        [HttpPost]
        public Task<HttpResponseMessage> GetByParams([FromBody]dynamic body)
        {
            if (InvalidBody(body))
                return CreateResponse(HttpStatusCode.BadRequest, new { success = false, erros = "Dados inválidos" });

            var queryUserParams = new QueryUserParams(
                nome: (string)body.nome,
                sobrenome: (string)body.sobrenome,
                cpf: (string)body.cpf
            );

            var result = _service.GetByParams(queryUserParams);

            var response = CreateResponse(HttpStatusCode.OK, result);

            return response;
        }

        [Route("CadastrarUsuario")]
        [HttpPost]
        public Task<HttpResponseMessage> CreateUser([FromBody]dynamic body)
        {
            if(InvalidBody(body))
                return CreateResponse(HttpStatusCode.BadRequest, "Dados inválidos"); 
            
            var command = new CreateUserCommand(
                       nome: (string)body.nome,
                       sobrenome: (string)body.sobrenome,
                       cPF: (string)body.cpf,
                       telefone: (string)body.telefone
                   );

            var result = _service.Register(command);
            var retorno = CreateResponse(HttpStatusCode.Created, result);

            return retorno;
        }

        [Route("AtualizarUsuario")]
        [HttpPost]
        public Task<HttpResponseMessage> UpdateUser([FromBody]dynamic body)
        {
            if (InvalidBody(body))
                return CreateResponse(HttpStatusCode.BadRequest, "Dados inválidos");

            var command = new UpdateUserCommand(
                       id: (Guid)body.id,
                       nome: (string)body.nome,
                       sobrenome: (string)body.sobrenome,
                       cPF: (string)body.cpf,
                       telefone: (string)body.telefone
                   );

            var result = _service.Update(command);
            return CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("DeletarUsuario")]
        [HttpPost]
        public Task<HttpResponseMessage> DeleteUser([FromBody]dynamic body)
        {
            if (InvalidBody(body))
                return CreateResponse(HttpStatusCode.BadRequest, "Dados inválidos");

            var command = new DeleteUserCommand((Guid)body.id);

            var result = _service.Delete(command);

            return CreateResponse(HttpStatusCode.OK, result);
        }
    }
}