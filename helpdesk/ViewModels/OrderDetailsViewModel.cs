using helpdesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderComment> OrderComments { get; set; }
       // public virtual OrderComment OrderComment{get;set;}
    }
}