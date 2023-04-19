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
    public class OrcamentoNomesController : Controller
    {
        private readonly DataContext _context;

        public OrcamentoNomesController(DataContext context)
        {
            _context = context;
        }

        // GET: OrcamentoNomes
        public async Task<IActionResult> Index()
        {
              return _context.orcamentoNomes != null ? 
                          View(await _context.orcamentoNomes.ToListAsync()) :
                          Problem("Entity set 'DataContext.orcamentoNomes'  is null.");
        }

        // GET: OrcamentoNomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orcamentoNomes == null)
            {
                return NotFound();
            }

            var orcamentoNome = await _context.orcamentoNomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamentoNome == null)
            {
                return NotFound();
            }

            return View(orcamentoNome);
        }

        // GET: OrcamentoNomes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrcamentoNomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CreatedBy,Ativo")] OrcamentoNome orcamentoNome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orcamentoNome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orcamentoNome);
        }

        // GET: OrcamentoNomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.orcamentoNomes == null)
            {
                return NotFound();
            }

            var orcamentoNome = await _context.orcamentoNomes.FindAsync(id);
            if (orcamentoNome == null)
            {
                return NotFound();
            }
            return View(orcamentoNome);
        }

        // POST: OrcamentoNomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CreatedBy,Ativo")] OrcamentoNome orcamentoNome)
        {
            if (id != orcamentoNome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orcamentoNome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoNomeExists(orcamentoNome.Id))
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
            return View(orcamentoNome);
        }

        // GET: OrcamentoNomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orcamentoNomes == null)
            {
                return NotFound();
            }

            var orcamentoNome = await _context.orcamentoNomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamentoNome == null)
            {
                return NotFound();
            }

            return View(orcamentoNome);
        }

        // POST: OrcamentoNomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.orcamentoNomes == null)
            {
                return Problem("Entity set 'DataContext.orcamentoNomes'  is null.");
            }
            var orcamentoNome = await _context.orcamentoNomes.FindAsync(id);
            if (orcamentoNome != null)
            {
                _context.orcamentoNomes.Remove(orcamentoNome);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoNomeExists(int id)
        {
          return (_context.orcamentoNomes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
