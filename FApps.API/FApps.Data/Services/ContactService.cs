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
    public class ContactService:RepositoryBase<Contact>, IContactService
    {
        private readonly ApplicationDbContext _context;
        public ContactService(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContactAsync()
        {
            var contacts = await FindAllAsync();
            return contacts.OrderBy(c => c.FirstName);
        }

        public async Task<Contact> GetContactByIdAsync(Guid ContactId)
        {
            var contacts = await FindByConditionAync(o=>o.Id.Equals(ContactId));
            return contacts.FirstOrDefault();
        }

        public async Task<Guid> CreateContactAsync(Contact contact)
        {
            Create(contact);
            await SaveAsync();
            return (Guid)contact.Id;
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            Update(contact);
            var rowsAffected = await Save();
            if(rowsAffected >0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            Delete(contact);
            var rowsAffected = await Save();
            if(rowsAffected >0)
            {
                return true;
            }
            return false;
        }
    }
}
