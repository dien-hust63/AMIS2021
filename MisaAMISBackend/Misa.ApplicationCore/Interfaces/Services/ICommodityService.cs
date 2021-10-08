﻿using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Interfaces.Services
{
    public interface ICommodityService : IBaseService<Commodity>
    {
        /// <summary>
        /// Lọc và phân trang hàng hóa
        /// </summary>
        /// <param name="searchData">chuỗi tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetCommodityFilterPaging(string searchData, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy mã hàng hóa mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        public ServiceResult getNewCode();

        /// <summary>
        /// Thêm mới hàng hóa
        /// </summary>
        /// <param name="commodityData"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        /// ModifiedBy: nvdien(24/9/2021)
       public ServiceResult InsertCommodity(Commodity commodityData);
    }
}
