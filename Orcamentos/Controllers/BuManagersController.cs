using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class BuManagersController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public BuManagersController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: BuManagers
        public async Task<IActionResult> Index()
        {
            List<BuManager> listaBuManagers = _context.buManagers.ToList();

            return View(listaBuManagers);
        }

        // GET: BuManagers/Details/5
        public async Task<IActionResult> Details(int? id)
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
                _context.Add(buManager);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Gestor de Negócio criado com sucesso");
                return RedirectToAction(nameof(Index));
            }
            return View(buManager);
        }

        // GET: BuManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Ativo")] BuManager buManager)
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
                    _toastNotification.AddSuccessToastMessage("Gestor de Negócio editado com sucesso");
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                _toastNotification.AddSuccessToastMessage("Gestor de Negócio eliminado com sucesso");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuManagerExists(int id)
        {
            return (_context.buManagers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult UpdateBuManagers([FromBody] List<BuManager> buManagers)
        {

            foreach (var buManager in buManagers)
            {
                _context.Update(buManager);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(buManagers);
        }

        public IActionResult GetTableBuManagers()
        {
            List<BuManager> data = _context.buManagers.ToList();

            return Ok(data);
        }

        [HttpPost]
        public JsonResult AddNewRow(BuManager novaLinha)
        {

            _context.buManagers.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.buManagers.ToList();

            return Json(linhas);
        }
    }
}
