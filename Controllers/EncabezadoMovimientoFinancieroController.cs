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
    public class EncabezadoMovimientoFinancieroController : Controller
    {
        private readonly ManejadorPresupuestoContext _context;

        public EncabezadoMovimientoFinancieroController(ManejadorPresupuestoContext context)
        {
            _context = context;
        }

        // GET: EncabezadoMovimientoFinanciero
        public async Task<IActionResult> Index()
        {
            var manejadorPresupuestoContext = _context.EncabezadoMovimientosFinanciero.OrderByDescending(x=>x.FechaMovimiento).Include(e => e.Estado);            
            return View(await manejadorPresupuestoContext.ToListAsync());
        }

        // GET: EncabezadoMovimientoFinanciero/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encabezadoMovimientoFinanciero = await _context.EncabezadoMovimientosFinanciero
                .Include(e => e.Estado)
                .Include(e => e.DetalleMovimientoGastos) 
                .Include(e => e.DetalleMovimientoIngresos)                                       
                .FirstOrDefaultAsync(m => m.Id == id);

            if (encabezadoMovimientoFinanciero == null)
            {
                return NotFound();
            }

            foreach (var gasto in encabezadoMovimientoFinanciero.DetalleMovimientoGastos)
            {
                 var detalleMovimientoGasto = await _context.DetalleMovimientoGastos                
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == gasto.Id);

                 if (detalleMovimientoGasto == null)
                {
                    return NotFound();
                }

                gasto.ItemCategoria = detalleMovimientoGasto.ItemCategoria;
            }

            foreach (var ingreso in encabezadoMovimientoFinanciero.DetalleMovimientoIngresos)
            {
                 var DetalleMovimientoIngreso = await _context.DetalleMovimientoIngresos                
                .Include(d => d.ItemCategoria)
                .FirstOrDefaultAsync(m => m.Id == ingreso.Id);

                 if (DetalleMovimientoIngreso == null)
                {
                    return NotFound();
                }

                ingreso.ItemCategoria = DetalleMovimientoIngreso.ItemCategoria;
            }

            return View(encabezadoMovimientoFinanciero);
        }

        // GET: EncabezadoMovimientoFinanciero/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado");
            return View();
        }

        // POST: EncabezadoMovimientoFinanciero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaMovimiento,Titulo,EstadoId")] EncabezadoMovimientoFinanciero encabezadoMovimientoFinanciero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encabezadoMovimientoFinanciero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", encabezadoMovimientoFinanciero.EstadoId);
            return View(encabezadoMovimientoFinanciero);
        }

        // GET: EncabezadoMovimientoFinanciero/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encabezadoMovimientoFinanciero = await _context.EncabezadoMovimientosFinanciero.FindAsync(id);
            if (encabezadoMovimientoFinanciero == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", encabezadoMovimientoFinanciero.EstadoId);
            return View(encabezadoMovimientoFinanciero);
        }

        // POST: EncabezadoMovimientoFinanciero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FechaMovimiento,Titulo,EstadoId,Id")] EncabezadoMovimientoFinanciero encabezadoMovimientoFinanciero)
        {
            if (id != encabezadoMovimientoFinanciero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encabezadoMovimientoFinanciero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncabezadoMovimientoFinancieroExists(encabezadoMovimientoFinanciero.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "Id", "NombreEstado", encabezadoMovimientoFinanciero.EstadoId);
            return View(encabezadoMovimientoFinanciero);
        }

        // GET: EncabezadoMovimientoFinanciero/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encabezadoMovimientoFinanciero = await _context.EncabezadoMovimientosFinanciero
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encabezadoMovimientoFinanciero == null)
            {
                return NotFound();
            }

            return View(encabezadoMovimientoFinanciero);
        }

        // POST: EncabezadoMovimientoFinanciero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var encabezadoMovimientoFinanciero = await _context.EncabezadoMovimientosFinanciero.FindAsync(id);
            _context.EncabezadoMovimientosFinanciero.Remove(encabezadoMovimientoFinanciero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncabezadoMovimientoFinancieroExists(string id)
        {
            return _context.EncabezadoMovimientosFinanciero.Any(e => e.Id == id);
        }
    }
}
