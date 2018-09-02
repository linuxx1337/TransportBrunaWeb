using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class Containers
    {
        [Key]
        public Guid ContainerID { get; set; }

        [Display(Name = "Tip kesona")]
        [Required(ErrorMessage = "Tip kesona mora biti izbran!")]
        public Guid ContainerTypeID { get; set; }
        public virtual ContainerTypes ContainerTypes { get; set; }

        [Display(Name = "Podjetje")]
        [Required(ErrorMessage = "Podjetje mora biti izbrano!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        //*****

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string Name { get; set; }

        [Display(Name = "Oznaka")]
        [Required(ErrorMessage = "Oznaka mora biti določena!")]
        public string Label { get; set; }

        [Display(Name = "Volumen")]
        public double? Volume { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // ******************************

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateModified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }

    public class ContainersViewModel
    {
        public Guid ContainerID { get; set; }

        [Display(Name = "Tip kesona")]
        [Required(ErrorMessage = "Tip kesona mora biti izbran!")]
        public Guid ContainerTypeID { get; set; }
        public virtual ContainerTypes ContainerTypes { get; set; }

        [Display(Name = "Podjetje")]
        [Required(ErrorMessage = "Podjetje mora biti izbrano!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        //*****

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string Name { get; set; }

        [Display(Name = "Oznaka")]
        [Required(ErrorMessage = "Oznaka mora biti določena!")]
        public string Label { get; set; }

        [Display(Name = "Volumen")]
        public double? Volume { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}