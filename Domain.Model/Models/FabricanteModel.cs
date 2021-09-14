using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    

    public class FabricanteModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Fabricante")]
        public string NomeFabricante { get; set; }

        [DisplayName("Fundador")]
        public string Fundador { get; set; }

        [DisplayName("Pais de Origem")]
        public string PaisOrigem { get; set; }

        [DisplayName("Data de fundação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFundacao { get; set; }

        public List<ProcessadorModel> Processadores { get; set; }
    }
}
