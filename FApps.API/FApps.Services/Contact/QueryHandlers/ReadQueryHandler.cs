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
   public class ReadQueryHandler:IRequestHandler<ReadQuery,IEnumerable<ContactDTO>>
    {
        private readonly IContactService _service;
        private readonly IMapper _mapper;

        public ReadQueryHandler(IContactService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDTO>>Handle(ReadQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var contactList = await _service.GetAllContactAsync();
            var output = _mapper.Map<IEnumerable<ContactDTO>>(contactList);
            return output;
        }
    }
}
