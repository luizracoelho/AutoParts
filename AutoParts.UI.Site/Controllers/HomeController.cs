using AutoMapper;
using AutoParts.BL.Tools;
using AutoParts.BLL;
using AutoParts.BLL.Tools;
using AutoParts.DL;
using AutoParts.UI.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AutoParts.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        ProdutoBO produtoBO;
        ServicoBO servicoBO;

        public HomeController()
        {
            produtoBO = new ProdutoBO();
            servicoBO = new ServicoBO();
        }

        public ActionResult Index() => View();

        public ActionResult Produtos()
        {
            try
            {
                var lista = Mapper.Map<IList<Produto>, IList<ProdutoVM>>(produtoBO.Listar());

                foreach (var item in lista)
                    item.Imagem = OnUpload.ObterImagem(item.ProdutoId, "Produto");

                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DetalhesProduto(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception();

                var produto = produtoBO.Encontrar((int)id);

                if (produto == null)
                    throw new Exception();

                var produtoVM = Mapper.Map<Produto, ProdutoVM>(produto);

                return View(produtoVM);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Não Encontrado");
            }
        }

        public ActionResult Servicos()
        {
            try
            {
                var lista = Mapper.Map<IList<Servico>, IList<ServicoVM>>(servicoBO.Listar());

                foreach (var item in lista)
                    item.Imagem = OnUpload.ObterImagem(item.ServicoId, "Servico");

                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DetalhesServico(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception();

                var servico = servicoBO.Encontrar((int)id);

                if (servico == null)
                    throw new Exception();

                var servicoVM = Mapper.Map<Servico, ServicoVM>(servico);

                return View(servicoVM);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Não Encontrado");
            }
        }

        public ActionResult Contato()
        {
            var sucesso = false;

            if (TempData["Sucesso"] != null)
                sucesso = (bool)TempData["Sucesso"];

            if (sucesso)
                ViewBag.Sucesso = "E-mail enviado com sucesso.";

            return View();
        }

        [HttpPost]
        public ActionResult Contato(EmailVM emailVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Alguns campos obrigatórios não foram preenchidos.");

                var email = MapearEmail(emailVM);

                var emailBo = new Mail(ObterMensagemEmail(email));

                emailBo.Enviar(email);

                TempData["Sucesso"] = true;

                return RedirectToAction("Contato");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(emailVM);
            }
        }

        private static Email MapearEmail(EmailVM emailVM)
        {
            return new Email
            {
                Assunto = emailVM.Assunto,
                Empresa = emailVM.Empresa,
                EnderecoEmail = emailVM.EnderecoEmail,
                Mensagem = emailVM.Mensagem,
                Nome = emailVM.Nome,
                Telefone = emailVM.Telefone
            };
        }

        private static string ObterMensagemEmail(Email email)
        {
            var sb = new StringBuilder();

            sb.Append("<body style='background-color:#eee;'>");
            sb.Append("<div style='padding:1rem;width:100%;color:#ddd;background-color:#16367F;display:flex'>");
            sb.Append("<div style='width:25%;text-align:center;'>");
            sb.Append("<img src='http://imagizer.imageshack.us/v2/280x200q90/924/cQ1zTn.png' alt='SQL AutoParts' style='width:100%;'/>");
            sb.Append("</div>");
            sb.Append("<div style='width:75%;text-align:center;'>");
            sb.Append("<h1>Obrigado =D</h1>");
            sb.Append("<h3>");
            sb.Append("Estamos muito felizes com seu contato.");
            sb.Append("<br />");
            sb.Append("Em breve te responderemos");
            sb.Append("</h3>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='padding:1rem;margin-left:25%;width:75%;color:#555;'>");
            sb.Append("<dl>");
            sb.Append("<dt><strong>Nome</strong></dt>");
            sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'>{email.Nome}</dd>");
            if (!string.IsNullOrEmpty(email.Empresa))
            {
                sb.Append("<dt><strong>Empresa</strong></dt>");
                sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'>{email.Empresa}</dd>");
            }
            if (!string.IsNullOrEmpty(email.Telefone))
            {
                sb.Append("<dt><strong>Telefone</strong></dt>");
                sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'><a href='tel:{email.Telefone}' style='color:#16367F;'/>{email.Telefone}</a></dd>");
            }
            sb.Append("<dt><strong>E-Mail</strong></dt>");
            sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'><a href='mailto:{email.EnderecoEmail}' style='color:#16367F'/>{email.EnderecoEmail}</a></dd>");
            sb.Append("<dt><strong>Assunto</strong></dt>");
            sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'>{email.Assunto}</dd>");
            sb.Append("<dt><strong>Mensagem</strong></dt>");
            sb.Append($"<dd style='padding-bottom:1rem;text-align:justified;'>{email.Mensagem}</dd>");
            sb.Append("</dl>");
            sb.Append("</body>");

            return sb.ToString();
        }
    }
}