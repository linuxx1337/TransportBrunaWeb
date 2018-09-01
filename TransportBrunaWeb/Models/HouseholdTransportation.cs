using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class HouseholdTransportation
    {
        [Key]
        public Guid HouseholdTransportationID { get; set; }

        [Display(Name = "Transportation Log")]
        [Required(ErrorMessage = "The Transportation Log must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string FirstName { get; set; }

        [Display(Name = "Priimek")]
        [Required(ErrorMessage = "Priimek mora biti določen!")]
        public string LastName { get; set; }

        [Display(Name = "Naslov")]
        [Required(ErrorMessage = "Naslov mora biti določen")]
        public string Address { get; set; }

        [Display(Name = "Poštna številka")]
        [Required(ErrorMessage = "Poštna številka mora biti določena!")]
        public string PostCode { get; set; }

        [Display(Name = "Mesto")]
        [Required(ErrorMessage = "Mesto mora biti določeno!")]
        public string City { get; set; }

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Priponka")]
        public string Attachment { get; set; }

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

    public class HouseholdTransportationViewModel
    {
        public Guid HouseholdTransportationID { get; set; }

        [Display(Name = "Transportation Log")]
        [Required(ErrorMessage = "The Transportation Log must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime mora biti določeno!")]
        public string FirstName { get; set; }

        [Display(Name = "Priimek")]
        [Required(ErrorMessage = "Priimek mora biti določen!")]
        public string LastName { get; set; }

        [Display(Name = "Naslov")]
        [Required(ErrorMessage = "Naslov mora biti določen")]
        public string Address { get; set; }

        [Display(Name = "Poštna številka")]
        [Required(ErrorMessage = "Poštna številka mora biti določena!")]
        public string PostCode { get; set; }

        [Display(Name = "Mesto")]
        [Required(ErrorMessage = "Mesto mora biti določeno!")]
        public string City { get; set; }

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Priponka")]
        public string Attachment { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}