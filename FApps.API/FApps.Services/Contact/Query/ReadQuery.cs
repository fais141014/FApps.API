using FApps.Core.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.Contact.Query 
{
    public class ReadQuery:IRequest<IEnumerable<ContactDTO>>
    {
        
    }
}
