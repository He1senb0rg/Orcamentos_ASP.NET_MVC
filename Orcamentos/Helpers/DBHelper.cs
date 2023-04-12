
using Orcamentos.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
