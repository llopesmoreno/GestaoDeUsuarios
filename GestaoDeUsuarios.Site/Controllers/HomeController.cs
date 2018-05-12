using GestaoDeUsuarios.Shared;
using GestaoDeUsuarios.Site.Models;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GestaoDeUsuarios.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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