using AutoMapper;
using AutoParts.BLL;
using AutoParts.DL;
using AutoParts.UI.Painel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AutoParts.UI.Painel.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(int id, string entidade)
        {
            try
            {
                //Fazer o upload
                OnUpload.Upload(id, entidade);

                //Retornar os Arquivos
                return Json(OnUpload.ObterImagem(id, entidade), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Entrar() => View();

        [HttpPost]
        public ActionResult Entrar(UsuarioEntrarVM usuarioVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("O Modelo não é válido.");

                var usuario = Mapper.Map<UsuarioEntrarVM, Usuario>(usuarioVM);

                var bo = new UsuarioBO();

                bo.Login(usuario);

                FormsAuthentication.SetAuthCookie(usuario.Login, false);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(usuarioVM);
            }
        }

        [Authorize]
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();

            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}