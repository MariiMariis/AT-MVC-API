using System;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models;
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
        public float BaseFrequency { get; set; }

        [DisplayName("Data de Lançamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }

        [DisplayName("Nome do Fabricante")]
        [Required]
        public int FabricanteId { get; set; }

        public FabricanteViewModel Fabricante { get; set; }


        public static ProcessadorViewModel From(ProcessadorModel processadorModel, bool firstMap = true)
        {
            var nomeFabricante = firstMap 
                 ? FabricanteViewModel.From(processadorModel.Fabricante)
                 : null;


            var processadorViewModel = new ProcessadorViewModel
            {
                Id = processadorModel.Id,
                NomeProcessador = processadorModel.NomeProcessador,
                ItemDescription = processadorModel.ItemDescription,
                Cores = processadorModel.Cores,
                Threads = processadorModel.Threads,
                BaseFrequency = processadorModel.BaseFrequency,
                LaunchDate = processadorModel.LaunchDate,
                FabricanteId = processadorModel.FabricanteModelId,

                Fabricante = nomeFabricante,
            };


            return processadorViewModel;
        }

        public ProcessadorModel ToModel(bool firstMap = true)
        {
            var nomeFabricante = firstMap
                  ? this.Fabricante?.ToModel()
                  : null;


            var processadorModel = new ProcessadorModel
            {
                Id = Id,
                NomeProcessador = NomeProcessador,
                ItemDescription = ItemDescription,
                Cores = Cores,
                Threads = Threads,
                BaseFrequency = BaseFrequency,
                LaunchDate = LaunchDate,
                FabricanteModelId = FabricanteId,

                Fabricante = nomeFabricante,
            };


            return processadorModel;
        }

    }
}

