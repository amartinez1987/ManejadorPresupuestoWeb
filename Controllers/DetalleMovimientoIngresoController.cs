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
    public class DetalleMovimientoIngresoController : Controller
    {
        private readonly ManejadorPresupuestoContext _context;

        public DetalleMovimientoIngresoController(ManejadorPresupuestoContext context)
        {
            _context = context;
        }

        // GET: DetalleMovimientoIngreso
        public async Task<IActionResult> Index()
        {
            var manejadorPresupuestoContext = _context.DetalleMovimientoIngresos.Include(d => d.EncabezadoMovimientoFinanciero).Include(d => d.Estado).Include(d => d.ItemCategoria);
            return View(await manejadorPresupuestoContext.ToListAsync());
        }

        // GET: DetalleMovimientoIngreso/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoIngreso = await _context.DetalleMovimientoIngresos
                .Include(d => d.EncabezadoMovimientoFinanciero)
                .Include(d => d.Estado)
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimientoIngreso == null)
            {
                return NotFound();
            }

            return View(detalleMovimientoIngreso);
        }

        // GET: DetalleMovimientoIngreso/Create
        [Route("DetalleMovimientoIngreso/Create")]
        public IActionResult Create()
        {
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo");
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado");
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem");
            return View();
        }

        // GET: DetalleMovimientoIngreso/Create
        [Route("DetalleMovimientoIngreso/Create/{EncabezadoMovimientoFinancieroId}")]
        public IActionResult Create(string EncabezadoMovimientoFinancieroId)
        {
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado");
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem");
            return View();
        }

        // POST: DetalleMovimientoIngreso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DetalleMovimientoIngreso/Create")]
        [Route("DetalleMovimientoIngreso/Create/{EncabezadoMovimientoFinancieroId}")]
        public async Task<IActionResult> Create([Bind("ItemCategoriaId,EstadoId,EncabezadoMovimientoFinancieroId,ValorMovimiento")] DetalleMovimientoIngreso detalleMovimientoIngreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleMovimientoIngreso);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","EncabezadoMovimientoFinanciero", new {@id=detalleMovimientoIngreso.EncabezadoMovimientoFinancieroId});
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoIngreso.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoIngreso.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoIngreso.ItemCategoriaId);
            return View(detalleMovimientoIngreso);
        }

        // GET: DetalleMovimientoIngreso/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoIngreso = await _context.DetalleMovimientoIngresos.FindAsync(id);
            if (detalleMovimientoIngreso == null)
            {
                return NotFound();
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoIngreso.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoIngreso.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoIngreso.ItemCategoriaId);
            return View(detalleMovimientoIngreso);
        }

        // POST: DetalleMovimientoIngreso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemCategoriaId,EstadoId,EncabezadoMovimientoFinancieroId,ValorMovimiento,Id")] DetalleMovimientoIngreso detalleMovimientoIngreso)
        {
            if (id != detalleMovimientoIngreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleMovimientoIngreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleMovimientoIngresoExists(detalleMovimientoIngreso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               return RedirectToAction("Details","EncabezadoMovimientoFinanciero", new {@id=detalleMovimientoIngreso.EncabezadoMovimientoFinancieroId});
            }
            ViewData["EncabezadoMovimientoFinancieroId"] = new SelectList(_context.EncabezadoMovimientosFinanciero, "Id", "Titulo", detalleMovimientoIngreso.EncabezadoMovimientoFinancieroId);
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", detalleMovimientoIngreso.EstadoId);
            ViewData["ItemCategoriaId"] = new SelectList(_context.ItemsCategoria, "Id", "NombreItem", detalleMovimientoIngreso.ItemCategoriaId);
            return View(detalleMovimientoIngreso);
        }

        // GET: DetalleMovimientoIngreso/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleMovimientoIngreso = await _context.DetalleMovimientoIngresos
                .Include(d => d.EncabezadoMovimientoFinanciero)
                .Include(d => d.Estado)
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleMovimientoIngreso == null)
            {
                return NotFound();
            }

            return View(detalleMovimientoIngreso);
        }

        // POST: DetalleMovimientoIngreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var detalleMovimientoIngreso = await _context.DetalleMovimientoIngresos.FindAsync(id);
            _context.DetalleMovimientoIngresos.Remove(detalleMovimientoIngreso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleMovimientoIngresoExists(string id)
        {
            return _context.DetalleMovimientoIngresos.Any(e => e.Id == id);
        }
    }
}
