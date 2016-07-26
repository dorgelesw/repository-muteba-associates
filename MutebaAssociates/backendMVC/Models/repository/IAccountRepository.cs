using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backendMVC.Models.repository
{
    interface IAccountRepository
    {
        List<dal.Account> getAccountList();
    }
}
