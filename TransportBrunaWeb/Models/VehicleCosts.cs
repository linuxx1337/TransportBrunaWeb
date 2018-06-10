using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class VehicleCosts
    {
        [Key]
        public Guid VehicleCostID { get; set; }

        [Display(Name = "Vehicle")]
        [Required(ErrorMessage = "The vehicle must be specified!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // ******************************

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateModified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }

    public class VehicleCostsViewModel
    {
        public Guid VehicleCostID { get; set; }

        [Display(Name = "Vehicle")]
        [Required(ErrorMessage = "The vehicle must be specified!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        public string Description { get; set; }
    }
}