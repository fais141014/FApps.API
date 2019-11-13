using FApps.Core.Extension;
using FApps.Data.Interfaces;
using FApps.Services.User.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FApps.Services.User.CommandHandler
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand, Guid>
    {
        public IUserServices _services;
        public InsertCommandHandler(IUserServices services)
        {
            _services = services;
        }
        public async Task<Guid> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            request.ThrowIfNull(nameof(request));
            Guid id = await _services.CreateUserAsync(request._user);
            return id;
        }
    }
}
