using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class DepartmentsController: BaseEntityController<Department>
    {
        public DepartmentsController(IBaseService<Department> baseService):base(baseService)
        {
        }

    }
}
