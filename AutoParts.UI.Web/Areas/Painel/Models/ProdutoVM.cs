using System.ComponentModel.DataAnnotations;

namespace AutoParts.UI.Web.Areas.Painel.Models
{
    public class ProdutoVM
    {
        public int ProdutoId { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [Required, Display(Name = "Descrição"), DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }

        public string Imagem { get; set; }
    }
}
