using FApps.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FApps.Data.Interfaces
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(string key1, string key2);
        Task<Guid> CreateUserAsync(User user);
    }
}
