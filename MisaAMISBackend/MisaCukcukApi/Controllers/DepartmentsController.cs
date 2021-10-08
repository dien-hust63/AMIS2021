﻿using Microsoft.AspNetCore.Mvc;
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
    public class DepartmentsController: BaseEntityController<Department>
    {
        IDepartmentService _departmentService;
        public DepartmentsController(IBaseService<Department> baseService, IDepartmentService departmentService):base(baseService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Lọc và phân trang đơn vị
        /// </summary>
        /// <param name="searchData">Dữ liệu lọc</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(20/8/2021)
        /// ModifiedBy: nvdien(20/8/2021)
        [HttpGet("Filter")]
        public IActionResult GetDepartmentFilterPaging([FromQuery] string searchData, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var serviceResult = _departmentService.GetDepartmentFilterPaging(searchData, pageIndex, pageSize);
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
