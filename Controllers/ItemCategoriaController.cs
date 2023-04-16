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
    public class ItemCategoriaController : Controller
    {
        private readonly ManejadorPresupuestoContext _context;

        public ItemCategoriaController(ManejadorPresupuestoContext context)
        {
            _context = context;
        }

        // GET: ItemCategoria
        public async Task<IActionResult> Index()
        {
            var manejadorPresupuestoContext = _context.ItemsCategoria.Include(i => i.Categoria);
            return View(await manejadorPresupuestoContext.ToListAsync());
        }

        // GET: ItemCategoria/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoria = await _context.ItemsCategoria
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCategoria == null)
            {
                return NotFound();
            }

            return View(itemCategoria);
        }

        // GET: ItemCategoria/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria");
            return View();
        }

        // POST: ItemCategoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreItem,CategoriaId")] ItemCategoria itemCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Id", itemCategoria.CategoriaId);
            return View(itemCategoria);
        }

        // GET: ItemCategoria/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoria = await _context.ItemsCategoria.FindAsync(id);
            if (itemCategoria == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", itemCategoria.CategoriaId);
            return View(itemCategoria);
        }

        // POST: ItemCategoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreItem,CategoriaId,Id")] ItemCategoria itemCategoria)
        {
            if (id != itemCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoriaExists(itemCategoria.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Id", itemCategoria.CategoriaId);
            return View(itemCategoria);
        }

        // GET: ItemCategoria/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoria = await _context.ItemsCategoria
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCategoria == null)
            {
                return NotFound();
            }

            return View(itemCategoria);
        }

        // POST: ItemCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var itemCategoria = await _context.ItemsCategoria.FindAsync(id);
            _context.ItemsCategoria.Remove(itemCategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoriaExists(string id)
        {
            return _context.ItemsCategoria.Any(e => e.Id == id);
        }
    }
}
