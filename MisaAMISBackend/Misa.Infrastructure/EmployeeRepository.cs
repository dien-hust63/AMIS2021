using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructor
        public EmployeeRepository(IConfiguration configuration):base(configuration)
        {
        }
        #endregion

        #region Method

        /// <summary>
        /// Lọc và phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="employeeFilter">giá trị tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// author: nvdien(27/8/2021)
        /// modifiedBy: nvdien(27/8/2021)
        public object GetEmployeeFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@EmployeeFilter", employeeFilter);
                dynamicParameters.Add("@PageIndex", pageIndex);
                dynamicParameters.Add("@PageSize", pageSize);
                dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var proceduce = "Proc_GetEmployeeFilterPaging";
                var employees = _dbConnection.Query(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalRecord = dynamicParameters.Get<int>("TotalRecord");
                var totalPage = dynamicParameters.Get<int>("TotalPage");
                var result = new
                {
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                    Employees = employees,
                };
                return result;
            }
        }

        /// <summary>
        /// Sinh mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public string GetNewEmployeeCode()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var proceduce = "Proc_GetNewEmployeeCode";

                var newEmployeecCode = _dbConnection.QueryFirstOrDefault<string>(proceduce, commandType: CommandType.StoredProcedure);
                return newEmployeecCode;
            }
        }

        /// <summary>
        /// Export thông tin nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public IEnumerable<Employee> ExportEmployee()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var proceduce = "Proc_ExportEmployee";
                var employees = _dbConnection.Query<Employee>(proceduce, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }

        #endregion

    }
}
