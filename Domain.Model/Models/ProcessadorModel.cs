using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Models
{
    
    public class ProcessadorModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Processador")]
        public string NomeProcessador { get; set; }

        [DisplayName("Descrição do ítem")]
        public string ItemDescription { get; set; }

        public int Cores { get; set; }

        public int Threads { get; set; }

        [DisplayName("Frequencia de Base")]
        public float BaseFrequency { get; set; }

        [DisplayName("Data de Lançamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }

        public int FabricanteModelId { get; set; }

        public FabricanteModel Fabricante { get; set; }
    }
}
