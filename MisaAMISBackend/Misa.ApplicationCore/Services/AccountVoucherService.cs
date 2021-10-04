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
                /**Validate dữ liệu*/
                //check các trường bắt buộc nhập
                var accountVoucher = data.in_inward;
                var voucherDetailContent = data.in_inward_detail;
                var lengthDetail = data.in_inward_detail.Count();
                var validateData = CheckRequiredField(accountVoucher);
                if(validateData != null)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = validateData;
                    return serviceResult;
                }
                var voucherDetailService = new AccountVoucherDetailService();
                for(int i =0; i < lengthDetail; ++i)
                {
                    var voucherDetail = voucherDetailContent[i];
                    var validateDataDetail = voucherDetailService.CheckRequiredField(voucherDetail);
                    if(validateDataDetail != null)
                    {
                        serviceResult.IsValid = false;
                        serviceResult.Data = validateDataDetail;
                        return serviceResult;
                    }
                }
                //Check trùng mã
                int isDuplicateCode = checkAccountVoucherCode((int)Mode.Add, accountVoucher.voucher_code);
                if(isDuplicateCode == 0)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = new
                    {
                        code = "MS001",
                        message = Resources.ResourceVN.Exception_Voucher_Code_Duplication
                    };
                    return serviceResult;
                }
                //Check ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ
                if(DateTime.Compare(accountVoucher.mathematics_date, accountVoucher.voucher_date) < 0)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = new
                    {
                        message = Resources.ResourceVN.Exceoption_Voucher_Date
                    };
                    return serviceResult;
                }
                /**thêm phiếu nhập*/
                data.in_inward.accountvoucher_id = Guid.NewGuid();
                
                for (int i=0; i< lengthDetail; ++i)
                {
                    data.in_inward_detail[i].accountvoucher_id = data.in_inward.accountvoucher_id;
                }
                var resultData = _accounVoucherRepository.addAccountVoucher(data);
                if(resultData != null)
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
                /**Validate dữ liệu*/
                #region Validate dữ liệu

                
                //check các trường bắt buộc nhập
                var accountVoucher = data.in_inward;
                var voucherDetailContent = data.in_inward_detail;
                var lengthDetail = data.in_inward_detail.Count();
                var validateData = CheckRequiredField(accountVoucher);
                if (validateData != null)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = validateData;
                    return serviceResult;
                }
                var voucherDetailService = new AccountVoucherDetailService();
                for (int i = 0; i < lengthDetail; ++i)
                {
                    var voucherDetail = voucherDetailContent[i];
                    var validateDataDetail = voucherDetailService.CheckRequiredField(voucherDetail);
                    if (validateDataDetail != null)
                    {
                        serviceResult.IsValid = false;
                        serviceResult.Data = validateDataDetail;
                        return serviceResult;
                    }
                }
                //Check trùng mã
                int isDuplicateCode = checkAccountVoucherCode((int)Mode.Update, accountVoucher.voucher_code, accountVoucherID.ToString());
                if (isDuplicateCode == 0)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = new
                    {
                        code = "MS001",
                        message = Resources.ResourceVN.Exception_Voucher_Code_Duplication
                    };
                    return serviceResult;
                }
                //Check ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ
                if (DateTime.Compare(accountVoucher.mathematics_date, accountVoucher.voucher_date) < 0)
                {
                    serviceResult.IsValid = false;
                    serviceResult.Data = new
                    {
                        message = Resources.ResourceVN.Exceoption_Voucher_Date
                    };
                    return serviceResult;
                }
                #endregion
                /**Sửa phiếu nhập*/

                var resultData = _accounVoucherRepository.updateAccountVoucher(accountVoucherID, data);
                if (resultData != null)
                {
                    serviceResult.Data = new
                    {
                        Message = Resources.ResourceVN.Success_Update,
                        Data = resultData
                    };
                }
                else
                {
                    serviceResult.IsValid = false;
                }

                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Check trùng mã chứng từ
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="accountVoucher"></param>
        /// <returns>0: mã bị trùng, 1: mã không bị trùng, -1: sai tham số truyền</returns>
        /// CreatedBy: nvdien(3/10/2021)
        public int checkAccountVoucherCode(int mode, string accountVoucherCode, string editId = "")
        {
            try
            {
                if (mode == (int)Mode.Add)
                {
                    var voucherList = _accounVoucherRepository.checkVoucherCodeDuplicate(accountVoucherCode);
                    if (voucherList.Count() > 0)
                    {
                        //Mã bị trùng
                        return 0;
                    }
                    //Mã không trùng
                    return 1;
                }
                if (mode == (int)Mode.Update)
                {
                    var voucherList = _accounVoucherRepository.checkVoucherCodeDuplicate(accountVoucherCode);
                    if (voucherList.Count() == 1)
                    {
                        //Kiểm tra xem mã này của chính nó hay của object khác
                        if (!String.IsNullOrEmpty(editId))
                        {
                            var voucherID = Guid.Parse(editId);
                            var voucher = _accounVoucherRepository.GetEntityById(voucherID);
                            if (voucher != null && voucher.voucher_code == accountVoucherCode)
                            {
                                //Không trùng
                                return 1;
                            }
                            else return 0;
                        }
                        return -1;
                    }
                    if (voucherList.Count() < 1)
                    {
                        //Mã không bị trùng
                        return 1;
                    }
                    //Mã bị trùng
                    return 0;
                }
                return -1;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
