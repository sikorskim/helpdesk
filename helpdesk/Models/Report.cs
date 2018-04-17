using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class Report
    {
        public string Name { get; private set; }
        public Dictionary<string, string> Items { get; private set; }
        public string Summary { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public DateTime Created { get; private set; }

        helpdeskContext db = new helpdeskContext();

        public Report(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            Items = new Dictionary<string, string>();
        }

        public Report()
        {}

        public Dictionary<string,string> OrderSummary()
        {
            Name = "Ilość zgłoszeń w wybranym okresie";
            string key0 = "Ilość zgłoszeń utworzonych";
            string value0 = db.Orders.Where(p => p.TimeCreated >= Start && p.TimeCreated <= End).Count().ToString();
            
            Items.Add(key0,value0);
            return Items;
        }

        public Dictionary<string, string> OrdersByStatus()
        {
            Name = "Ilość zgłoszeń w wybranym okresie wg statusu";
            string[] statusName = db.Status.Select(p => p.StatusName).ToArray();

            foreach (string s in statusName)
            {
                string value = db.Orders.Where(p=>p.Status.StatusName==s).Count().ToString();
                Items.Add(s, value);
            }

            return Items;
        }

        public Dictionary<string, string> OrdersByCategory()
        {
            Name = "Ilość zgłoszeń w wybranym okresie wg kategorii";
            string[] categoryName = db.Categories.Select(p => p.CategoryName).ToArray();

            foreach (string s in categoryName)
            {
                string value = db.Orders.Where(p => p.Category.CategoryName == s).Count().ToString();
                Items.Add(s, value);
            }

            return Items;
        }
    }
}