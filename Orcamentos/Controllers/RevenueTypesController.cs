using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class RevenueTypesController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public RevenueTypesController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: RevenueTypes
        public async Task<IActionResult> Index()
        {
            List<RevenueType> revenueTypes = _context.revenueTypes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

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
                _toastNotification.AddSuccessToastMessage("Sub Família criada com sucesso");
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
                    _toastNotification.AddSuccessToastMessage("Sub Família editada com sucesso");
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
                if(revenueType.Id != 1)
                {
                    revenueType.Ativo = false;

                    List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.revenueTypeId == revenueType.Id)
                    .ToList();

                    foreach (var orcamento in listaOrcamentos)
                    {
                        orcamento.revenueTypeId = 1;
                    }

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Sub Família eliminada com sucesso");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Não é possivel eliminar esta Sub Família");
                }
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
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(revenueTypes);
        }

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelAsync([FromBody] int idRevenueType)
        {

            var revenueType = await _context.revenueTypes.FindAsync(idRevenueType);
            if (revenueType.Id != 1)
            {
                revenueType.Ativo = false;

                List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.revenueTypeId == revenueType.Id)
                    .ToList();

                foreach (var orcamento in listaOrcamentos)
                {
                    orcamento.revenueTypeId = 1;
                }

                _context.Update(revenueType);

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Sub Família eliminada com sucesso");

                
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Não é possivel eliminar esta Sub Família");
            }

            List<RevenueType> data = _context.revenueTypes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(data);
        }

        public IActionResult GetTableRevenueTypes()
        {
            List<RevenueType> data = _context.revenueTypes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Ok(data);
        }

		public JsonResult getSubFamilia()
		{

			var linhas2 = _context.revenueTypes.Where(d => d.Ativo == true).Select(o => new {
				o.Id,
				o.Nome,
				o.Tipo,
			}).ToList();


			return Json(linhas2);


		}


		[HttpPost]
        public JsonResult AddNewRow(RevenueType novaLinha)
        {

            _context.revenueTypes.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.revenueTypes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(linhas);
        }

    }
}
