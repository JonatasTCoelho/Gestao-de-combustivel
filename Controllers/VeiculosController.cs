using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestao_de_combustivel.Models;
using Gestao_de_combustivel.Services;
using FastReport.Barcode;
using FastReport.Export.PdfSimple;

namespace Gestao_de_combustivel.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly DataContext _context;
        private readonly IVeiculoService _veiculoService;

        public VeiculosController(DataContext context, IVeiculoService veiculoService)
        {
            _context = context;
            _veiculoService = veiculoService;

        }

        public ActionResult Veiculos()
        {
            return View();
        }

        public JsonResult GetVeiculos()
        {
            var veiculos = _veiculoService.GetVeiculos();
            return Json(veiculos);
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
              return _context.Veiculos != null ? 
                          View(await _context.Veiculos.ToListAsync()) :
                          Problem("Entity set 'DataContext.Veiculos'  is null.");
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos   
                .Include(t => t.Usuarios).ThenInclude(t => t.Usuario)
                .Include(t => t.Consumos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
           if(veiculo.AnoFabricacao <= 0 || veiculo.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano de Fabricação e Ano do Modelo são obrigatórios e devem ser maiores que zero" });
            }
                _context.Veiculos.Add(veiculo);
                await _context.SaveChangesAsync();
                return Ok(veiculo);
            
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            if (id != veiculo.ID)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Veiculos.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(veiculo);
            
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veiculos == null)
            {
                return Problem("Entity set 'DataContext.Veiculos'  is null.");
            }
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
            }
            
            await _context.SaveChangesAsync();
            return Ok(veiculo);
        }

        private bool VeiculoExists(int id)
        {
          return (_context.Veiculos?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario(int veiculoId, int usuarioId)
        {
            var veiculoUsuario = new VeiculoUsuarios
            {
                VeiculoID = veiculoId,
                UsuarioID = usuarioId
            };

            _context.VeiculoUsuarios.Add(veiculoUsuario);

            _context.SaveChanges();

            return Ok(veiculoUsuario);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUsuario(int veiculoID, int usuarioID)
        {
            var model = await _context.VeiculoUsuarios
                .Where(c => c.VeiculoID == veiculoID && c.UsuarioID == usuarioID)
                .FirstOrDefaultAsync();

            if (model == null) return NotFound();

            _context.VeiculoUsuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
