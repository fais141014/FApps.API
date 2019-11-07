using FApps.Core.DTO;
using MediatR;
using System;

namespace FApps.Services.Contact.Query
{
    public class ReadByIdQuery:IRequest<ContactDTO>
    {
        public Guid Id { get; set; }
    }
}
