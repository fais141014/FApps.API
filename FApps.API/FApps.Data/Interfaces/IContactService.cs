using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FApps.Core.Domain;

namespace FApps.Data.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactAsync();
        Task<Contact> GetContactByIdAsync(Guid ContactId);
        Task<Guid> CreateContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(Contact contact);
    }
}
