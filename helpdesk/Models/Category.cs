using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [DisplayName("Kategoria")]
        public string CategoryName { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
    }
}