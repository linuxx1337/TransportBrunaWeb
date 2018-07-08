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
}