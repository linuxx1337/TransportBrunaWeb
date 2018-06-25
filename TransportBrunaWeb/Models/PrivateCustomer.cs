using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class PrivateCustomer
    {
        [Key]
        public Guid PrivateCustomerID { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The name must be specified!")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "The address must be specified!")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^([\+]|0)[(\s]{0,1}[2-9][0-9]{0,2}[\s-)]{0,2}[0-9][0-9][0-9\s-]*[0-9]$", ErrorMessage = "Please enter right number, example: 040 123-456, 07 12-12456, +(386) 334-452")]
        [Required(ErrorMessage = "The phone number must be specified!")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "VAT")]
        public int Vat { get; set; }

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

    public class PrivateCustomerViewModel
    {
        public Guid PrivateCustomerID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "VAT")]
        public int Vat { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}