using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Repository
{
    public interface ICommodityGroupRepository : IBaseRepository<CommodityGroup>
    {
        /// <summary>
        /// Lọc và phân trang nhóm hàng hóa
        /// </summary>
        /// <param name="searchData">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// author: nvdien(27/8/2021)
        /// modifiedBy: nvdien(27/8/2021)
        object GetCommodityGroupFilterPaging(string searchData, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy group chính
        /// </summary>
        /// <returns></returns>
        IEnumerable<CommodityGroup> GetMainGroup();
    }
}
