using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helpdesk.Models
{
    public class Credential
    {
        public int CredentialId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AppUser> AppUser { get; set; }
    }
}