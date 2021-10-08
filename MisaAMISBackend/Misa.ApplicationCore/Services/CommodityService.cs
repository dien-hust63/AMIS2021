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
    public class CommodityService : BaseService<Commodity>, ICommodityService
    {
        #region Declare
        ICommodityRepository _commodityRepository;
        #endregion

        #region Constructor
        public CommodityService(IBaseRepository<Commodity> baseRepository, ICommodityRepository commodityRepository) : base(baseRepository)
        {
            _commodityRepository = commodityRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search_data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetCommodityFilterPaging(string search_data, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _commodityRepository.GetCommodityFilterPaging(search_data, pageIndex, pageSize);
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
                var item = _commodityRepository.getNewCode();
                var currentCommodityCode = item.commodity_code;
                var numberString = Regex.Match(currentCommodityCode, @"\d+").Value;
                int numberCode = Int32.Parse(numberString);
                numberCode = numberCode + 1;
                numberString = numberCode.ToString();
                var numberStringLength = numberString.Length;
                var newAccountObjectCode = "VT";
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

        /// <summary>
        /// Thêm mới hàng hóa
        /// </summary>
        /// <param name="commodityData"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        /// ModifiedBy: nvdien(24/9/2021)
        public ServiceResult InsertCommodity(Commodity commodityData)
        {
            var serviceResult = new ServiceResult();
            /**Validate dữ liệu*/
            //check các trường bắt buộc nhập
            var validateData = CheckRequiredField(commodityData);
            if (validateData != null)
            {
                serviceResult.IsValid = false;
                serviceResult.Data = validateData;
                return serviceResult;
            }
            //Check trùng mã
            /**thêm phiếu nhập*/
            commodityData.commodity_id = Guid.NewGuid();
            var lengthUnits = commodityData.units.Count();

            for (int i = 0; i < lengthUnits; ++i)
            { 
                commodityData.units[i].commodityandunit_id = Guid.NewGuid();
                commodityData.units[i].commodity_id = commodityData.commodity_id;
            }
            var resultData = _commodityRepository.InsertCommodity(commodityData);
            if (resultData != null)
            {
                serviceResult.Data = new
                {
                    Message = Resources.ResourceVN.Success_Insert,
                    Data = resultData
                };
            }
            else
            {
                serviceResult.IsValid = false;
            }

            return serviceResult;
        }
        #endregion
    }
}
