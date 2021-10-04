using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// lọc và tìm kiếm khách hàng
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedByL nvdien(3/10/2021)
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

        /// <summary>
        /// Lấy mã khách hàng mới
        /// </summary>
        /// <returns></returns>
        public ServiceResult getNewCode()
        {
            try
            {
                var serviceResult = new ServiceResult();
                var item = _accounObjectRepository.getNewCode();
                var currentVoucherCode = item.account_object_code;
                var numberString = Regex.Match(currentVoucherCode, @"\d+").Value;
                int numberCode = Int32.Parse(numberString);
                numberCode = numberCode + 1;
                numberString = numberCode.ToString();
                var numberStringLength = numberString.Length;
                var newAccountObjectCode = "KH";
                if (numberStringLength < 6)
                {
                    for (int i = 0; i < 6 - numberStringLength; ++i)
                    {
                        newAccountObjectCode = newAccountObjectCode + "0";
                    }
                }
                newAccountObjectCode = newAccountObjectCode + numberString;
                serviceResult.Data = newAccountObjectCode;
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
