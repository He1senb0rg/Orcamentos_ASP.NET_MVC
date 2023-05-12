using Microsoft.EntityFrameworkCore;

namespace Orcamentos.Models.ViewModels
{
    public class GraphicsViewModel
    {
        public List<int> ocorrencias { get; set; }
        public List<BusinessUnit> listaBu{ get; set; } 

    }
}
