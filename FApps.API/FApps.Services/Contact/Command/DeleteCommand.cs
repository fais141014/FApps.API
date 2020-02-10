using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.Contact.Command
{
    public class DeleteCommand:IRequest<bool>
    {
        public Guid? Id { get; set; }
        public DeleteCommand(Guid? id)
        {
            Id = id;
        }
    }
}
