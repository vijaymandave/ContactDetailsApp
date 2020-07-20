using System;
using System.Collections.Generic;
using System.Web.Http;

using CommonAPI;
using CommonAPI.Models;

namespace WebApiApp.Controllers
{
    public class ContactController : ApiController
    {
        // GET: api/Contact
        public IEnumerable<ContactDetail> Get()
        {
            return new ContactDetailManager().GetAllContactDetails();
        }

        // GET: api/Contact/5
        public ContactDetail Get(Guid id)
        {
            return new ContactDetailManager().GetContactDetail(id);
        }

        // POST: api/Contact
        public void Post(ContactDetail contactDetail)
        {
            new ContactDetailManager().SaveContactDetail(contactDetail);
        }

        // PUT: api/Contact/5
        public void Put(ContactDetail contactDetail)
        {
            new ContactDetailManager().SaveContactDetail(contactDetail);
        }

        // DELETE: api/Contact/5
        public void Delete(Guid id)
        {
            new ContactDetailManager().DeactivateContact(id);
        }
    }
}
