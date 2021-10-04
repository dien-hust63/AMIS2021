using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class AccountObjectRepository : BaseRepository<AccountObject>, IAccountObjectRepository
    {
        #region Constructor
        public AccountObjectRepository(IConfiguration configuration) : base(configuration)
        {

        }

       
        #endregion
        /// <summary>
        /// lọc và phân trang đối tượng theo mã và tên đối tượng
        /// </summary>
        /// <param name="searchData">Từ khóa tìm kiếm</param>
        /// <param name="pageIndex">index trang</param>
        /// <param name="pageSize">số bản ghi trên trang</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(24/9/2021)
        public object getAccountObjectPagingFilter(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var objectFilter = searchData == null ? string.Empty : searchData;

                dynamicParameters.Add("@search_data", objectFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);

                var sql = "select * from  public.func_get_accountobject_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_accountobject_paging_filter(@search_data)) as filtertable;";
                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var accountObjects = response.Read<AccountObject>();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    AccountObjects = accountObjects,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,

                };
                return result;
            }
        }


        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        public AccountObject getNewCode()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {


                var sqlCommand = "select * from public.accountobject av order by cast( public.func_extract_number(av.account_object_code) as int) DESC LIMIT 1";
                var content = _dbConnection.Query<AccountObject>(sqlCommand).Single();
                return content;
            }
        }

    }
}
