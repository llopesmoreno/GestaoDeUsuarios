using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Services;

namespace GestaoDeUsuarios.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class AccountController : BaseController
    {
        private readonly IUserApplicationService _service;

        public AccountController(IUserApplicationService userService)
        {
            _service = userService;
        }

        [HttpPost]
        [Route("account")]
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

        [HttpGet]
        [Route("account")]
        public Task<HttpResponseMessage> Get()
        {
            return CreateResponse(HttpStatusCode.Created, new { message = "API online" });
        }
    }   
}