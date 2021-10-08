using Misa.ApplicationCore.Entities;
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
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {

        #region Declare
        IDepartmentRepository _departmentRepository;
        #endregion

        public DepartmentService(IBaseRepository<Department> baseRepository, IDepartmentRepository departmentRepository) : base(baseRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Lọc và phân trang đơn vị
        /// </summary>
        /// <param name="search_data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetDepartmentFilterPaging(string search_data, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _departmentRepository.GetDepartmentFilterPaging(search_data, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
