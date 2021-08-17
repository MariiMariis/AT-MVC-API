using System;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models
{
    public class ProcessadorViewModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string NomeProcessador { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Remote(action: "IsItemDescriptionValid", controller: "Processador", AdditionalFields = "Id")]
        public string ItemDescription { get; set; }

        [Range(1, 100)]
        public int Cores { get; set; }

        [Range(1, 100)]
        public int Threads { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public float BaseFrequency { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }

        [Required]
        public int FabricanteId { get; set; }

        public FabricanteViewModel NomeFabricante { get; set; }


        public static ProcessadorViewModel From(ProcessadorModel processadorModel, bool firstMap = true)
        {
            var nomeFabricante = firstMap 
                 ? FabricanteViewModel.From(processadorModel.NomeFabricante)
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
                FabricanteId = processadorModel.FabricanteId,

                NomeFabricante = nomeFabricante,
            };


            return processadorViewModel;
        }

        public ProcessadorModel ToModel(bool firstMap = true)
        {
            var nomeFabricante = firstMap
                  ? NomeFabricante?.ToModel()
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
                FabricanteId = FabricanteId,

                NomeFabricante = nomeFabricante,
            };


            return processadorModel;
        }

    }
}

