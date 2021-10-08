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
    public class UnitService : BaseService<Unit>, IUnitService
    {

        #region Declare
        IUnitRepository _unitRepository;
        #endregion

        public UnitService(IBaseRepository<Unit> baseRepository, IUnitRepository unitRepository) : base(baseRepository)
        {
            _unitRepository = unitRepository;
        }

        /// <summary>
        /// Lọc và phân trang đơn vị tính
        /// </summary>
        /// <param name="search_data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetUnitFilterPaging(string search_data, int pageIndex, int pageSize)
        {
            try
            {
                var serviceResult = new ServiceResult();
                serviceResult.Data = _unitRepository.GetUnitFilterPaging(search_data, pageIndex, pageSize);
                return serviceResult;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
