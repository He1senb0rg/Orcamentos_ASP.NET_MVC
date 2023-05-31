using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Helpers;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public ProfilesController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            List<Profile> listaProfiles = _context.profiles.Include(o => o.ProfileLevel).Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            IEnumerable<SelectListItem> profileLevelsList = DBHelper.FillProfileLevels(_context);
            ViewBag.profileLevelsList = profileLevelsList;

            return View(listaProfiles);
            //var dataContext = _context.profiles.Include(p => p.ProfileLevel);
            //return View(await dataContext.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
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
                _context.Add(profile);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Perfil criado com sucesso");
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileLevelId"] = new SelectList(_context.profileLevels, "Id", "Id", profile.profileLevelId);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,profileLevelId,Ativo")] Profile profile)
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
                    _toastNotification.AddSuccessToastMessage("Perfil editado com sucesso");
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.profiles == null)
            {
                return Problem("Entity set 'DataContext.profiles'  is null.");
            }
            var profile = await _context.profiles.FindAsync(id);
            if (profile != null)
            {
                if (profile.Id != 1)
                {
                    profile.Ativo = false;

                    List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.profileId == profile.Id)
                    .ToList();

                    foreach (var orcamento in listaOrcamentos)
                    {
                        orcamento.profileId = 1;
                    }

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Perfil eliminado com sucesso");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Não é possivel eliminar este Perfil");
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return (_context.profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult UpdateProfiles([FromBody] List<Profile> profiles)
        {

            foreach (var profile in profiles)
            {
                _context.Update(profile);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(profiles);
        }

        public IActionResult GetTableProfiles()
        {
            List<Profile> data = _context.profiles.Include(o => o.ProfileLevel).Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Ok(data);
        }

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelAsync([FromBody] int idProfile)
        {

            var profile = await _context.profiles.FindAsync(idProfile);
            if (profile.Id != 1)
            {
                profile.Ativo = false;

                List<Orcamento> listaOrcamentos =
                    _context.orcamentos
                    .Include(o => o.OrcamentoNome)
                    .Include(o => o.BusinessUnit)
                    .Include(o => o.Profile)
                    .Include(o => o.RevenueType)
                    .Where(d => d.profileId == profile.Id)
                    .ToList();

                foreach (var orcamento in listaOrcamentos)
                {
                    orcamento.profileId = 1;
                }

                _context.Update(profile);

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Perfil eliminado com sucesso");


            }
            else
            {
                _toastNotification.AddErrorToastMessage("Não é possivel eliminar este Perfil");
            }

            var data = _context.profiles.Where(d => d.Ativo == true).Where(d => d.Id != 1).Select(o => new {
                o.Id,
                o.Name,
                ProfileLevelId = o.profileLevelId,
                ProfileLevelName = o.ProfileLevel.Name,
                o.Ativo
            }).ToList();

            return Json(data);
        }

        public JsonResult getProfiles()
        {

            var linhas2 = _context.profiles.Where(d => d.Ativo == true).Select(o => new {
                o.Id,
                o.Name,
                ProfileLevelId = o.profileLevelId,
                ProfileLevelName = o.ProfileLevel.Name,

            }).ToList();


            return Json(linhas2);


        }

        [HttpPost]
        public JsonResult AddNewRow(Profile novaLinha)
        {

            _context.profiles.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.profiles.Where(d => d.Ativo == true).Where(d => d.Id != 1).Select(o => new {
                o.Id,
                o.Name,
                ProfileLevelId = o.profileLevelId,
                ProfileLevelName = o.ProfileLevel.Name,
                o.Ativo
            }).ToList();

            return Json(linhas);
        }
    }
}
