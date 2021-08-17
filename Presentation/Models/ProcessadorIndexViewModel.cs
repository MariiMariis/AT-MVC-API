using System.Collections.Generic;
using Domain.Model.Models;

namespace Presentation.Models
{
    public class ProcessadorIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<ProcessadorModel> Processadores { get; set; }
    }
}
