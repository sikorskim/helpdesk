using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class OrderComment
    {
        public int OrderCommentId { get; set; }
        public Order Order { get; set; }
        [DisplayName("Czas")]
        public DateTime Time { get; set; }
        public Status Status { get; set; }
        [DisplayName("Treść")]
        public string Text { get; set; }
        [DisplayName("Użytkownik")]
        public string Username { get; set; }
        public virtual int OrderId { get; set; }
    }
}