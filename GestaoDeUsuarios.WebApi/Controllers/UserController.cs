using System;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Domain.Services;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserApplicationService<UserDTO> _service;

        public UserController(IUserApplicationService<UserDTO> userService) => _service = userService;

        [Route("getall")]
        [HttpGet]
        public Task<HttpResponseMessage> GetAllUsers()
        {
            var result = _service.GetAll();
             
            var response =  CreateResponse(HttpStatusCode.OK, result);

            return response;
        }

        [Route("getbyname")]
        [HttpPost]
        public Task<HttpResponseMessage> GetByName(Name name)
        {
            if (name.Invalid)            
                return CreateResponse(HttpStatusCode.BadRequest, new { success = false, erros = name.Notifications });
            
            var result = _service.GetByName(name);

            var response = CreateResponse(HttpStatusCode.OK, result);

            return response;
        }

        [Route("getbycpf")]
        [HttpPost]
        public Task<HttpResponseMessage> GetByCPF(CPF cpf)
        {
            if (cpf.Invalid)
                return CreateResponse(HttpStatusCode.BadRequest, new { success = false, erros = cpf.Notifications });

            var result = _service.GetByCPF(cpf);

            var response = CreateResponse(HttpStatusCode.OK, result);

            return response;
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
            var retorno = CreateResponse(HttpStatusCode.Created, user);

            return retorno;
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

            var result = _service.Update(command);
            return CreateResponse(HttpStatusCode.OK, result);
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