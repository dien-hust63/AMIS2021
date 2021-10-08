using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICommodityGroupService : IBaseService<CommodityGroup>
    {
        /// <summary>
        /// Lọc và phân trang nhóm hàng hóa
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        ServiceResult GetCommodityGroupFilterPaging(string searchData, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy nhóm hàng hóa chính
        /// </summary>
        /// <returns></returns>
        ServiceResult GetMainGroup();
    }
}
