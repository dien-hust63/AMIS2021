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
    public class WarehouseService : BaseService<Warehouse>, IWarehouseService
    {
        #region Declare
        IWarehouseRepository _warehouseRepository;
        #endregion

        #region Constructor
        public WarehouseService(IBaseRepository<Warehouse> baseRepository, IWarehouseRepository warehouseRepository) : base(baseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetWarehouseFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _warehouseRepository.GetWarehouseFilterPaging(searchData, pageIndex, pageSize);
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
                var item = _warehouseRepository.getNewCode();
                var currentVoucherCode = item.warehouse_code;
                var numberString = Regex.Match(currentVoucherCode, @"\d+").Value;
                int numberCode = Int32.Parse(numberString);
                numberCode = numberCode + 1;
                numberString = numberCode.ToString();
                var numberStringLength = numberString.Length;
                var newAccountObjectCode = "K";
                if (numberStringLength < 4)
                {
                    for (int i = 0; i < 4 - numberStringLength; ++i)
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
        #endregion
    }
}

