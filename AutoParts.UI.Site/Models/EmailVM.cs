using System.ComponentModel.DataAnnotations;

namespace AutoParts.UI.Site.Models
{
    public class EmailVM
    {
        [Required]
        public string Nome { get; set; }

        public string Empresa { get; set; }

        public string Telefone { get; set; }

        [Required, Display(Name = "E-Mail"), DataType(DataType.EmailAddress, ErrorMessage = "Insira um endereço de e-mail válido.")]
        public string EnderecoEmail { get; set; }

        [Required]
        public string Assunto { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }
    }
}
