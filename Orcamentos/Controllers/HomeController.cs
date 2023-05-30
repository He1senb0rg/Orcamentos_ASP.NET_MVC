using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Orcamentos.Infrastructure;
using Orcamentos.Models;
using Orcamentos.Models.ViewModels;
using System.Collections.Generic;
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

            List<int> ocorrencias = new List<int>();
            
            List<BusinessUnit> listaBu = _context.businessUnits.Include(p => p.BuManager).Where(d => d.Ativo == true).Where(d => d.Id != 1).ToList();
           
            foreach (var v in listaBu)
            {
                int count = 0;
                foreach (var item in _context.orcamentos)
                {
                    if (v.Id == item.businessUnitId)
                    {
                         count = count + 1;
                    }
                
                };

                ocorrencias.Add(count);

            };


            int totalOrcamentos = 0;
            decimal mediaPrecos = 0.0m;
            decimal mediaCustos = 0.0m;

            // Verificar se existem orçamentos ativos na base de dados
            var orcamentosAtivos = _context.orcamentos.Where(o => o.Ativo == true);
            if (orcamentosAtivos.Any())
            {
                totalOrcamentos = orcamentosAtivos.Count();
                mediaPrecos = (decimal)orcamentosAtivos.Average(o => o.TotalPrice);
                mediaCustos = (decimal)orcamentosAtivos.Average(o => o.TotalCost);
            }

            ViewBag.TotalOrcamentos = totalOrcamentos;
            ViewBag.MediaPrecos = mediaPrecos;
            ViewBag.MediaCusto = mediaCustos;


            GraphicsViewModel Gvm = new GraphicsViewModel
            {
                ocorrencias = ocorrencias,  
                listaBu = listaBu
            };

            return View(Gvm);
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