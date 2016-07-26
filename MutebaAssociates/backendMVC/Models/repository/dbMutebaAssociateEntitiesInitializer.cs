using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using dal;

namespace backendMVC.Models.repository
{
    public class dbMutebaAssociateEntitiesInitializer : DropCreateDatabaseAlways<dbMutebaAssociateEntities>
    {
        protected override void Seed(dbMutebaAssociateEntities context)
        {
            //Create and add a user with his account
            //context.Users.Add(new User
            //{
            //    FirstName = "Muteba",
            //    LastName = "Associate"
            //});

            context.Accounts.Add(new Account
            {
                Login = "muteba@p7c-sarl.com",
                Password = "1234",
                User = new User
                {
                    FirstName = "Muteba",
                    LastName = "Associate"
                }
            });
            context.SaveChanges();
        }
    }
}