using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_de_combustivel.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }       

        public ICollection<VeiculoUsuarios> Veiculos { get; set; }
    }

    public enum Perfil
    {
        [Display(Name = "Administrador")]
        Adiministrador,
        [Display(Name = "Usuario")]
        Usuario
    }
}
