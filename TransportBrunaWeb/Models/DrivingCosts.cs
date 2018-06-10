using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class DrivingCosts
    {
        [Key]
        public Guid DrivingCostID { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "The cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        [Display(Name = "TransportationLog")]
        [Required(ErrorMessage = "The TransportationLog must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

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

    public class DrivingCostsViewModel
    {
        public Guid DrivingCostID { get; set; }

        [Display(Name = "Cost")]
        [Required(ErrorMessage = "The cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }

        [Display(Name = "TransportationLog")]
        [Required(ErrorMessage = "The TransportationLog must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        public string Description { get; set; }
    }
}