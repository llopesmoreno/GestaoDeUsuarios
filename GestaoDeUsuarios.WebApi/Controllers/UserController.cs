using System;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Services;

namespace GestaoDeUsuarios.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserApplicationService _service;

        public UserController(IUserApplicationService userService) => _service = userService;

        [Route("getall")]
        [HttpPost]
        public Task<HttpResponseMessage> GetAllUsers()
        {
            return CreateResponse(HttpStatusCode.Created, new { message = "API online" });
        }

        [Route("create")]
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

            var user = _service.Register(command);
            return CreateResponse(HttpStatusCode.Created, user.Entitie);
        }

        [Route("update")]
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

            var user = _service.Update(command);
            return CreateResponse(HttpStatusCode.OK, user.Entitie);
        }

        [Route("delete")]
        [HttpPost]
        public Task<HttpResponseMessage> DeleteUser([FromBody]dynamic body)
        {
            if (InvalidBody(body))
                return CreateResponse(HttpStatusCode.BadRequest, "Dados inválidos");

            var command = new DeleteUserCommand((Guid)body.id);

            var user = _service.Delete(command);

            return CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
