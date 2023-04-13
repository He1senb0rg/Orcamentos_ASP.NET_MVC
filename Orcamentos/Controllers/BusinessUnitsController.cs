using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orcamentos.Infrastructure;
using Orcamentos.Models;
using Orcamentos.Helpers;

namespace Orcamentos.Controllers
{
    public class BusinessUnitsController : Controller
    {
        private readonly DataContext _context;

        public BusinessUnitsController(DataContext context)
        {
            _context = context;
        }

        // GET: BusinessUnits
        public async Task<IActionResult> Index()
        {
            List<BusinessUnit> listaBu = _context.businessUnits.Include(p => p.BuManager).ToList();
            
            return View(listaBu);
            // return _context.businessUnits != null ? 
            //             View(await _context.businessUnits.ToListAsync()) :
            //             Problem("Entity set 'DataContext.businessUnits'  is null.");
        }

        // GET: BusinessUnits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.businessUnits == null)
            {
                return NotFound();
            }

            var businessUnit = await _context.businessUnits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessUnit == null)
            {
                return NotFound();
            }

            return View(businessUnit);
        }

        // GET: BusinessUnits/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> bumList = DBHelper.FillBuManagers(_context);
            ViewBag.bumList = bumList;

            return View();
        }

        // POST: BusinessUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,buManagerId,Ativo")] BusinessUnit businessUnit)
        {
            if (ModelState.IsValid)
            {
                businessUnit.Id = Guid.NewGuid();
                _context.Add(businessUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessUnit);
        }

        // GET: BusinessUnits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.businessUnits == null)
            {
                return NotFound();
            }

            var businessUnit = await _context.businessUnits.FindAsync(id);
            if (businessUnit == null)
            {
                return NotFound();
            }
            return View(businessUnit);
        }

        // POST: BusinessUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,BumId,Ativo")] BusinessUnit businessUnit)
        {
            if (id != businessUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessUnitExists(businessUnit.Id))
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
            return View(businessUnit);
        }

        // GET: BusinessUnits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.businessUnits == null)
            {
                return NotFound();
            }

            var businessUnit = await _context.businessUnits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessUnit == null)
            {
                return NotFound();
            }

            return View(businessUnit);
        }

        // POST: BusinessUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.businessUnits == null)
            {
                return Problem("Entity set 'DataContext.businessUnits'  is null.");
            }
            var businessUnit = await _context.businessUnits.FindAsync(id);
            if (businessUnit != null)
            {
                businessUnit.Ativo = false;
                _context.SaveChanges();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessUnitExists(Guid id)
        {
          return (_context.businessUnits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
