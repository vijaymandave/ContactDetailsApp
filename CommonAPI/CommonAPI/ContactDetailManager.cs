using System;
using System.Collections.Generic;
using System.Linq;
using CommonAPI.Models;

namespace CommonAPI
{
    public class ContactDetailManager
    {
        private ContactAppDatabaseEntities dbContext;

        public ContactDetailManager()
        {
            dbContext = new ContactAppDatabaseEntities();
            
        }

        public List<ContactDetail> GetAllContactDetails()
        {
            return dbContext.ContactDetails.ToList();
        }

        public ContactDetail GetContactDetail(Guid contactId)
        {
            return dbContext.ContactDetails.FirstOrDefault(c => c.Id == contactId);
        }

        public void SaveContactDetail(ContactDetail contactDetail)
        {
            var existingContact = GetContactDetail(contactDetail.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contactDetail.FirstName;
                existingContact.LastName = contactDetail.LastName;
                existingContact.Email = contactDetail.Email;
                existingContact.PhoneNumber = contactDetail.PhoneNumber;
                existingContact.Status = contactDetail.Status;
            }
            else
            {
                if (contactDetail.Id== Guid.Empty)
                    contactDetail.Id = Guid.NewGuid();

                dbContext.ContactDetails.Add(contactDetail);
            }
            dbContext.SaveChanges();
        }

        public void DeactivateContact(Guid contactId)
        {
            var existingContact = GetContactDetail(contactId);
            if (existingContact != null)
            {
                existingContact.Status = false;
                dbContext.SaveChanges();
            }
        }

        public void DeleteContactDetail(Guid contactId)
        {
            var existingContact = GetContactDetail(contactId);
            if (existingContact != null)
            {
                dbContext.ContactDetails.Remove(existingContact);
            }
        }
    }
}
