using FApps.Core.Context;
using FApps.Core.Domain;
using FApps.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FApps.Data.Services
{
    public class UserServices: RepositoryBase<User> ,IUserServices
    {
        private readonly ApplicationDbContext _context;
        public UserServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var user = await FindAllAsync();
            return user.OrderBy(u => u.Name);
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            Create(user);
            await SaveAsync();
            return (Guid)user.Id;
        }

        public async Task<User> GetUserByIdAsync(string key1, string key2)
        {
            var user = await FindByConditionAync(u => u.Email.Equals(key1) && u.Email.Equals(key2));
            return user.FirstOrDefault();
        }
    }
}
