using AutoMapper;
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
    public class ServicoController : Controller
    {
        ServicoBO bo;

        public ServicoController()
        {
            bo = new ServicoBO();
        }

        public ActionResult Index()
        {
            var lista = Mapper.Map<IList<Servico>, IList<ServicoVM>>(bo.Listar());
            return View(lista);
        }

        public ActionResult Salvar(int? id)
        {
            try
            {
                if (id == null)
                    return View();

                var servico = bo.Encontrar((int)id);

                if (servico == null)
                    throw new HttpException(404, "Não Encontrado");

                var servicoVM = Mapper.Map<Servico, ServicoVM>(servico);

                return View(servicoVM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Salvar(ServicoVM servicoVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("O modelo não é válido.");

                var servico = Mapper.Map<ServicoVM, Servico>(servicoVm);

                bo.Salvar(servico);

                return View("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(servicoVm);
            }
        }

        [HttpPost]
        public ActionResult Remover(int id)
        {
            try
            {
                var servico = bo.Encontrar(id);

                if (servico == null)
                    throw new HttpException(404, "Não Encontrado");

                bo.Remover(servico);

                return View("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}