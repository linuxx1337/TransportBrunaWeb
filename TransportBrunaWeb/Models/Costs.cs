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

        [Display(Name = "Tip stroška")]
        [Required(ErrorMessage = "Tip stroška mora biti izbran!")]
        public Guid CostTypeID { get; set; }
        public virtual CostTypes CostTypes { get; set; }

        [Display(Name = "Znesek")]
        [Required(ErrorMessage = "Znesek mora biti določen!")]
        public double Amount { get; set; }

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)] // tole rabiš za date picker
        public DateTime Date { get; set; }

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

    public class CostsViewModel
    {
        public Guid CostID { get; set; }

        [Display(Name = "Tip stroška")]
        [Required(ErrorMessage = "Tip stroška mora biti izbran!")]
        public Guid CostTypeID { get; set; }
        public virtual CostTypes CostTypes { get; set; }

        [Display(Name = "Znesek")]
        [Required(ErrorMessage = "Znesek mora biti določen!")]
        public double Amount { get; set; }

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Datum mora biti določen!")]
        [DisplayFormat(DataFormatString = "{0:d. M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)] // tole rabiš za date picker
        public DateTime Date { get; set; }

        [Display(Name = "Opomba")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}