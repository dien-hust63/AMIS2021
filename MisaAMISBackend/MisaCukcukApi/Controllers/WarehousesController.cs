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
    public class WarehousesController : BaseEntityController<Warehouse>
    {
        #region Declare
        IWarehouseService _warehouseService;
        #endregion
        #region Constructor
        public WarehousesController(IBaseService<Warehouse> baseService, IWarehouseService warehouseService) : base(baseService)
        {
            _warehouseService = warehouseService;
        }
        #endregion

        /// <summary>
        /// Lọc và phân trang kho
        /// </summary>
        /// <param name="searchData">Dữ liệu lọc</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        [HttpGet("Filter")]
        public IActionResult GetEmployeeFilterPaging([FromQuery] string searchData, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _warehouseService.GetWarehouseFilterPaging(searchData, pageIndex, pageSize);
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
                var serviceResult = _warehouseService.getNewCode();
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
