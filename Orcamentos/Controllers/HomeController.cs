using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Orcamentos.Infrastructure;
using Orcamentos.Models;
using System.Diagnostics;

namespace Orcamentos.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILogger<HomeController> logger, DataContext context, IToastNotification toastNotification)
        {
            _logger = logger;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult Index()
        {
            int totalOrcamentos = _context.orcamentos.Count();
            decimal mediaPrecos = (decimal)_context.orcamentos.Average(o => o.TotalPrice);
            decimal mediaCustos = (decimal)_context.orcamentos.Average(o => o.TotalCost);


            ViewBag.TotalOrcamentos = totalOrcamentos;
            ViewBag.MediaPrecos = mediaPrecos;
            ViewBag.MediaCusto = mediaCustos;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}