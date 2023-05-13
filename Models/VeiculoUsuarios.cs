using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_de_combustivel.Models
{
    [Table("VeiculoUsuarios")]
    public class VeiculoUsuarios
    {
        public int VeiculoID { get; set; }
        public Veiculo Veiculo { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
