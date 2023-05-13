using Gestao_de_combustivel.Models;
using Gestao_de_combustivel.Services;

namespace Gestao_de_combustivel.Services
{
    public class ConsumoService : IConsumosService
    {
        private readonly DataContext _context;

        public ConsumoService(DataContext context)
        {
            _context = context;
        }

        public List<Consumo> GetConsumos()
        {
            var consumos = _context.Consumos.ToList();
            return consumos;
        }
    }
}
