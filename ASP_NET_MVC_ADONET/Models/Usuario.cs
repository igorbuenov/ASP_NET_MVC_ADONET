using System.ComponentModel.DataAnnotations;

namespace ASP_NET_MVC_ADONET.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Sobrenome!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email!"), EmailAddress(ErrorMessage = "Formato de email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Cargo!")]
        public string Cargo { get; set; }
    }
}
