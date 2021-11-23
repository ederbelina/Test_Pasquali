using Pasquali.Application.Services;
using Pasquali.Application.ViewModels;
using System.Web.Mvc;

namespace Pasquali.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly ILogAcessoAppService _logAcessoAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService, ILogAcessoAppService logAcessoAppService)
        {
            this._usuarioAppService = usuarioAppService;
            this._logAcessoAppService = logAcessoAppService;
        }

        public ActionResult Index()
        {
            return View(this._usuarioAppService.ObterTodosUsuarios());
        }

        public ActionResult SaveUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUser(UsuarioViewModel usuarioViewModel)
        {
            this._usuarioAppService.SalvarUsuario(usuarioViewModel);

            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Usuario");

            return RedirectToAction("Index", "Account");
        }

        public ActionResult DeleteUser(int usuarioId)
        {
            this._usuarioAppService.DeletarUsuario(usuarioId);

            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult UpdateUser(int usuarioId)
        {
            return View(this._usuarioAppService.ObterUsuarioPorId(usuarioId));
        }

        public ActionResult LogAccess()
        {
            return View(this._logAcessoAppService.ObterLogAcesso());
        }
    }
}