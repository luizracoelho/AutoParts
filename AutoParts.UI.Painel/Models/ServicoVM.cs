using System.ComponentModel.DataAnnotations;

namespace AutoParts.UI.Painel.Models
{
    public class ServicoVM
    {
        public int ServicoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, Display(Name = "Descrição"), DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Valor { get; set; }
    }
}
