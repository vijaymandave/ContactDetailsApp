using CommanAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanAPI
{
    public class LoginManager
    {
        private ContactAppDatabaseEntities dbContext;

        public LoginManager()
        {
            dbContext = new ContactAppDatabaseEntities();
        }

        public void SaveLogin(Login login)
        {
            var existingContact = dbContext.Logins.FirstOrDefault(l => l.Id == login.Id);
            if (existingContact != null)
            {
                existingContact.UserName = login.UserName;
                existingContact.Password = login.Password;
            }
            else
            {
                dbContext.Logins.Add(login);
            }
            dbContext.SaveChanges();
        }

        public bool ValidateLogin(string userName, string password)
        {
            return dbContext.Logins.Any(l => l.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && l.Password == password);
        }
    }
}
