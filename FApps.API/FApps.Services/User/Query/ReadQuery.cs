using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.User.Query
{
    public class ReadQuery:IRequest<IEnumerable<FApps.Core.DTO.UserDTO>>
    {
        
    }
}
