using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Core.DTO
{
    public class ContactDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFamilyMember { get; set; }
        public string Company { get; set; }
    }
}
