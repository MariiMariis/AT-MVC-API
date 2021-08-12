using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Models
{
    public class FabricanteModel
    {
        public int Id { get; set; }

        public string NomeFabricante { get; set; }

        public string Fundador { get; set; }

        public string PaisOrigem { get; set; }

        public DateTime DataFundacao { get; set; }

        public List<ProcessadorModel> Processadores { get; set; }
    }
}
