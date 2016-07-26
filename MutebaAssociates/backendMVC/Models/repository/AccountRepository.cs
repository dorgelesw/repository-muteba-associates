using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backendMVC.Models.repository
{
    public class AccountRepository : IAccountRepository
    {
        public List<dal.Account> getAccountList()
        {
            using (var context = new dal.dbMutebaAssociateEntities())
            {
                var accounts = context.Accounts;
                return accounts.ToList();
            }
        }
    }
}
