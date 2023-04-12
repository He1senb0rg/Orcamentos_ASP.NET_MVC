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
    public class ProfileLevelsController : Controller
    {
        private readonly DataContext _context;

        public ProfileLevelsController(DataContext context)
        {
            _context = context;
        }

        // GET: ProfileLevels
        public async Task<IActionResult> Index()
        {
              return _context.profileLevels != null ? 
                          View(await _context.profileLevels.ToListAsync()) :
                          Problem("Entity set 'DataContext.profileLevels'  is null.");
        }

        // GET: ProfileLevels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.profileLevels == null)
            {
                return NotFound();
            }

            var profileLevel = await _context.profileLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileLevel == null)
            {
                return NotFound();
            }

            return View(profileLevel);
        }

        // GET: ProfileLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfileLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Quantidade")] ProfileLevel profileLevel)
        {
            if (ModelState.IsValid)
            {
                profileLevel.Id = Guid.NewGuid();
                _context.Add(profileLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profileLevel);
        }

        // GET: ProfileLevels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.profileLevels == null)
            {
                return NotFound();
            }

            var profileLevel = await _context.profileLevels.FindAsync(id);
            if (profileLevel == null)
            {
                return NotFound();
            }
            return View(profileLevel);
        }

        // POST: ProfileLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductName,Quantidade")] ProfileLevel profileLevel)
        {
            if (id != profileLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profileLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileLevelExists(profileLevel.Id))
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
            return View(profileLevel);
        }

        // GET: ProfileLevels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.profileLevels == null)
            {
                return NotFound();
            }

            var profileLevel = await _context.profileLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileLevel == null)
            {
                return NotFound();
            }

            return View(profileLevel);
        }

        // POST: ProfileLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.profileLevels == null)
            {
                return Problem("Entity set 'DataContext.profileLevels'  is null.");
            }
            var profileLevel = await _context.profileLevels.FindAsync(id);
            if (profileLevel != null)
            {
                _context.profileLevels.Remove(profileLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileLevelExists(Guid id)
        {
          return (_context.profileLevels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
