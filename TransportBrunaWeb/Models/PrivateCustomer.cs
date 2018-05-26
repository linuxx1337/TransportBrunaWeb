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
        [Required(ErrorMessage = "The phone number must be specified!")]
        public int Phone { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateModified { get; set; }

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
        public int Phone { get; set; }

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