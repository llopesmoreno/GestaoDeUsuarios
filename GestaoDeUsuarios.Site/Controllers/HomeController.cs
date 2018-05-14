using System.Web.Mvc;
using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Site.Models;

namespace GestaoDeUsuarios.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Home();

            if (Request.IsAjaxRequest())
                return PartialView("_TableUsers", model);

            return View(model);
        }

        [HttpPost]
        public JsonResult Cadastrar(UserDTO dto)
        {
            var model = new Home();

            var result = model.AdicionarUsuario(dto);

            return Json(result);
        }

        [HttpPost]
        public JsonResult AtualizarDados(UserDTO dto)
        {
            var model = new Home();

            var result = model.AtualizarDados(dto);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Excuir(UserDTO dto)
        {
            var model = new Home();

            var result = model.Excluir(dto);

            return Json(result);
        }
    }
}