﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class ReportItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ReportItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}