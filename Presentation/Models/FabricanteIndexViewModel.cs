using System.Collections.Generic;

namespace Presentation.Models
{
    public class FabricanteIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<FabricanteViewModel> Fabricantes { get; set; }
    }
}