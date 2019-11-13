using AutoMapper;
using FApps.Core.DTO;
using FApps.Core.Extension;
using FApps.Data.Interfaces;
using FApps.Data.Services;
using FApps.Services.User.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FApps.Services.User.QueryHandler
{
    public class ReadQueryHandler:IRequestHandler<ReadQuery,IEnumerable<FApps.Core.DTO.UserDTO>>
    {
        private readonly IUserServices _services;
        private readonly IMapper _mapper;
        public ReadQueryHandler(IUserServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FApps.Core.DTO.UserDTO>>Handle(ReadQuery request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var userList = await _services.GetAllUserAsync();
            var output = _mapper.Map<IEnumerable<UserDTO>>(userList);
            return output;
        }

    }
}
