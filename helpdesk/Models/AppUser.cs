using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class AppUser
    {
        public int AppUSerId { get; set; }
        public string Username { get; set; }
        public int Visits { get; set; }
        public Credential Credential { get; set; }
        public virtual string UsernameShort
        {
            get
            {
                int index = Username.IndexOf(@"\") + 1;
                return Username.Substring(index, Username.Length - index);
            }
        }
        public virtual string Hostname
        {
            get
            {
                int index = Username.IndexOf(@"\");
                return Username.Substring(0, index);
            }
        }

    }
}