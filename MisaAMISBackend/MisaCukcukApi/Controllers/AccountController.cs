using Dapper;
using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.Web.Properties;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.Web.Controllers
{
    public class AccountsController : BaseEntityController<Account>
    {
        #region Declare
        IAccountService _accountService;
        #endregion
        public AccountsController(IBaseService<Account> baseService, IAccountService accountService) : base(baseService)
        {
            _accountService = accountService;
        }

        [HttpGet("Filter")]
        public IActionResult GetAccountPagingFilter([FromQuery] string searchData, [FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string type)
        {
            try
            {
                var serviceResult = _accountService.getAccountPagingFilter(searchData, pageIndex, pageSize, type);
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
