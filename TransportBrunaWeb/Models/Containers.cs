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

        [Display(Name = "Container Type")]
        [Required(ErrorMessage = "The Container type must be specified!")]
        public Guid ContainerTypeID { get; set; }
        public virtual ContainerTypes ContainerTypes { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The Company must be specified!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        //*****

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name must be specified!")]
        public string Name { get; set; }

        [Display(Name = "Label")]
        [Required(ErrorMessage = "The label must be specified!")]
        public string Label { get; set; }

        [Display(Name = "Volume")]
        public double Volume { get; set; }

        [Display(Name = "Note")]
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

        [Display(Name = "Container Type")]
        [Required(ErrorMessage = "The Container type must be specified!")]
        public Guid ContainerTypeID { get; set; }
        public virtual ContainerTypes ContainerTypes { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The Company must be specified!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public double Volume { get; set; }

        public string Note { get; set; }

        public string Description { get; set; }
    }
}