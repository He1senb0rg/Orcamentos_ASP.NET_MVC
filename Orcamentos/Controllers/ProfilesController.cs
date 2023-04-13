using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orcamentos.Helpers;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly DataContext _context;

        public ProfilesController(DataContext context)
        {
            _context = context;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            List<Profile> listaProfiles = _context.profiles.Include(p => p.ProfileLevel).ToList();

            return View(listaProfiles);
            //var dataContext = _context.profiles.Include(p => p.ProfileLevel);
            //return View(await dataContext.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.profiles
                .Include(p => p.ProfileLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> profileLevelsList = DBHelper.FillProfileLevels(_context);
            ViewBag.profileLevelsList = profileLevelsList;

            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,profileLevelId,Ativo")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.Id = Guid.NewGuid();
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileLevelId"] = new SelectList(_context.profileLevels, "Id", "Id", profile.profileLevelId);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> profileLevelsList = DBHelper.FillProfileLevels(_context);
            ViewBag.profileLevelsList = profileLevelsList;

            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,profileLevelId,Ativo")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
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
            ViewData["ProfileLevelId"] = new SelectList(_context.profileLevels, "Id", "Id", profile.profileLevelId);
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.profiles
                .Include(p => p.ProfileLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.profiles == null)
            {
                return Problem("Entity set 'DataContext.profiles'  is null.");
            }
            var profile = await _context.profiles.FindAsync(id);
            if (profile != null)
            {
                profile.Ativo = false;
                _context.SaveChanges();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(Guid id)
        {
          return (_context.profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
