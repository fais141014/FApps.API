using AutoMapper;
using FApps.Core.DTO;
using FApps.Core.Extension;
using FApps.Data.Interfaces;
using FApps.Services.Contact.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FApps.Services.Contact.QueryHandlers
{
    public class ReadByIdQueryHandler:IRequestHandler<ReadByIdQuery,ContactDTO>
    {
        private readonly IContactService _service;
        private readonly IMapper _mapper;

        public ReadByIdQueryHandler(IContactService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<ContactDTO> Handle(ReadByIdQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var contact = await _service.GetContactByIdAsync(request.Id);
            var output = _mapper.Map<ContactDTO>(contact);
            return output;
        }
    }
}
