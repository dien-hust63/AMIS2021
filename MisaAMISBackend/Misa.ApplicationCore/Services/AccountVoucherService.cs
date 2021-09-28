﻿using Misa.ApplicationCore.Entities;
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
    public class AccountVoucherService : BaseService<AccountVoucher>, IAccountVoucherService
    {
        #region Declare
        IAccountVoucherRepository _accounVoucherRepository;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accounVoucherRepository) : base(baseRepository)
        {
            _accounVoucherRepository = accounVoucherRepository;
        }
        #endregion

        /// <summary>
        /// Lấy chi tiết chứng từ theo Id
        /// </summary>
        /// <param name="accountVoucherID">ID chứng từ</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        public ServiceResult getAccountVoucherDetail(Guid accountVoucherID)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounVoucherRepository.getAccountVoucherDetail(accountVoucherID);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// lọc và phân trang chứng từ
        /// </summary>
        /// <param name="searchData">Từ khóa tìm kiếm</param>
        /// <param name="mentionState">Trạng thái ghi sổ</param>
        /// <param name="voucherType">loại chứng từ</param>
        /// <param name="startDate">ngày lọc đầu</param>
        /// <param name="endDate">ngày lọc cuối</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        public ServiceResult getAccountVoucherPagingFilter(string searchData, int? mentionState, string voucherType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _accounVoucherRepository.getAccountVoucherPagingFilter(searchData, mentionState, voucherType, startDate, endDate, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
