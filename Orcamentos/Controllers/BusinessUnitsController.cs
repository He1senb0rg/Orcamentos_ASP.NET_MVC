using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Helpers;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class BusinessUnitsController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public BusinessUnitsController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: BusinessUnits
        public async Task<IActionResult> Index()
        {
            List<BusinessUnit> listaBu = _context.businessUnits.Include(p => p.BuManager).ToList();

            IEnumerable<SelectListItem> buManagersList = DBHelper.FillBuManagers(_context);
            ViewBag.buManagersList = buManagersList;

            return View(listaBu);
            // return _context.businessUnits != null ? 
            //             View(await _context.businessUnits.ToListAsync()) :
            //             Problem("Entity set 'DataContext.businessUnits'  is null.");
        }

        // GET: BusinessUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.businessUnits == null)
            {
                return NotFound();
            }

            var businessUnit = await _context.businessUnits
                .Include(p => p.BuManager)
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
                _context.Add(businessUnit);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Unidade de Negócio criada com sucesso");
                return RedirectToAction(nameof(Index));
            }
            return View(businessUnit);
        }

        // GET: BusinessUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            IEnumerable<SelectListItem> bumList = DBHelper.FillBuManagers(_context);
            ViewBag.bumList = bumList;

            return View(businessUnit);
        }

        // POST: BusinessUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,buManagerId,Ativo")] BusinessUnit businessUnit)
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
                    _toastNotification.AddSuccessToastMessage("Unidade de Negócio editada com sucesso");
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.businessUnits == null)
            {
                return NotFound();
            }

            var businessUnit = await _context.businessUnits
                .Include(p => p.BuManager)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                _toastNotification.AddSuccessToastMessage("Unidade de Negócio eliminada com sucesso");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessUnitExists(int id)
        {
            return (_context.businessUnits?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult UpdateBusinessUnits([FromBody] List<BusinessUnit> businessUnits)
        {

            foreach (var businessUnit in businessUnits)
            {
                _context.Update(businessUnit);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(businessUnits);
        }

        public IActionResult GetTableBusinessUnits()
        {
            List<BusinessUnit> data = _context.businessUnits.Include(o => o.BuManager).ToList();

            return Ok(data);
        }

        [HttpPost]
        public JsonResult AddNewRow(BusinessUnit novaLinha)
        {

            _context.businessUnits.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.businessUnits.Select(o => new {
                o.Id,
                o.Name,
                BuManagerId = o.buManagerId,
                BuManagerName = o.BuManager.Nome,
                o.Ativo
            }).ToList();

            return Json(linhas);
        }
    }
}
