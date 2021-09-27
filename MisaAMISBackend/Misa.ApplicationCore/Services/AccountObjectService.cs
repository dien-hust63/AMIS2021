using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{
    public class AccountObjectService:BaseService<AccountObject>, IAccountObjectService
    {
        #region Declare
        IAccountObjectRepository _accounObjectRepository;
        #endregion
        #region Constructor
        public AccountObjectService(IBaseRepository<AccountObject> baseRepository, IAccountObjectRepository accountObjectVoucherRepository) : base(baseRepository)
        {
            _accounObjectRepository = accountObjectVoucherRepository;
        }
        #endregion
        public ServiceResult getAccountObjectPagingFilter(string searchData, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounObjectRepository.getAccountObjectPagingFilter(searchData,pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
