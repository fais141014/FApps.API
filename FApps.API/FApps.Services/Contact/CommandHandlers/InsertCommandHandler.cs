using MediatR;
using System;
using FApps.Data.Interfaces;
using FApps.Services.Contact.Command;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FApps.Services.Contact.CommandHandlers
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand, Guid>
    {
        public IContactService _service;
        public InsertCommandHandler(IContactService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            Guid id = await _service.CreateContactAsync(request._contact);
            return id;
        }
    }
}
