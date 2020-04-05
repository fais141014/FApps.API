using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FApps.Core.DTO
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }
        //[Required]
        //public string Password { get; set; }
        public string Name { get; set; }
        public string token { get; set; }
        public DateTime? expiration { get; set; }
       
    }
}
