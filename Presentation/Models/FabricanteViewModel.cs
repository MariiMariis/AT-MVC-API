using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Model.Models;
using System.ComponentModel;

namespace Presentation.Models
{
    using System.ComponentModel;

    public class FabricanteViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome do Fabricante")]
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

        public List<ProcessadorViewModel> Processadores { get; set; }

        public static FabricanteViewModel From(FabricanteModel fabricanteModel)
        {
            var fabricanteViewModel = new FabricanteViewModel
            {
                Id = fabricanteModel.Id,
                NomeFabricante = fabricanteModel.NomeFabricante,
                Fundador = fabricanteModel.Fundador,
                PaisOrigem = fabricanteModel.PaisOrigem,
                DataFundacao = fabricanteModel.DataFundacao,

                Processadores = fabricanteModel.Processadores?.Select(x => ProcessadorViewModel.From(x, false)).ToList(),
            };


            return fabricanteViewModel;
        }

        public FabricanteModel ToModel()
        {
            var fabricanteModel = new FabricanteModel
            {
                Id = Id,
                NomeFabricante = NomeFabricante,
                Fundador = Fundador,
                PaisOrigem = PaisOrigem,
                DataFundacao = DataFundacao,

                Processadores = Processadores?.Select(x => x.ToModel(false)).ToList(),
            };

            return fabricanteModel;
        }
    }
}