using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class Order
    {
        [DisplayName("#")]
        public int OrderId { get; set; }
        [DisplayName("Użytkownik")]
        public string UserName { get; set; }
        [DisplayName("Data utworzenia")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}",
        //       ApplyFormatInEditMode = true)]
        public DateTime TimeCreated { get; set; }
        [DisplayName("Data zamknięcia")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}",
        //       ApplyFormatInEditMode = true)]
        public DateTime? TimeClosed { get; set; }
        [DisplayName("Pilne")]
        public bool Urgent { get; set; }
        [Required]
        [MinLength(8)]
        [DisplayName("Treść")]
        public string Content { get; set; }

        [DisplayName("Kategoria")]
        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }

        [DisplayName("Treść skrót")]
        public virtual string ContentShort {
            get {
                string shortcut;
                if (Content.Length < 30)
                {
                    shortcut = Content;
                }
                else
                {
                    shortcut = Content.Substring(0, 30) + "...";
                }
                return shortcut;
            } }
    }
}