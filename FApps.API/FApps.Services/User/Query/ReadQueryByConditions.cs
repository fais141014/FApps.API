using FApps.Data.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.User.Query
{
    public class ReadQueryByConditions:IRequest<FApps.Core.DTO.UserDTO>
    {
       public string Email { get; set; }
        public string Password { get; set; }
        public ReadQueryByConditions(string email,string password)
        {
            Email = email;
            Password = password;
        }
    }
}
