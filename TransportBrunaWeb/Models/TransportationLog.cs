using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class TransportationLog
    {
        [Key]
        public Guid TransportationLogID { get; set; }

        [Display(Name = "Container")]
        [Required(ErrorMessage = "The Container must be specified!")]
        public Guid ContainerID { get; set; }
        public virtual Containers Containers { get; set; }

        [Display(Name = "Vehicle")]
        [Required(ErrorMessage = "The Vehicle must be specified!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Cargo Type")]
        [Required(ErrorMessage = "The Cargo Type must be specified!")]
        public Guid CargoID { get; set; }
        public virtual CargoTypes CargoTypes { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "The Customer must be specified!")]
        public Guid CustomerID { get; set; }
        public virtual Customers Customers { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "The Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        // ******

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date must be specified!")]
        public DateTime Date { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "The location must be specified!")]
        public string Location { get; set; }

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

    public class TransportationLogViewModel
    {
        public Guid TransportationLogID { get; set; }

        [Display(Name = "Container")]
        [Required(ErrorMessage = "The Container must be specified!")]
        public Guid ContainerID { get; set; }
        public virtual Containers Containers { get; set; }

        [Display(Name = "Vehicle")]
        [Required(ErrorMessage = "The Vehicle must be specified!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Cargo Type")]
        [Required(ErrorMessage = "The Cargo Type must be specified!")]
        public Guid CargoID { get; set; }
        public virtual CargoTypes CargoTypes { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "The Customer must be specified!")]
        public Guid CustomerID { get; set; }
        public virtual Customers Customers { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "The Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Note { get; set; }

        public string Description { get; set; }
    }
}