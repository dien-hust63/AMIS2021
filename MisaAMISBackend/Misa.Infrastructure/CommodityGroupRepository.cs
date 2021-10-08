using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
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
    public class CommodityGroupRepository : BaseRepository<CommodityGroup>, ICommodityGroupRepository
    {
        public CommodityGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
    
        /// <summary>
        /// Lọc và phân trang nhóm hàng hóa
        /// </summary>
        /// <param name="searchData"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(5/10/2021)
        /// ModifiedBy: nvdien(5/10/2021)
        public object GetCommodityGroupFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var employeeFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@search_data", employeeFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);
                var sql = "select * from  public.func_get_commoditygroup_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_commoditygroup_paging_filter(@search_data)) as filtertable;";

                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);
                var commodityGroups = response.Read<CommodityGroup>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);
                var result = new
                {
                    CommodityGroups = commodityGroups,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
                return result;
            }
        }

        /// <summary>
        /// Lấy nhóm hàng hóa chính
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: nvdien(5/10/2021)
        /// ModifiedBy: nvdien(5/10/2021)
        public IEnumerable<CommodityGroup> GetMainGroup()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var sql = "select * from public.commoditygroup cg where cg.is_main = 1 order by cg.created_date asc";
                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);
                var commodityGroups = response.Read<CommodityGroup>().ToList();
                return commodityGroups;
            }
        }

      
    }
}
