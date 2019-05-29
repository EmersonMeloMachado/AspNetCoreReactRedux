using System.Linq;
using BusinessLibrary.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.EntityModels;

namespace BusinessLibrary.Service
{
    public class ContactService : IContactService
    {
        public async Task<List<ContactModel>> GetContacts()
        {
            using(ContactDBContext db = new ContactDBContext())
            {
                return await (from a in db.Contacts.AsNoTracking()
                              select new ContactModel
                              {
                                  ContactId = a.ContactId,
                                  FirstName = a.FirstName,
                                  LastName = a.LastName,
                                  Email = a.Email,
                                  Phone = a.Phone
                              }).ToListAsync();
            }
        }

        public async Task<bool> SaveContact(ContactModel contactModel)
        {
            using(ContactDBContext db = new ContactDBContext())
            {
                Contacts contact = db.Contacts.Where
                         (x => x.ContactId == contactModel.ContactId).FirstOrDefault();
                if(contact == null)
                {
                    contact = new Contacts()
                    {
                        FirstName = contactModel.FirstName,
                        LastName = contactModel.LastName,
                        Email = contactModel.Email,
                        Phone = contactModel.Phone
                    };
                    db.Contacts.Add(contact);

                }
                else
                {
                    contact.FirstName = contactModel.FirstName;
                    contact.LastName = contactModel.LastName;
                    contact.Email = contactModel.Email;
                    contact.Phone = contactModel.Phone;
                }

                return await db.SaveChangesAsync() >= 1;
            }
        }

        public async Task<bool> DeleteContact(int contactId)
        {
            using(ContactDBContext db = new ContactDBContext())
            {
                Contacts contact =
                     db.Contacts.Where(x => x.ContactId == contactId).FirstOrDefault();
                if(contact != null)
                {
                    db.Contacts.Remove(contact);
                }
                return await db.SaveChangesAsync() >= 1;
            }
        }
    }
}
