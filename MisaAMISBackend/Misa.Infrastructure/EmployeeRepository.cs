using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces;
using MySqlConnector;
using Npgsql;
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
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@search_data", employeeFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);
                var sql = "select * from  public.func_get_employee_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_employee_paging_filter(@search_data)) as filtertable;";

                var response = _dbConnection.QueryMultiple(sql, param:dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var employees = response.Read<Employee>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    Employees = employees,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
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
        public Employee GetNewEmployeeCode()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                var sqlCommand = "select * from public.employee av order by cast( public.func_extract_number(av.employee_code) as int) DESC LIMIT 1";
                var content = _dbConnection.Query<Employee>(sqlCommand).Single();
                return content;

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

        /// <summary>
        /// Kiểm tra mã nhân viên có tồn tại không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        // CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public int CheckEmployeeCodeExist(string employeeCode)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = "SELECT EXISTS(SELECT * from employee WHERE EmployeeCode= @EmployeeCode);";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeCode", employeeCode);
                var result = _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters);
                return result;
            }
        }
        #endregion

    }
}
