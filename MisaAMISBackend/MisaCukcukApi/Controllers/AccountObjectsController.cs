using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class AccountObjectsController : BaseEntityController<AccountObject>
    {
        #region Declare
        IAccountObjectService _accountObjectService;
        #endregion
        public AccountObjectsController(IBaseService<AccountObject> baseService, IAccountObjectService accountObjectService) : base(baseService)
        {
            _accountObjectService = accountObjectService;
        }

        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult GetAccountObjectPagingFilter([FromQuery] string searchData, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _accountObjectService.getAccountObjectPagingFilter(searchData, pageIndex, pageSize);
                return Ok(serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(3/10/2021)
        [HttpGet("NewCode")]
        public IActionResult getNewCode()
        {
            try
            {
                var serviceResult = _accountObjectService.getNewCode();
                return Ok(serviceResult.Data);
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.Exception_ErrorMsg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errorObj);
            }
        }

    }
}
