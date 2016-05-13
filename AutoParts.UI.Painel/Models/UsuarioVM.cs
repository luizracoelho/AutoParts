using System.ComponentModel.DataAnnotations;

namespace AutoParts.UI.Painel.Models
{
    public class UsuarioVM
    {
        public int UsuarioId { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [Required, MaxLength(25)]
        public string Login { get; set; }

        [Required, MaxLength(25), DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha"), Compare("Senha"), DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

        public string Imagem { get; set; }
    }

    public class UsuarioEntrarVM
    {
        public string Login { get; set; }

        [Required, MaxLength(25), DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
