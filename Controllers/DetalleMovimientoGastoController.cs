using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManejadorPresupuesto2.Models;

namespace ManejadorPresupuesto2.Controllers
{
    public class DetalleMovimientoGastoController : Controller
    {
        private readonly ManejadorPresupuestoContext _context;

        public DetalleMovimientoGastoController(ManejadorPresupuestoContext context)
        {
            _context = context;
        }

        // GET: DetalleMovimientoGasto
        public async Task<IActionResult> Index()
        {
            var manejadorPresupuestoContext = _context.DetalleMovimientoGastos.Include(d => d.EncabezadoMovimientoFinanciero).Include(d => d.Estado).Include(d => d.ItemCategoria);
            return View(await manejadorPresupuestoContext.ToListAsync());
        }

        // GET: DetalleMovimientoGasto/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoGasto = await _context.DetalleMovimientoGastos
                .Include(d => d.EncabezadoMovimientoFinanciero)
                .Include(d => d.Estado)
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimientoGasto == null)
            {
                return NotFound();
            }

            return View(detalleMovimientoGasto);
        }
        
        [Route("DetalleMovimientoGasto/Create")]
        public IActionResult Create()
        {
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem");
            return View();
        }
        
        // GET: DetalleMovimientoGasto/Create
        [Route("DetalleMovimientoGasto/Create/{EncabezadoMovimientoFinancieroId}")]
        public IActionResult Create(string EncabezadoMovimientoFinancieroId)
        {
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "Id");
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem");
            ViewData["EncabezadoId"] = EncabezadoMovimientoFinancieroId;
            return View();
        }

        // POST: DetalleMovimientoGasto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DetalleMovimientoGasto/Create")]
        [Route("DetalleMovimientoGasto/Create/{EncabezadoMovimientoFinancieroId}")]
        public async Task<IActionResult> Create([Bind("ItemCategoriaId,EstadoId,EncabezadoMovimientoFinancieroId,ValorMovimiento,Descripcion,FechaCreacion")] DetalleMovimientoGasto detalleMovimientoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleMovimientoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","EncabezadoMovimientoFinanciero", new {@id=detalleMovimientoGasto.EncabezadoMovimientoFinancieroId});
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoGasto.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoGasto.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoGasto.ItemCategoriaId);
            ViewData["EncabezadoId"] = detalleMovimientoGasto.EncabezadoMovimientoFinancieroId;
            return View(detalleMovimientoGasto);
        }

        // GET: DetalleMovimientoGasto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoGasto = await _context.DetalleMovimientoGastos.FindAsync(id);
            if (detalleMovimientoGasto == null)
            {
                return NotFound();
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoGasto.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoGasto.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoGasto.ItemCategoriaId);
            return View(detalleMovimientoGasto);
        }

        // POST: DetalleMovimientoGasto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemCategoriaId,EstadoId,EncabezadoMovimientoFinancieroId,ValorMovimiento,Descripcion,FechaCreacion,Id")] DetalleMovimientoGasto detalleMovimientoGasto)
        {
            if (id != detalleMovimientoGasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleMovimientoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleMovimientoGastoExists(detalleMovimientoGasto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","EncabezadoMovimientoFinanciero", new {@id=detalleMovimientoGasto.EncabezadoMovimientoFinancieroId});
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoGasto.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoGasto.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoGasto.ItemCategoriaId);
            return View(detalleMovimientoGasto);
        }

        // GET: DetalleMovimientoGasto/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoGasto = await _context.DetalleMovimientoGastos
                .Include(d => d.EncabezadoMovimientoFinanciero)
                .Include(d => d.Estado)
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimientoGasto == null)
            {
                return NotFound();
            }

            return View(detalleMovimientoGasto);
        }

        // POST: DetalleMovimientoGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var detalleMovimientoGasto = await _context.DetalleMovimientoGastos.FindAsync(id);
            _context.DetalleMovimientoGastos.Remove(detalleMovimientoGasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleMovimientoGastoExists(string id)
        {
            return _context.DetalleMovimientoGastos.Any(e => e.Id == id);
        }
    }
}
