using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class Report
    {
        public string Name { get; private set; }
        public List<ReportItem> Items { get; private set; }
        public string Summary { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public DateTime Created { get; private set; }

        helpdeskContext db = new helpdeskContext();

        public Report(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            Items = new List<ReportItem>();
        }

        public Report()
        {
            Items = new List<ReportItem>();
        }

        public Report OrderSummary()
        {
            Name = "Ilość zgłoszeń w wybranym okresie";
            string key0 = "Ilość zgłoszeń utworzonych";
            string value0 = db.Orders.Where(p => p.TimeCreated >= Start && p.TimeClosed <= End).Count().ToString();
            ReportItem reportItem = new ReportItem(key0, value0);
            Items.Add(reportItem);
            return this;
        }
    }
}