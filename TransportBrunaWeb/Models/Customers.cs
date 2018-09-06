using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class Customers
    {
        [Key]
        public Guid CustomerID { get; set; }

        [Display(Name = "Company")]
        //[Required(ErrorMessage = "The Company must be specified!")]
        public Guid? CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Display(Name = "Private Customer")]
        //[Required(ErrorMessage = "The Private Customer must be specified!")]
        public Guid? PrivateCustomerID { get; set; }
        public virtual PrivateCustomer PrivateCustomer { get; set; }

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

    public class CutomersViewModel
    {
        public Guid CustomerID { get; set; }

        [Display(Name = "Company")]
        //[Required(ErrorMessage = "The Company must be specified!")]
        public Guid? CompanyID { get; set; }
        public virtual Company Company { get; set; }

        [Display(Name = "Private Customer")]
        //[Required(ErrorMessage = "The Private Customer must be specified!")]
        public Guid? PrivateCustomerID { get; set; }
        public virtual PrivateCustomer PrivateCustomer { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class CustomerSpecial
    {
        public Guid CustomerID { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? PrivateCustomerID { get; set; }


        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv mora biti določen!")]
        public string Name { get; set; }

        [Display(Name = "Naslov")]
        [Required(ErrorMessage = "Naslov mora biti določen!")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        [RegularExpression(@"^([\+]|0)[(\s]{0,1}[1-9][0-9]{0,2}[\s-)]{0,2}[0-9][0-9][0-9\s-]*[0-9]$", ErrorMessage = "Vnesite pravilno številko, primer: 040 123-456, 07 12-12456, +(386) 334-452")]
        [Required(ErrorMessage = "Telefon mora biti določena!")]
        public string Phone { get; set; }

        [Display(Name = "E-pošta")]
        [Required(ErrorMessage = "E-pošta mora biti določena!")]
        public string Email { get; set; }

        [Display(Name = "Davčna številka")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Vnesite SI davčno številko!")]
        [Required(ErrorMessage = "Davčna številka mora biti določena!")]
        public int? Vat { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

    }
}