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

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The first name must be specified!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The last name must be specified!")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "The address must be specified!")]
        public string Address { get; set; }

        [Display(Name = "Post Code")]
        [Required(ErrorMessage = "The post code must be specified!")]
        public string PostCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "The city must be specified!")]
        public string City { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date must be specified!")]
        public DateTime Date { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Attachment")]
        public string Attachment { get; set; }

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

    public class HouseholdTransportationViewModel
    {
        public Guid HouseholdTransportationID { get; set; }

        [Display(Name = "Transportation Log")]
        [Required(ErrorMessage = "The Transportation Log must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public string Attachment { get; set; }

        public string Description { get; set; }
    }
}