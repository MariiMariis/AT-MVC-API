using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models
{
    

    public class FabricanteViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome do Fabricante")]
        [Remote(action: "IsNameValid", controller: "FabricanteController", AdditionalFields = "Id")]
        [StringLength(150)]
        public string NomeFabricante { get; set; }

        [Required]
        [DisplayName("Fundador")]
        [StringLength(150)]
        public string Fundador { get; set; }

        [Required]
        [DisplayName("Pais de Origem")]
        [StringLength(150)]
        public string PaisOrigem { get; set; }

        [Required]
        [DisplayName("Data de Fundação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFundacao { get; set; }

        public ProcessadorViewModel Processadores { get; set; }

    }
}