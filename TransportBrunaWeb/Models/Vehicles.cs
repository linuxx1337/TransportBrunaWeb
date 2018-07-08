using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class Vehicles
    {
        [Key]
        public Guid VehicleID { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The Company must be specified!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name must be specified!")]
        public string Name { get; set; }

        [Display(Name = "Reg Plate")]
        [Required(ErrorMessage = "The registration plate must be specified!")]
        public string RegPlate { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The brand must be specified!")]
        public string Brand { get; set; }

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "The VIN number must be specified!")]
        public string Vin { get; set; }

        [Display(Name = "GVW")]
        [Required(ErrorMessage = "The Gross vehicle weight must be specified!")]
        public int Gvw { get; set; }

        [Display(Name = "Mass Cargo")]
        [Required(ErrorMessage = "The mass cargo must be specified!")]
        public int MassCargo { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The type of vehicle must be specified!")]
        public string Type { get; set; }

        [Display(Name = "Date Reg")]
        [Required(ErrorMessage = "The date reg must be specified!")]
        [DataType(DataType.Date)]
        public DateTime DateReg { get; set; }

        [Display(Name = "Date MOT")]
        [Required(ErrorMessage = "The date MOT must be specified!")]
        [DataType(DataType.Date)]
        public DateTime DateMot { get; set; }

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

    public class VehiclesViewModel
    {
        public Guid VehicleID { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The Company must be specified!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name must be specified!")]
        public string Name { get; set; }

        [Display(Name = "Reg Plate")]
        [Required(ErrorMessage = "The registration plate must be specified!")]
        public string RegPlate { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "The brand must be specified!")]
        public string Brand { get; set; }

        [Display(Name = "VIN")]
        [Required(ErrorMessage = "The VIN number must be specified!")]
        public string Vin { get; set; }

        [Display(Name = "GVW")]
        [Required(ErrorMessage = "The Gross vehicle weight must be specified!")]
        public int Gvw { get; set; }

        [Display(Name = "Mass Cargo")]
        [Required(ErrorMessage = "The mass cargo must be specified!")]
        public int MassCargo { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "The type of vehicle must be specified!")]
        public string Type { get; set; }

        [Display(Name = "Date Reg")]
        [Required(ErrorMessage = "The date reg must be specified!")]
        [DataType(DataType.Date)]
        public DateTime DateReg { get; set; }

        [Display(Name = "Date MOT")]
        [Required(ErrorMessage = "The date MOT must be specified!")]
        [DataType(DataType.Date)]
        public DateTime DateMot { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}