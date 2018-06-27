using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public class Costs
    {
        [Key]
        public Guid CostID { get; set; }

        //[ForeignKey("CostTypes")]
        [Display(Name = "Cost Type")]
        [Required(ErrorMessage = "The cost type must be specified!")]
        public Guid CostTypeID { get; set; }
        public virtual CostTypes CostTypes { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "The amount must be specified!")]
        public double Amount { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The date must be specified!")]
        [DataType(DataType.Date)] // tole rabiš za date picker
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
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateModified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }

    public class CostsViewModel
    {
        public Guid CostID { get; set; }

        [Display(Name = "Cost Type")]
        [Required(ErrorMessage = "The cost type must be specified!")]
        public Guid CostTypeID { get; set; }
        public virtual CostTypes CostTypes { get; set; }

        [Display(Name = "Amount")]
        public double Amount { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}