using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class TransportationStatus
    {
        [Key]
        public Guid TransportationStatusID { get; set; }

        [Display(Name = "Transportation Status Type")]
        [Required(ErrorMessage = "The Transportation Status Type must be specified!")]
        public Guid TransportationTypeStatusID { get; set; }
        public virtual TransportationStatusTypes TransportationStatusTypes { get; set; }

        [Display(Name = "Transportation Log")]
        [Required(ErrorMessage = "The Transportation Log must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date must be specified!")]
        public DateTime Date { get; set; }

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

    public class TransportationStatusViewModel
    {
        public Guid TransportationStatusID { get; set; }

        [Display(Name = "Transportation Status Type")]
        [Required(ErrorMessage = "The Transportation Status Type must be specified!")]
        public Guid TransportationTypeStatusID { get; set; }
        public virtual TransportationStatusTypes TransportationStatusTypes { get; set; }

        [Display(Name = "Transportation Log")]
        [Required(ErrorMessage = "The Transportation Log must be specified!")]
        public Guid TransportationLogID { get; set; }
        public virtual TransportationLog TransportationLog { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date must be specified!")]
        public DateTime Date { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}