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

            return View(model);
        }

        [HttpPost]
        public JsonResult Cadastrar(UserDTO dto)
        {
            var model = new Home();

            var result = model.AdicionarUsuario(dto);

            return Json(result);
        }
    }
}