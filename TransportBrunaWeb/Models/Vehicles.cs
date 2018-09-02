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

        [Display(Name = "Podjetje")]
        [Required(ErrorMessage = "Podjetje mora biti izbrano!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string Name { get; set; }

        [Display(Name = "Registrska tablica")]
        [Required(ErrorMessage = "Registrska tablica mora biti določena!")]
        public string RegPlate { get; set; }

        [Display(Name = "Znamka")]
        [Required(ErrorMessage = "Znamka mora biti določena!")]
        public string Brand { get; set; }

        [Display(Name = "VIN številka")]
        [Required(ErrorMessage = "VIN številka mora biti določena!")]
        public string Vin { get; set; }

        [Display(Name = "Največja dovoljena masa")]
        [Required(ErrorMessage = "Največja dovoljena masa mora biti določena!")]
        public int Gvw { get; set; }

        [Display(Name = "Masa tovora")]
        [Required(ErrorMessage = "Masa tovora mora biti določena!")]
        public int MassCargo { get; set; }

        [Display(Name = "Vrsta")]
        [Required(ErrorMessage = "Vrsta vozila mora biti določena!")]
        public string Type { get; set; }

        [Display(Name = "Datum registracije")]
        [Required(ErrorMessage = "Datum registracije mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateReg { get; set; }

        [Display(Name = "Datum tehničnega")]
        [Required(ErrorMessage = "Datum tehničnega pregleda mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateMot { get; set; }

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

    public class VehiclesViewModel
    {
        public Guid VehicleID { get; set; }

        [Display(Name = "Podjetje")]
        [Required(ErrorMessage = "Podjetje mora biti izbrano!")]
        public Guid CompanyID { get; set; }
        public virtual Company Company { get; set; }

        /*[Display(Name = "Cost")]
        [Required(ErrorMessage = "Cost must be specified!")]
        public Guid CostID { get; set; }
        public virtual Costs Costs { get; set; }*/

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string Name { get; set; }

        [Display(Name = "Registrska tablica")]
        [Required(ErrorMessage = "Registrska tablica mora biti določena!")]
        public string RegPlate { get; set; }

        [Display(Name = "Znamka")]
        [Required(ErrorMessage = "Znamka mora biti določena!")]
        public string Brand { get; set; }

        [Display(Name = "VIN številka")]
        [Required(ErrorMessage = "VIN številka mora biti določena!")]
        public string Vin { get; set; }

        [Display(Name = "Največja dovoljena masa")]
        [Required(ErrorMessage = "Največja dovoljena masa mora biti določena!")]
        public int Gvw { get; set; }

        [Display(Name = "Masa tovora")]
        [Required(ErrorMessage = "Masa tovora mora biti določena!")]
        public int MassCargo { get; set; }

        [Display(Name = "Vrsta")]
        [Required(ErrorMessage = "Vrsta vozila mora biti določena!")]
        public string Type { get; set; }

        [Display(Name = "Datum registracije")]
        [Required(ErrorMessage = "Datum registracije mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateReg { get; set; }

        [Display(Name = "Datum tehničnega")]
        [Required(ErrorMessage = "Datum tehničnega pregleda mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateMot { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}