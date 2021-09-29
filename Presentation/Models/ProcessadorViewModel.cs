using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Presentation.Models
{
    public class ProcessadorViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome do Processador")]
        [StringLength(150)]
        public string NomeProcessador { get; set; }

        [DisplayName("Descrição")]
        [StringLength(maximumLength: 50, MinimumLength = 1)]
        [Remote(action: "IsItemDescriptionValid", controller: "Processador", AdditionalFields = "Id")]
        public string ItemDescription { get; set; }

        [Range(1, 200)]
        public int Cores { get; set; }

        [Range(1, 400)]
        public int Threads { get; set; }

        [DisplayName("Frequência de Base")]
        [Range(1, 100)]
        public double BaseFrequency { get; set; }

        [DisplayName("Data de Lançamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }

        [DisplayName("Nome do Fabricante")]
        [Required]
        public int FabricanteId { get; set; }

        public FabricanteViewModel Fabricante { get; set; }

    }
}

