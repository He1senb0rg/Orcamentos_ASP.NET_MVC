using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Helpers;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class OrcamentoNomesController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public OrcamentoNomesController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: OrcamentoNomes
        public async Task<IActionResult> Index()
        {
            List<OrcamentoNome> listaOrcamentoNomes = _context.orcamentoNomes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return View(listaOrcamentoNomes);
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

            List<Orcamento> listaOrcamentos = 
                _context.orcamentos
                .Include(o => o.OrcamentoNome)
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .Include(o => o.RevenueType)
                .Where(d => d.orcamentoNomeId == orcamentoNome.Id)
                .ToList();

            IEnumerable<SelectListItem> profilesList = DBHelper.FillProfiles(_context);
            ViewBag.profilesList = profilesList;

            IEnumerable<SelectListItem> revenueTypesList = DBHelper.FillRevenueTypes(_context);
            ViewBag.revenueTypesList = revenueTypesList;

            IEnumerable<SelectListItem> businessUnitsList = DBHelper.FillBu(_context);
            ViewBag.businessUnitsList = businessUnitsList;

            IEnumerable<SelectListItem> orcamentosNomesList = DBHelper.FillOrcamentosNomes(_context);
            ViewBag.orcamentosNomesList = orcamentosNomesList;

            TempData["listaOrcamentos"] = listaOrcamentos;

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
        public async Task<IActionResult> Create([Bind("Id,Nome,CreatedBy,Ativo,ProposalNumber")] OrcamentoNome orcamentoNome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orcamentoNome);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Nome de Orçamento criado com sucesso");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CreatedBy,Ativo,ProposalNumber")] OrcamentoNome orcamentoNome)
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
                    _toastNotification.AddSuccessToastMessage("Proposta editada com sucesso");
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
                if (orcamentoNome.Id != 1)
                {
                    orcamentoNome.Ativo = false;

                    List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.orcamentoNomeId == orcamentoNome.Id)
                    .ToList();

                    foreach (var orcamento in listaOrcamentos)
                    {
                        orcamento.orcamentoNomeId = 1;
                    }

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Proposta eliminada com sucesso");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Não é possivel eliminar esta Proposta");
                }         
                //_context.orcamentoNomes.Remove(orcamentoNome);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoNomeExists(int id)
        {
          return (_context.orcamentoNomes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GetTableOrcamentosAsync(int id)
        {
            var orcamentoNome = await _context.orcamentoNomes.FindAsync(id);

            var data = _context.orcamentos.Where(d => d.Ativo == true)
                .Include(o => o.OrcamentoNome)
                .Include(o => o.BusinessUnit)
                .Include(o => o.Profile)
                .Include(o => o.RevenueType)
                .Select(o => new {
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
				}).Where(o => o.OrcamentoNomeId == orcamentoNome.Id).ToList();



            return Ok(data);
        }

        [HttpPost]
        public IActionResult UpdateOrcamentoNomes([FromBody] List<OrcamentoNome> orcamentoNomes)
        {

            foreach (var orcamento in orcamentoNomes)
            {
                _context.Update(orcamento);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(orcamentoNomes);
        }

        public IActionResult GetTableOrcamentoNomes()
        {
            List<OrcamentoNome> data = _context.orcamentoNomes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Ok(data);
        }

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelAsync([FromBody] int idOrcamentoNome)
        {

            var orcamentoNome = await _context.orcamentoNomes.FindAsync(idOrcamentoNome);
            if (orcamentoNome.Id != 1)
            {
                orcamentoNome.Ativo = false;

                List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.orcamentoNomeId == orcamentoNome.Id)
                    .ToList();

                foreach (var orcamento in listaOrcamentos)
                {
                    orcamento.orcamentoNomeId = 1;
                }

                _context.Update(orcamentoNome);

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Proposta eliminada com sucesso");


            }
            else
            {
                _toastNotification.AddErrorToastMessage("Não é possivel eliminar esta Proposta");
            }

            List<OrcamentoNome> data = _context.orcamentoNomes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(data);
        }

        [HttpPost]
        public JsonResult AddNewRow(OrcamentoNome novaLinha)
        {

            _context.orcamentoNomes.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.orcamentoNomes.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(linhas);
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

            return Ok(linhas);
        }

        //public IActionResult GetTableOrcamentos()
        //{
        //    List<Orcamento> data = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).ToList();

        //    return Ok(data);
        //}

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelOrcamentoAsync([FromBody] int idOrcamento)
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
        public JsonResult AddNewRowOrcamentos(Orcamento novaLinha)
        {
            //var id = (int)ViewBag.orcamentoNomeId;

            var id = novaLinha.orcamentoNomeId;
            

            _context.orcamentos.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(o => o.Ativo == true).Where(o => o.orcamentoNomeId == id).Select(o => new {
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

            return Json(linhas);
        }

		public JsonResult getOrcamentoNomes()
		{

			var linhas2 = _context.orcamentoNomes.Where(d => d.Ativo == true).Select(o => new {
				o.Id,
				o.Nome,
				o.ProposalNumber,
                o.CreatedBy
			}).ToList();


			return Json(linhas2);


		}


		[HttpPost]
        public JsonResult getOrcamentos ([FromBody] int id)
        {

            var linhas2 = _context.orcamentos.Include(o => o.OrcamentoNome).Include(o => o.BusinessUnit).Include(o => o.Profile).Include(o => o.RevenueType).Where(d => d.Ativo == true).Where(d => d.orcamentoNomeId == id).Select(o => new {

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
