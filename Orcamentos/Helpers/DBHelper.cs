using Microsoft.AspNetCore.Mvc.Rendering;
using Orcamentos.Infrastructure;

namespace Orcamentos.Helpers
{
    public class DBHelper
    {

        static public IEnumerable<SelectListItem> FillBuManagers(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaBuManagers = context.buManagers
                .OrderBy(c => c.Nome)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Nome
                    }).ToList();

            return listaBuManagers;
        }

        static public IEnumerable<SelectListItem> FillProfileLevels(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaProfileLevels = context.profileLevels
                .OrderBy(c => c.Name)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Name
                    }).ToList();

            return listaProfileLevels;
        }

        static public IEnumerable<SelectListItem> FillProfiles(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaProfiles = context.profiles
                .OrderBy(c => c.Name)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Name
                    }).ToList();

            return listaProfiles;
        }

        static public IEnumerable<SelectListItem> FillBu(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaBu = context.businessUnits
                .OrderBy(c => c.Name)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Name
                    }).ToList();

            return listaBu;
        }

        static public IEnumerable<SelectListItem> FillRevenueTypes(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaRevenueTypes = context.revenueTypes
                .OrderBy(c => c.Nome)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Nome
                    }).ToList();

            return listaRevenueTypes;
        }

        static public IEnumerable<SelectListItem> FillOrcamentosNomes(DataContext context)
        {
            //https://www.c-sharpcorner.com/article/different-ways-bind-the-value-to-razor-dropdownlist-in-aspnet-mvc5/
            // Fill Categorias List
            IEnumerable<SelectListItem> listaOrcamentosNomes = context.orcamentoNomes
                .OrderBy(c => c.Nome)
                .Where(d => d.Ativo == true)
                .Select(c =>
                    new SelectListItem
                    {
                        Value = Convert.ToString(c.Id),
                        Text = c.Nome
                    }).ToList();

            return listaOrcamentosNomes;
        }
    }
}
