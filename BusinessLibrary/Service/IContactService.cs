using BusinessLibrary.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessLibrary.Service
{
    public interface IContactService
    {
        Task<List<ContactModel>> GetContacts();
        Task<bool> SaveContact(ContactModel contact);
        Task<bool> DeleteContact(int contactId);
    }
}
