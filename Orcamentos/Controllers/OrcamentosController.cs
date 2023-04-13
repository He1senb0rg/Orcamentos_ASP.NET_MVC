﻿using System;
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
    public class OrcamentosController : Controller
    {
        private readonly DataContext _context;

        public OrcamentosController(DataContext context)
        {
            _context = context;
        }

        // GET: Orcamentos
        public async Task<IActionResult> Index()
        {
            List<Orcamento> listaOrcamentos = _context.orcamentos.Include(o => o.BusinessUnit).Include(o => o.Profile).ToList();
            return View(listaOrcamentos);
        }

        // GET: Orcamentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await _context.orcamentos
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamento == null)
            {
                return NotFound();
            }

            return View(orcamento);
        }

        // GET: Orcamentos/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> profilesList = DBHelper.FillProfiles(_context);
            ViewBag.profilesList = profilesList;

            IEnumerable<SelectListItem> revenueTypesList = DBHelper.FillRevenueTypes(_context);
            ViewBag.revenueTypesList = revenueTypesList;

            IEnumerable<SelectListItem> businessUnitsList = DBHelper.FillBu(_context);
            ViewBag.businessUnitsList = businessUnitsList;

            return View();
        }

        // POST: Orcamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,profileId,revenueTypeId,businessUnitId,Marca,TipoUni,Partnumb,modelo,SerialNumb,ProductName,Ativo")] Orcamento orcamento)
        {
            if (ModelState.IsValid)
            {
                orcamento.Id = Guid.NewGuid();
                _context.Add(orcamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuManagerId"] = new SelectList(_context.buManagers, "Id", "Id", orcamento.businessUnitId);
            ViewData["ProfileId"] = new SelectList(_context.profiles, "Id", "Id", orcamento.profileId);
            return View(orcamento);
        }

        // GET: Orcamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await _context.orcamentos.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> profilesList = DBHelper.FillProfiles(_context);
            ViewBag.profilesList = profilesList;

            IEnumerable<SelectListItem> revenueTypesList = DBHelper.FillRevenueTypes(_context);
            ViewBag.revenueTypesList = revenueTypesList;

            IEnumerable<SelectListItem> businessUnitsList = DBHelper.FillBu(_context);
            ViewBag.businessUnitsList = businessUnitsList;
            return View(orcamento);
        }

        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,profileId,revenueTypeId,businessUnitId,Marca,TipoUni,Partnumb,modelo,SerialNumb,ProductName,Ativo")] Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orcamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoExists(orcamento.Id))
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
            ViewData["BuManagerId"] = new SelectList(_context.buManagers, "Id", "Id", orcamento.businessUnitId);
            ViewData["ProfileId"] = new SelectList(_context.profiles, "Id", "Id", orcamento.profileId);
            return View(orcamento);
        }

        // GET: Orcamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await _context.orcamentos
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamento == null)
            {
                return NotFound();
            }

            return View(orcamento);
        }

        // POST: Orcamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.orcamentos == null)
            {
                return Problem("Entity set 'DataContext.orcamentos'  is null.");
            }
            var orcamento = await _context.orcamentos.FindAsync(id);
            if (orcamento != null)
            {
                orcamento.Ativo = false;
                _context.SaveChanges();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoExists(Guid id)
        {
          return (_context.orcamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
