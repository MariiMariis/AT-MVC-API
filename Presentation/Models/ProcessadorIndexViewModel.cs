using System.Collections.Generic;

namespace Presentation.Models
{
    public class ProcessadorIndexViewModel
    {

        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<ProcessadorViewModel> Processadores { get; set; }

    }
}
