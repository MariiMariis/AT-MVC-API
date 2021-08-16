using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Models
{
    public class ProcessadorModel
    {
        public int Id { get; set; }

        public string NomeProcessador { get; set; }

        public string ItemDescription { get; set; }

        public int Cores { get; set; }

        public int Threads { get; set; }

        public float BaseFrequency { get; set; }

        public DateTime LaunchDate { get; set; }

        public int FabricanteId { get; set; }

        public FabricanteModel NomeFabricante { get; set; }
    }
}
