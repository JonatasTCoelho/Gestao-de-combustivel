using Gestao_de_combustivel.Models;
using Gestao_de_combustivel.Services;

namespace Gestao_de_combustivel.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly DataContext _context;

        public VeiculoService(DataContext context)
        {
            _context = context;
        }

        public List<Veiculo> GetVeiculos()
        {
            var veiculos = _context.Veiculos.ToList();
            return veiculos;
        }
    }
}
