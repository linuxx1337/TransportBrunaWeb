using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportBrunaWeb.Models
{
    public partial class Gallery
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int WebImageId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> OrderNo { get; set; }
    }
}