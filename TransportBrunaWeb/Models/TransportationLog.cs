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

        [Display(Name = "Keson")]
        [Required(ErrorMessage = "Keson mora biti izbran!")]
        public Guid ContainerID { get; set; }
        public virtual Containers Containers { get; set; }

        [Display(Name = "Vozilo")]
        [Required(ErrorMessage = "Vozilo mora biti izbrano!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Tip tovora")]
        [Required(ErrorMessage = "Tip tovora mora biti izbran!")]
        public Guid CargoID { get; set; }
        public virtual CargoTypes CargoTypes { get; set; }

        [Display(Name = "Stranka")]
        [Required(ErrorMessage = "Stranka mora biti izbrana!")]
        public Guid CustomerID { get; set; }
        public virtual Customers Customers { get; set; }

        [Display(Name = "Cost")]
        //[Required(ErrorMessage = "The Cost must be specified!")]
        public Guid? CostID { get; set; }
        public virtual Costs Costs { get; set; }

        [Required]
        public bool Active { get; set; }

        // ******

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Lokacija mora biti določena!")]
        public string Location { get; set; }

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

    public class TransportationLogViewModel
    {
        public Guid TransportationLogID { get; set; }

        [Display(Name = "Keson")]
        [Required(ErrorMessage = "Keson mora biti izbran!")]
        public Guid ContainerID { get; set; }
        public virtual Containers Containers { get; set; }

        [Display(Name = "Vozilo")]
        [Required(ErrorMessage = "Vozilo mora biti izbrano!")]
        public Guid VehicleID { get; set; }
        public virtual Vehicles Vehicles { get; set; }

        [Display(Name = "Tip tovora")]
        [Required(ErrorMessage = "Tip tovora mora biti izbran!")]
        public Guid CargoID { get; set; }
        public virtual CargoTypes CargoTypes { get; set; }

        [Display(Name = "Stranka")]
        [Required(ErrorMessage = "Stranka mora biti izbrana!")]
        public Guid CustomerID { get; set; }
        public virtual Customers Customers { get; set; }

        [Display(Name = "Cost")]
        //[Required(ErrorMessage = "The Cost must be specified!")]
        public Guid? CostID { get; set; }
        public virtual Costs Costs { get; set; }

        // ******

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Lokacija")]
        [Required(ErrorMessage = "Lokacija mora biti določena!")]
        public string Location { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}