using Pasquali.Application.Services;
using Pasquali.Application.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pasquali.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public AccountController(IUsuarioAppService usuarioAppService)
        {
            this._usuarioAppService = usuarioAppService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var usuario = this._usuarioAppService.ObterUsuario(
                usuarioViewModel.Login,
                usuarioViewModel.Senha,
                System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

            if (usuario.UsuarioId > 0)
            {
                this.SetFormsAuth(usuario.UsuarioId, usuario.Login);

                return RedirectToAction("Index", "Usuario");
            }

            ModelState.AddModelError("", "Usuário ou senha incorretos");
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        private void SetFormsAuth(int usuarioId, string login)
        {
            this.Response.Cookies.Add(
                 new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                                    login,
                                    DateTime.Now,
                                    DateTime.Now.AddMinutes(30),
                                    true,
                                    usuarioId.ToString()))));

        }
    }
}