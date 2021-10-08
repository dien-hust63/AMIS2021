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
    public class CommodityRepository : BaseRepository<Commodity>, ICommodityRepository
    {
        #region Constructor
        public CommodityRepository(IConfiguration configuration) : base(configuration)
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
        public object GetCommodityFilterPaging(string searchData, int pageIndex, int pageSize)
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var commodityFilter = searchData == null ? string.Empty : searchData;
                dynamicParameters.Add("@search_data", commodityFilter);
                dynamicParameters.Add("@offset", (pageIndex - 1) * pageSize);
                dynamicParameters.Add("@page_size", pageSize);
                var sql = "select * from  public.func_get_commodity_paging_filter(@search_data) limit @page_size offset @offset;";
                sql += "select count(*) from (select * from  public.func_get_commodity_paging_filter(@search_data)) as filtertable;";

                var response = _dbConnection.QueryMultiple(sql, param: dynamicParameters, commandType: CommandType.Text);

                //var vmodel = Activator.CreateInstance<Employee>();
                var commodity = response.Read<Commodity>().ToList();
                var totalRecord = response.Read<int>().FirstOrDefault();
                var totalPage = Math.Ceiling((double)totalRecord / pageSize);

                if (commodity.Count() > 0)
                {
                    for(int i=0; i< commodity.Count(); ++i)
                    {
                        var item = commodity[i];
                        var commodityId = item.commodity_id;
                        DynamicParameters dynamicParameters1 = new DynamicParameters();

                        dynamicParameters1.Add("@commodity_id", commodityId);
                        var sql2 = "select * from view_commodity_unit vcu2 where vcu2.commodity_id = @commodity_id order by vcu2.is_main_unit desc";
                        var units = _dbConnection.Query<CommodityUnit>(sql2, param: dynamicParameters1, commandType: CommandType.Text).ToList();
                        commodity[i].units = units;
                    }
                }
                var result = new
                {
                    Commoditys = commodity,
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
        public Commodity getNewCode()
        {
            using (_dbConnection = new NpgsqlConnection(_connectionString))
            {


                var sqlCommand = "select * from public.commodity av order by cast( public.func_extract_number(av.commodity_code) as int) DESC LIMIT 1";
                var content = _dbConnection.Query<Commodity>(sqlCommand).Single();
                return content;
            }
        }

        /// <summary>
        /// Thêm mới hàng hoá
        /// </summary>
        /// <param name="commodityData"></param>
        /// <returns></returns>
        /// CreatedBy: nvdien(04/10/2021)
        public object InsertCommodity(Commodity commodityData)
        {
            NpgsqlConnection connection = null;
            IDbTransaction transaction = null;
            try
            {
                connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                //Thêm hàng hóa

                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = commodityData.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(commodityData);
                    dynamicParameters.Add($"@{propName}", propValue);

                }
                var funcInsertMaster = $"func_insert_commodity";
                var response = connection.Execute(funcInsertMaster, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                //Thêm mới các đơn vị tính
                var units = commodityData.units;
                if(units.Count() > 0)
                {
                    for (int i = 0; i < units.Count(); ++i)
                    {
                        var unit = units[i];
                        DynamicParameters dynamicParameters2 = new DynamicParameters();
                        var properties2 = unit.GetType().GetProperties();
                        foreach (var property in properties2)
                        {
                            if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                            var propName = property.Name;
                            var propValue = property.GetValue(unit);
                            dynamicParameters2.Add($"@{propName}", propValue);

                        }
                        var funcInsertCommodityUnit = $"func_insert_commodityandunit";
                        var rowEffect = connection.Execute(funcInsertCommodityUnit, param: dynamicParameters2, commandType: CommandType.StoredProcedure);
                    }
                }
                transaction.Commit();
                return commodityData;
            }
            catch (Exception)
            {

                if (transaction != null)

                {
                    transaction.Rollback();
                    return null;
                }
                throw;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        #endregion
    }
}