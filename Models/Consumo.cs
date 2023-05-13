using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  

namespace Gestao_de_combustivel.Models
{
    [Table("Consumos")]  
    public class Consumo  
    {
        [Key]  
        public int ID { get; set; }  
        [Required] 
        public string Descricao { get; set; }  
        [Required]  
        public DateTime Data { get; set; }  
        [Required]  
        [Column(TypeName = "decimal(18,2)")]  
        public Decimal Valor { get; set; }  
        [Required]  
        public TipoCombustivel Tipo { get; set; }  
          
        [Required]  
        public int VeiculoID { get; set; }  
           
        public Veiculo Veiculo { get; set; }  
    }  
       
    public enum TipoCombustivel  
    {  
        Diesel,  
        Etanol,  
        Gasolina  
    }  
}
