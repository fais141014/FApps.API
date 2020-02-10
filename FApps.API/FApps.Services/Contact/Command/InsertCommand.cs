using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FApps.Services.Contact.Command
{
   public class InsertCommand:IRequest<Guid>
    {
        public FApps.Core.Domain.Contact _contact;
        public InsertCommand(FApps.Core.Domain.Contact contact)
        {
            _contact = contact;
        }       
    }
}
