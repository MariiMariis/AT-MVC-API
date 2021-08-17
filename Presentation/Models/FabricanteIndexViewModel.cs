using System.Collections.Generic;
using Domain.Model.Models;

namespace Presentation.Models
{
    public class FabricanteIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<FabricanteModel> Fabricantes { get; set; }
    }
}