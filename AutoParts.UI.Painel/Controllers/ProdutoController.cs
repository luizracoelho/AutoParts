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
    public class ProdutoController : Controller
    {
        ProdutoBO bo;

        public ProdutoController()
        {
            bo = new ProdutoBO();
        }

        public ActionResult Index()
        {
            var lista = Mapper.Map<IList<Produto>, IList<ProdutoVM>>(bo.Listar());
            return View(lista);
        }

        public ActionResult Salvar(int? id)
        {
            try
            {
                if (id == null)
                    return View();

                var produto = bo.Encontrar((int)id);

                if (produto == null)
                    throw new HttpException(404, "Não Encontrado");

                var produtoVM = Mapper.Map<Produto, ProdutoVM>(produto);

                return View(produtoVM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Salvar(ProdutoVM produtoVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("O modelo não é válido.");

                var produto = Mapper.Map<ProdutoVM, Produto>(produtoVm);

                bo.Salvar(produto);

                return View("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(produtoVm);
            }
        }

        [HttpPost]
        public ActionResult Remover(int id)
        {
            try
            {
                var produto = bo.Encontrar(id);

                if (produto == null)
                    throw new HttpException(404, "Não Encontrado");

                bo.Remover(produto);

                return View("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}