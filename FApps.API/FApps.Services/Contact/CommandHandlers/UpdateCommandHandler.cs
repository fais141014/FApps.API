using FApps.Data.Interfaces;
using FApps.Services.Contact.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FApps.Services.Contact.CommandHandlers
{
    public class UpdateCommandHandler:IRequestHandler<UpdateCommand,bool>
    {
        private readonly IContactService _service;
        public UpdateCommandHandler(IContactService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            //request.contactModel.Id = request.id;
            var contact = await _service.GetContactByIdAsync(request.id);
            if (contact == null) { return false; }
            else
            {
                return await _service.UpdateContactAsync(request.contactModel);
            }
        }
    }
}
