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
    [Authorize]
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

            foreach (var item in lista)
                item.Imagem = OnUpload.ObterImagem(item.ServicoId, "Servico");

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

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(servicoVm);
            }
        }

        public ActionResult Remover(int id)
        {
            try
            {
                var servico = bo.Encontrar(id);

                if (servico == null)
                    throw new HttpException(404, "Não Encontrado");

                bo.Remover(servico);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}