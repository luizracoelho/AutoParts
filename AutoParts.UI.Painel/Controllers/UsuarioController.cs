﻿using AutoMapper;
using AutoParts.BLL;
using AutoParts.DL;
using AutoParts.UI.Painel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoParts.UI.Painel.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBO bo;

        public UsuarioController()
        {
            bo = new UsuarioBO();
        }

        public ActionResult Index()
        {
            var lista = Mapper.Map<IList<Usuario>, IList<UsuarioVM>>(bo.Listar());
            return View(lista);
        }

        public ActionResult Salvar(int? id)
        {
            try
            {
                if (id == null)
                    return View();

                var usuario = bo.Encontrar((int)id);

                if (usuario == null)
                    throw new HttpException(404, "Não Encontrado");

                var usuarioVM = Mapper.Map<Usuario, UsuarioVM>(usuario);

                return View(usuarioVM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Salvar(UsuarioVM usuarioVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("O modelo não é válido.");

                var usuario = Mapper.Map<UsuarioVM, Usuario>(usuarioVm);

                bo.Salvar(usuario);

                return View("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(usuarioVm);
            }
        }

        [HttpPost]
        public ActionResult Remover(int id)
        {
            try
            {
                var usuario = bo.Encontrar(id);

                if (usuario == null)
                    throw new HttpException(404, "Não Encontrado");

                bo.Remover(usuario);

                return View("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}