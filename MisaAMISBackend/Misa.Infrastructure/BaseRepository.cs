using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Interfaces.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region DECLARE
        protected IConfiguration _configuration;
        protected IDbConnection _dbConnection;
        protected string _connectionString = string.Empty;
        private string _className;

        #endregion

        #region CONSTRUCTOR
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("MisaCukcukConnection");
            _className = typeof(TEntity).Name;
        }


        #endregion

        #region METHOD
        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public int Delete(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {_className} WHERE {_className}Id = @{_className}Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_className}Id", entityId);
                var rowEffects = _dbConnection.Execute(sqlCommand, param: parameters);
                return rowEffects;
            }

        }

        /// <summary>
        /// Xóa nhiều 
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        public int DeleteMultiple(string entityIds)
        {
            var entityIdList = entityIds.Split('$');
            string tmp = "(";
            foreach(var entityId in entityIdList)
            {
                tmp += $"'{entityId}',";
            }
            tmp = tmp.Remove(tmp.Length - 1);
            tmp += ")";
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {_className} WHERE {_className}Id IN {tmp}";
                var rowEffects = _dbConnection.Execute(sqlCommand);
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public IEnumerable<TEntity> GetAllEntities()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} ORDER BY CreatedDate DESC";
                var entities = _dbConnection.Query<TEntity>(sqlCommand);
                return entities;
            }

        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021) 
        public TEntity GetEntityById(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {_className}Id = @{_className}Id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{_className}Id", entityId);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        /// <summary>
        /// Lấy thông tin theo property
        /// </summary>
        /// <param name="propName">Tên property</param>
        /// <param name="propValue">Gía trị property</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(19/8/2021)
        /// ModifiedBy: nvdien(19/8/2021)
        public TEntity GetEntityByProperty(string propName, object propValue)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"SELECT * from {_className} WHERE {propName} = @{propName}";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{propName}", propValue);
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: dynamicParameters);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public int Insert(TEntity entity)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    dynamicParameters.Add($"@{propName}", propValue);

                }
                var proceduce = $"Proc_Insert{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public int Update(TEntity entity, Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                var properties = entity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(MisaNotMap), false)) continue;
                    var propName = property.Name;
                    var propValue = property.GetValue(entity);
                    var propId = $"{_className}Id";
                    if (propName == propId)
                    {
                        dynamicParameters.Add($"{propName}", entityId);
                    }
                    else
                    {
                        dynamicParameters.Add($"{propName}", propValue);
                    }
                }

                var proceduce = $"Proc_Update{_className}";
                var rowEffects = _dbConnection.Execute(proceduce, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }


        #endregion
    }
}
