using AutoMapper;
using FApps.Core.Extension;
using FApps.Data.Interfaces;
using FApps.Services.User.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FApps.Services.User.QueryHandler
{
    public class ReadQueryByConditionsHandler:IRequestHandler<ReadQueryByConditions,FApps.Core.DTO.UserDTO>
    {
        private readonly IUserServices _services;
        private readonly IMapper _mapper;
        public ReadQueryByConditionsHandler(IUserServices services,IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        public async Task<FApps.Core.DTO.UserDTO>Handle(ReadQueryByConditions request,CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            var result = await _services.GetUserByIdAsync(request.Email, request.Password);
            var output = _mapper.Map<FApps.Core.DTO.UserDTO>(result);
            return output;
        }
    }
}
