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
    public class CommodityGroupService : BaseService<CommodityGroup>, ICommodityGroupService
    {

        #region Declare
        ICommodityGroupRepository _commodityGroupRepository;
        #endregion

        public CommodityGroupService(IBaseRepository<CommodityGroup> baseRepository, ICommodityGroupRepository commodityGroupRepository) : base(baseRepository)
        {
            _commodityGroupRepository = commodityGroupRepository;
        }

        /// <summary>
        /// Lọc và phân trang đơn vị
        /// </summary>
        /// <param name="search_data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetCommodityGroupFilterPaging(string search_data, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _commodityGroupRepository.GetCommodityGroupFilterPaging(search_data, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Lấy nhóm hàng hóa chính
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(5/10/2021)
        /// ModifiedBy: nvdien(5/10/201)
        public ServiceResult GetMainGroup()
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _commodityGroupRepository.GetMainGroup();
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
