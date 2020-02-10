using FApps.Core.DTO;
using MediatR;
using System;
using FApps.Core.Domain;

namespace FApps.Services.Contact.Query
{
    public class ReadByIdQuery:IRequest<FApps.Core.Domain.Contact>
    {
        public Guid Id { get; set; }
    }
}
