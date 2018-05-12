using System;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Services;

namespace GestaoDeUsuarios.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApplicationService _service;

        public UserController(IUserApplicationService userService) => _service = userService;

        [HttpGet]
        public Task<HttpResponseMessage> Get()
        {
            return CreateResponse(HttpStatusCode.Created, new { message = "API online" });
        }

        [HttpPost]
        public Task<HttpResponseMessage> Post([FromBody]dynamic body)
        {
            var command = new CreateUserCommand(
                       nome: (string)body.nome,
                       sobrenome: (string)body.sobrenome,
                       cPF: (string)body.cpf,
                       telefone: (string)body.telefone
                   );

            var user = _service.Register(command);
            return CreateResponse(HttpStatusCode.Created, command);
        }

        [HttpPost]
        public Task<HttpResponseMessage> Put([FromBody]dynamic body)
        {
            var command = new UpdateUserCommand(
                       id: (Guid)body.id,
                       nome: (string)body.nome,
                       sobrenome: (string)body.sobrenome,
                       cPF: (string)body.cpf,
                       telefone: (string)body.telefone
                   );

            var user = _service.Update(command);
            return CreateResponse(HttpStatusCode.OK, command);
        }

        [HttpPost]
        public Task<HttpResponseMessage> Delete([FromBody]dynamic body)
        {
            var command = new DeleteUserCommand((Guid)body.id);

            var user = _service.Delete(command);
            return CreateResponse(HttpStatusCode.OK, command);
        }
    }
}
