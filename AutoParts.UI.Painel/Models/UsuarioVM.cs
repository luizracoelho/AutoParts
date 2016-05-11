using System.ComponentModel.DataAnnotations;

namespace AutoParts.UI.Painel.Models
{
    public class UsuarioVM
    {
        public int UsuarioId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login { get; set; }

        [Required, DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha"), Compare("Senha"), DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }

    public class UsuarioEntrarVM
    {
        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
