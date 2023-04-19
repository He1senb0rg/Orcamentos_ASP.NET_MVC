﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class RevenueTypesController : Controller
    {
        private readonly DataContext _context;

        public RevenueTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: RevenueTypes
        public async Task<IActionResult> Index()
        {
            List<RevenueType> revenueTypes = _context.revenueTypes.ToList();

           return View(revenueTypes);
        }

        // GET: RevenueTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.revenueTypes == null)
            {
                return NotFound();
            }

            var revenueType = await _context.revenueTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenueType == null)
            {
                return NotFound();
            }

            return View(revenueType);
        }

        // GET: RevenueTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RevenueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Tipo,Ativo")] RevenueType revenueType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revenueType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revenueType);
        }

        // GET: RevenueTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.revenueTypes == null)
            {
                return NotFound();
            }

            var revenueType = await _context.revenueTypes.FindAsync(id);
            if (revenueType == null)
            {
                return NotFound();
            }
            return View(revenueType);
        }

        // POST: RevenueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Tipo,Ativo")] RevenueType revenueType)
        {
            if (id != revenueType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revenueType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevenueTypeExists(revenueType.Id))
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
            return View(revenueType);
        }

        // GET: RevenueTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.revenueTypes == null)
            {
                return NotFound();
            }

            var revenueType = await _context.revenueTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenueType == null)
            {
                return NotFound();
            }

            return View(revenueType);
        }

        // POST: RevenueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.revenueTypes == null)
            {
                return Problem("Entity set 'DataContext.revenueTypes'  is null.");
            }
            //RevenueType revenueType = db.Parts.Where(c => c.id == model.PartId).FirstOrDefault();
            var revenueType = await _context.revenueTypes.FindAsync(id);
            if (revenueType != null)
            {
                revenueType.Ativo = false;
                _context.SaveChanges();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevenueTypeExists(int id)
        {
            return (_context.revenueTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public IActionResult UpdateRevenueTypes([FromBody] List<RevenueType> revenueTypes)
        {

            foreach (var revenueType in revenueTypes)
            {
                _context.Update(revenueType);
            }
            _context.SaveChanges();

            return Ok(revenueTypes);
        }

        public IActionResult GetTableRevenueTypes()
        {
            List<RevenueType> data = _context.revenueTypes.ToList();

            return Ok(data);
        }

    }
}
