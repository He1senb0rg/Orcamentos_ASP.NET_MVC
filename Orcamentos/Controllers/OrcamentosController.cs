using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orcamentos.Helpers;
using Orcamentos.Infrastructure;
using Orcamentos.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NToastNotify;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Orcamentos.Controllers
{
    public class OrcamentosController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public OrcamentosController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Orcamentos
        public async Task<IActionResult> Index()
        {
            List<Orcamento> listaOrcamentos = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).ToList();

            IEnumerable<SelectListItem> profilesList = DBHelper.FillProfiles(_context);
            ViewBag.profilesList = profilesList;

            IEnumerable<SelectListItem> revenueTypesList = DBHelper.FillRevenueTypes(_context);
            ViewBag.revenueTypesList = revenueTypesList;

            IEnumerable<SelectListItem> businessUnitsList = DBHelper.FillBu(_context);
            ViewBag.businessUnitsList = businessUnitsList;

            IEnumerable<SelectListItem> orcamentosNomesList = DBHelper.FillOrcamentosNomes(_context);
            ViewBag.orcamentosNomesList = orcamentosNomesList;

            return View(listaOrcamentos);
        }

        // GET: Orcamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await _context.orcamentos
                .Include(o => o.OrcamentoNome)
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .Include(o => o.RevenueType)
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

            IEnumerable<SelectListItem> orcamentosNomesList = DBHelper.FillOrcamentosNomes(_context);
            ViewBag.orcamentosNomesList = orcamentosNomesList;

            var culture = Thread.CurrentThread.CurrentCulture;
            var decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;
            ViewBag.decimalSeparator = decimalSeparator;

            return View();
        }

        // POST: Orcamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,orcamentoNomeId,profileId,revenueTypeId,businessUnitId,Marca,TipoUni,Partnumb,modelo,SerialNumb,ProductName,Quantidade,UnitPrice,UnitCost,DescontoTabela,PrecoParcial,CustoTabela,CustoDesc1,CustoDesc2,CustoDesc3,TotalCost,TotalPrice,Margin,MG,Ativo, DelivaryDate,ExternalProvider")] Orcamento orcamento)
        {
            //decimal result;
            //bool isDecimal = decimal.TryParse(orcamento.UnitPrice.ToString(CultureInfo.InvariantCulture),NumberStyles.Any, CultureInfo.InvariantCulture, out result);      
            //if (!isDecimal)
            //{
            //    return StatusCode(500);
            //}
            //orcamento.UnitPrice = result;
            if (ModelState.IsValid)
            {
                if (orcamento.DelivaryDate.Contains('-'))
                {
                    var dates = orcamento.DelivaryDate.Split("-");

                    var dataCorrect = dates[2] + '/' + dates[1] + '/' + dates[0];

                    orcamento.DelivaryDate = dataCorrect;
                }
                
                _context.Add(orcamento);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Orçamento criado com sucesso");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuManagerId"] = new SelectList(_context.buManagers, "Id", "Id", orcamento.businessUnitId);
            ViewData["ProfileId"] = new SelectList(_context.profiles, "Id", "Id", orcamento.profileId);
            ViewData["OrcamentoNomeId"] = new SelectList(_context.orcamentoNomes, "Id", "Id", orcamento.orcamentoNomeId);
            ViewData["RevenueTypeId"] = new SelectList(_context.revenueTypes, "Id", "Id", orcamento.revenueTypeId);

            return View(orcamento);
        }

        // GET: Orcamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            IEnumerable<SelectListItem> orcamentosNomesList = DBHelper.FillOrcamentosNomes(_context);
            ViewBag.orcamentosNomesList = orcamentosNomesList;

            var culture = Thread.CurrentThread.CurrentCulture;
            var decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;
            ViewBag.decimalSeparator = decimalSeparator;

            return View(orcamento);
        }

        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,orcamentoNomeId,profileId,revenueTypeId,businessUnitId,Marca,TipoUni,Partnumb,modelo,SerialNumb,ProductName,Quantidade,UnitPrice,UnitCost,DescontoTabela,PrecoParcial,CustoTabela,CustoDesc1,CustoDesc2,CustoDesc3,TotalCost,TotalPrice,Margin,MG,Ativo,DelivaryDate,ExternalProvider")] Orcamento orcamento)
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
                    _toastNotification.AddSuccessToastMessage("Orçamento editado com sucesso");
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await _context.orcamentos
                .Include(o => o.OrcamentoNome)
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .Include(o => o.RevenueType)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
                _toastNotification.AddSuccessToastMessage("Orçamento eliminado com sucesso");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoExists(int id)
        {
            return (_context.orcamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult UpdateOrcamentos([FromBody] List<Orcamento> orcamentos)
        {

            foreach (var orcamento in orcamentos)
            {
                _context.Update(orcamento);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(orcamentos);
        }

        public IActionResult GetTableOrcamentos()
        {
            List<Orcamento> data = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).ToList();

            return Ok(data);
        }

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelAsync([FromBody] int idOrcamento)
        {

            var orcamento = await _context.orcamentos.FindAsync(idOrcamento);

            orcamento.Ativo = false;

            _context.Update(orcamento);

            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Orçamento eliminado com sucesso");

            var data = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).Select(o => new {
                o.Id,
                OrcamentoNomeId = o.orcamentoNomeId,
                OrcamentoName = o.OrcamentoNome.Nome,
                ProfileId = o.profileId,
                ProfileName = o.Profile.Name,
                RevenueTypeId = o.revenueTypeId,
                RevenueTypeName = o.RevenueType.Nome,
                BusinessUnitId = o.businessUnitId,
                BusinessUnitName = o.BusinessUnit.Name,
                o.Marca,
                o.TipoUni,
                o.Partnumb,
                o.modelo,
                o.SerialNumb,
                o.ProductName,
                o.Quantidade,
                o.UnitPrice,
                o.UnitCost,
                o.DescontoTabela,
                o.PrecoParcial,
                o.CustoTabela,
                o.CustoDesc1,
                o.CustoDesc2,
                o.CustoDesc3,
                o.TotalCost,
                o.TotalPrice,
                o.Margin,
                o.MG,
                o.Ativo,
                o.DelivaryDate,
                o.ExternalProvider
            }).ToList();

            return Json(data);
        }

        [HttpPost]
        public JsonResult AddNewRow(Orcamento novaLinha)
        {

            _context.orcamentos.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).Select(o => new {
                o.Id,
                OrcamentoNomeId = o.orcamentoNomeId,
                OrcamentoName = o.OrcamentoNome.Nome,
                ProfileId = o.profileId,
                ProfileName = o.Profile.Name,
                RevenueTypeId = o.revenueTypeId,
                RevenueTypeName = o.RevenueType.Nome,
                BusinessUnitId = o.businessUnitId,
                BusinessUnitName = o.BusinessUnit.Name,
                o.Marca,
                o.TipoUni,
                o.Partnumb,
                o.modelo,
                o.SerialNumb,
                o.ProductName,
                o.Quantidade,
                o.UnitPrice,
                o.UnitCost,
                o.DescontoTabela,
                o.PrecoParcial,
                o.CustoTabela,
                o.CustoDesc1,
                o.CustoDesc2,
                o.CustoDesc3,
                o.TotalCost,
                o.TotalPrice,
                o.Margin,
                o.MG,
                o.Ativo,
				o.DelivaryDate,
				o.ExternalProvider
			}).ToList();

            ViewBag.countOrcamentos = linhas.Count;

            return Json(linhas);


        }

		public JsonResult getOrcamentos ()
		{

			var linhas2 = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).Select(o => new {

				ProposalNumber = o.OrcamentoNome.ProposalNumber,
				Sequencia = o.Id * 10,
				o.Partnumb,
				o.ProductName,
				o.Quantidade,
				o.DescontoTabela,
				o.Marca,
				o.TotalPrice,
				o.TotalCost,
				o.DelivaryDate,
				SubFamilia = o.RevenueType.Nome,
				o.ExternalProvider,

			}).ToList();


			return Json(linhas2);


		}



	}
}
