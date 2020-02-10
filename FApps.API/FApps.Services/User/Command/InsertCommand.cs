using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FApps.Services.User.Command
{
    public class InsertCommand:IRequest<Guid>
    {
        public FApps.Core.Domain.User _user;
        public InsertCommand(FApps.Core.Domain.User user)
        {
            _user = user;
        }
    }
}
