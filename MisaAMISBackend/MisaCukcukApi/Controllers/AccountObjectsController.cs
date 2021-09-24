using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class AccountObjectsController : BaseEntityController<AccountObject>
    {
        public AccountObjectsController(IBaseService<AccountObject> baseService) : base(baseService)
        {
        }

    }
}
