using Microsoft.EntityFrameworkCore; 

namespace Gestao_de_combustivel.Models
{
    public class DataContext : DbContext 
    { 
        public DataContext(DbContextOptions options) : base(options) 
        { 
             
        } 
         
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.Entity<VeiculoUsuarios>() 
                .HasKey(c => new { c.VeiculoID, c.UsuarioID });

            modelBuilder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Veiculo).WithMany(c => c.Usuarios)
                .HasForeignKey(c => c.VeiculoID);

            modelBuilder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Usuario).WithMany(c => c.Veiculos)
                .HasForeignKey(c => c.UsuarioID);
        } 

        public DbSet<Veiculo> Veiculos { get; set; } 
        public DbSet<Consumo> Consumos { get; set; }   
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VeiculoUsuarios> VeiculoUsuarios { get; set; }
    } 
}
