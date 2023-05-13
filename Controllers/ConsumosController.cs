using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestao_de_combustivel.Models;
using Gestao_de_combustivel.Services;

namespace Gestao_de_combustivel.Controllers
{
    public class ConsumosController : Controller
    {
        private readonly DataContext _context;
        private readonly IConsumosService _consumoService;

        public ConsumosController(DataContext context, IConsumosService consumoService)
        {
            _context = context;
            _consumoService = consumoService;

        }

        public ActionResult Consumos()
        {
            return View("Consumos");
        }

        public JsonResult GetConsumos()
        {
            var consumos = _consumoService.GetConsumos();
            return Json(consumos);
        }

        // GET: Consumos
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Consumos.Include(c => c.Veiculo);
            return View(await dataContext.ToListAsync());
        }

        // GET: Consumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consumos == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // GET: Consumos/Create
        public IActionResult Create()
        {
            ViewData["VeiculoID"] = new SelectList(_context.Veiculos, "ID", "Marca");
            return View();
        }

        // POST: Consumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(Consumo consumo)
        {
           
                _context.Consumos.Add(consumo);
                await _context.SaveChangesAsync();
                return Ok(consumo);
            
            
        }

        // GET: Consumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consumos == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo == null)
            {
                return NotFound();
            }
            ViewData["VeiculoID"] = new SelectList(_context.Veiculos, "ID", "Marca", consumo.VeiculoID);
            return View(consumo);
        }

        // POST: Consumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Consumo consumo)
        {
            if (id != consumo.ID)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Consumos.Update(consumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumoExists(consumo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(consumo);
            
            
        }

        // GET: Consumos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consumos == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // POST: Consumos/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consumos == null)
            {
                return Problem("Entity set 'DataContext.Consumos'  is null.");
            }
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo != null)
            {
                _context.Consumos.Remove(consumo);
            }
            
            await _context.SaveChangesAsync();
            return Ok(consumo);
        }

        private bool ConsumoExists(int id)
        {
          return (_context.Consumos?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
