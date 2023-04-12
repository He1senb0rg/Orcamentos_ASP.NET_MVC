using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class BuManagersController : Controller
    {
        private readonly DataContext _context;

        public BuManagersController(DataContext context)
        {
            _context = context;
        }

        // GET: BuManagers
        public async Task<IActionResult> Index()
        {
              return _context.buManagers != null ? 
                          View(await _context.buManagers.ToListAsync()) :
                          Problem("Entity set 'DataContext.buManagers'  is null.");
        }

        // GET: BuManagers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.buManagers == null)
            {
                return NotFound();
            }

            var buManager = await _context.buManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buManager == null)
            {
                return NotFound();
            }

            return View(buManager);
        }

        // GET: BuManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BuManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] BuManager buManager)
        {
            if (ModelState.IsValid)
            {
                buManager.Id = Guid.NewGuid();
                _context.Add(buManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buManager);
        }

        // GET: BuManagers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.buManagers == null)
            {
                return NotFound();
            }

            var buManager = await _context.buManagers.FindAsync(id);
            if (buManager == null)
            {
                return NotFound();
            }
            return View(buManager);
        }

        // POST: BuManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Ativo")] BuManager buManager)
        {
            if (id != buManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuManagerExists(buManager.Id))
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
            return View(buManager);
        }

        // GET: BuManagers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.buManagers == null)
            {
                return NotFound();
            }

            var buManager = await _context.buManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buManager == null)
            {
                return NotFound();
            }

            return View(buManager);
        }

        // POST: BuManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.buManagers == null)
            {
                return Problem("Entity set 'DataContext.buManagers'  is null.");
            }
            var buManager = await _context.buManagers.FindAsync(id);
            if (buManager != null)
            {
                buManager.Ativo = false;
                _context.SaveChanges();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuManagerExists(Guid id)
        {
          return (_context.buManagers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
