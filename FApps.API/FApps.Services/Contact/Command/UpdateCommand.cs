using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.Contact.Command
{
    public class UpdateCommand:IRequest<bool>
    {
        public Guid id { get; set; }
        public FApps.Core.Domain.Contact contactModel { get; set; }
        public UpdateCommand(Guid contactId, FApps.Core.Domain.Contact model)
        {
            id = contactId;
            contactModel = model;
        }
    }
}
