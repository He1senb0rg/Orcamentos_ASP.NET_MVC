using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Infrastructure;
using Orcamentos.Models;

namespace Orcamentos.Controllers
{
    public class ProfileLevelsController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public ProfileLevelsController(DataContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        // GET: ProfileLevels
        public async Task<IActionResult> Index()
        {
            List<ProfileLevel> listaProfileLevels = _context.profileLevels.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return View(listaProfileLevels);
        }

        // GET: ProfileLevels/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Ativo")] ProfileLevel profileLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profileLevel);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Nivel de Perfil criado com sucesso");
                return RedirectToAction(nameof(Index));
            }
            return View(profileLevel);
        }

        // GET: ProfileLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ativo")] ProfileLevel profileLevel)
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
                    _toastNotification.AddSuccessToastMessage("Nivel de Perfil editado com sucesso");
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.profileLevels == null)
            {
                return Problem("Entity set 'DataContext.profileLevels'  is null.");
            }
            var profileLevel = await _context.profileLevels.FindAsync(id);
            if (profileLevel != null)
            {
                if (profileLevel.Id != 1)
                {
                    profileLevel.Ativo = false;

                    List<Profile> listaProfile =
                    _context.profiles
                    .Where(d => d.profileLevelId == profileLevel.Id)
                    .ToList();

                    foreach (var profile in listaProfile)
                    {
                        profile.profileLevelId = 1;
                    }

                    _context.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Nivel de Perfil eliminado com sucesso");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Não é possivel eliminar este Nivel de Perfil");
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileLevelExists(int id)
        {
            return (_context.profileLevels?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult UpdateProfileLevels([FromBody] List<ProfileLevel> profileLevels)
        {

            foreach (var profileLevel in profileLevels)
            {
                _context.Update(profileLevel);
            }
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Tabela guardada com sucesso");

            return Ok(profileLevels);
        }

        public IActionResult GetTableProfileLevels()
        {
            List<ProfileLevel> data = _context.profileLevels.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Ok(data);
        }

        [HttpPost]
        public async Task<JsonResult> deleteOnExcelAsync([FromBody] int idProfileLevel)
        {

            var profileLevel = await _context.profileLevels.FindAsync(idProfileLevel);
            if (profileLevel.Id != 1)
            {
                profileLevel.Ativo = false;

                List<Profile> listaProfile =
                    _context.profiles
                    .Where(d => d.profileLevelId == profileLevel.Id)
                    .ToList();

                foreach (var profile in listaProfile)
                {
                    profile.profileLevelId = 1;
                }

                _context.Update(profileLevel);

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Nivel de Perfil eliminado com sucesso");


            }
            else
            {
                _toastNotification.AddErrorToastMessage("Não é possivel eliminar este Nivel de Perfil");
            }

            List<ProfileLevel> data = _context.profileLevels.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(data);
        }

		public JsonResult getProfileLevels()
		{

			var linhas2 = _context.profileLevels.Where(d => d.Ativo == true).Select(o => new {
				o.Id,
				o.Name,
			}).ToList();


			return Json(linhas2);


		}

		[HttpPost]
        public JsonResult AddNewRow(ProfileLevel novaLinha)
        {

            _context.profileLevels.Add(novaLinha);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Linha adicionada");

            var linhas = _context.profileLevels.Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();

            return Json(linhas);
        }
    }
}
