using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Enum;
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
    public class AccountVoucherService : BaseService<AccountVoucher>, IAccountVoucherService
    {
        #region Declare
        IAccountVoucherRepository _accounVoucherRepository;
        IAccountVoucherDetailRepository _accountVoucherDetailRepository;
        #endregion

        #region Constructor
        public AccountVoucherService(IBaseRepository<AccountVoucher> baseRepository, IAccountVoucherRepository accounVoucherRepository, IAccountVoucherDetailRepository accountVoucherDetailRepository) : base(baseRepository)
        {
            _accounVoucherRepository = accounVoucherRepository;
            _accountVoucherDetailRepository = accountVoucherDetailRepository;
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

        /// <summary>
        /// Lấy mã chứng từ mới
        /// </summary>
        /// <returns></returns>
        public ServiceResult getNewVoucherCode()
        {
            try
            {
                var serviceResult = new ServiceResult();
                var voucher = _accounVoucherRepository.getNewVoucherCode();
                var currentVoucherCode = voucher.voucher_code;
                var numberString = Regex.Match(currentVoucherCode, @"\d+").Value;
                int numberCode = Int32.Parse(numberString);
                numberCode = numberCode + 1;
                numberString = numberCode.ToString();
                var numberStringLength = numberString.Length;
                var newVoucherCode = "BK";
                if (numberStringLength < 6)
                {
                    for (int i = 0; i < 6 - numberStringLength; ++i)
                    {
                        newVoucherCode = newVoucherCode + "0";
                    }
                }
                newVoucherCode = newVoucherCode + numberString;
                serviceResult.Data = newVoucherCode;
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Ghi sổ nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(28/09/2021)
        public ServiceResult mentionMany(List<Guid> entityIds)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _accounVoucherRepository.mentionMany(entityIds);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Update,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Bỏ ghi nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(28/09/2021)
        public ServiceResult unMentionMany(List<Guid> entityIds)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var rowEffects = _accounVoucherRepository.unMentionMany(entityIds);
                serviceResult.Data = new
                {
                    rowEffects = rowEffects,
                    messages = Resources.ResourceVN.Success_Update,
                };
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Thêm mới phiếu nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(30/09/2021)
        public ServiceResult addAccountVoucher(AccountVoucherData data)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var accountVoucher = data.in_inward;
                var accountVoucherId = Guid.NewGuid();
                accountVoucher.accountvoucher_id = accountVoucherId;
                // Thêm vào bảng chính
                var voucherResult = _baseRepository.Insert(accountVoucher);
                // Thêm vào hàng tiền
                var accountVoucherDetails = data.in_inward_detail;
                for (int i = 0; i < accountVoucherDetails.Count(); i++)
                {
                    var accountVoucherDetail = accountVoucherDetails[i];
                    accountVoucherDetail.accountvoucher_id = accountVoucherId;
                    _accountVoucherDetailRepository.Insert(accountVoucherDetail);
                }
                serviceResult.Data = Resources.ResourceVN.Success_Insert;

                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Chỉnh sửa phiếu nhập
        /// </summary>
        /// <param name="accountVoucherID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(30/09/2021)
        public ServiceResult updateAccountVoucher(Guid accountVoucherID, AccountVoucherData data)
        {
            try
            {
                var serviceResult = new ServiceResult();
                var accountVoucher = data.in_inward;
                // Thêm vào bảng chính
                _baseRepository.Update(accountVoucher, accountVoucherID);
                // Thêm vào hàng tiền
                var accountVoucherDetails = data.in_inward_detail;
                for (int i = 0; i < accountVoucherDetails.Count(); i++)
                {
                    var accountVoucherDetail = accountVoucherDetails[i];
                    var accountVoucherDetailId = accountVoucherDetail.accountvoucherdetail_id;
                    var state = accountVoucherDetail.state;

                    switch (state)
                    {
                        case (int)Mode.Add:
                            _accountVoucherDetailRepository.Insert(accountVoucherDetail);
                            break;
                        case (int)Mode.Update:
                            _accountVoucherDetailRepository.Update(accountVoucherDetail, accountVoucherDetailId);
                            break;
                        case (int)Mode.Delete:
                            _accountVoucherDetailRepository.Delete(accountVoucherDetailId);
                            break;
                    }
                }
                serviceResult.Data = Resources.ResourceVN.Success_Update;
                //serviceResult.Data = _accounVoucherRepository.updateAccountVoucher(accountVoucherID,data);

                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
