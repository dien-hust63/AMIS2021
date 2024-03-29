﻿using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Entities;
using Misa.ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Services
{

    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region DECLARE
        protected IBaseRepository<TEntity> _baseRepository;
        string _className;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _className = typeof(TEntity).Name;
        }

        #endregion

        #region Method

        /// <summary>
        /// Xóa theo Id
        /// </summary>
        /// <param name="entityId">Id </param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult Delete(Guid entityId)
        {
            var serviceResult = new ServiceResult();
            var rowEffects = _baseRepository.Delete(entityId);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Delete,
            };
            return serviceResult;
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="entityIds">chuỗi chứa các Id</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult DeleteMultiple(string entityIds)
        {
            var serviceResult = new ServiceResult();
            var rowEffects = _baseRepository.DeleteMultiple(entityIds);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Delete,
            };
            return serviceResult;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>  
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetAllEntities()
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _baseRepository.GetAllEntities();
            return serviceResult;
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult GetEntityById(Guid entityId)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _baseRepository.GetEntityById(entityId);
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity">Thông tin được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        public ServiceResult Insert(TEntity entity)
        {
            var serviceResult = new ServiceResult();
            //Validate dữ liệu
            //Validate chung
            var validateData = ValidateData(entity);
            if (validateData != null)
            {
                serviceResult.IsValid = false;
                serviceResult.Data = validateData;
                return serviceResult;
            }
            //Validate riêng
            var validateCustom = ValidateCustom(entity);
            if (validateCustom != null)
            {
                serviceResult.IsValid = false;
                serviceResult.Data = validateCustom;
                return serviceResult;
            }
            //Thêm dữ liệu
            var rowEffects = _baseRepository.Insert(entity);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Insert,
            };
            return serviceResult;
        }

        /// <summary>
        /// Sửa thông tin
        /// </summary>
        /// <param name="entity">Thông tin cần sửa</param>
        /// <param name="entityId">Id </param>
        /// <returns>số bản ghi được sửa</returns>
        /// CreatedBy: nvdien(17/8/2021)
        /// ModifiedBy: nvdien(17/8/2021)
        public ServiceResult Update(TEntity entity, Guid entityId)
        {
            var serviceResult = new ServiceResult();
            //Validate dữ liệu
            //Validate chung
            var validateData = ValidateData(entity, entityId);
            if (validateData != null)
            {
                serviceResult.IsValid = false;
                serviceResult.Data = validateData;
                return serviceResult;
            }
            //Validate riêng
            var validateCustom = ValidateCustom(entity);
            if (validateCustom != null)
            {
                serviceResult.IsValid = false;
                serviceResult.Data = validateCustom;
                return serviceResult;
            }
            //Thêm dữ liệu
            var rowEffects = _baseRepository.Update(entity, entityId);
            serviceResult.Data = new
            {
                rowEffects = rowEffects,
                messages = Resources.ResourceVN.Success_Update,
            };
            return serviceResult;
        }

        #region Private Method


        /// <summary>
        /// Validate dữ liệu khi thêm mới
        /// </summary>
        /// <param name="entity">thông tin cần validate</param>
        /// <returns>error object nếu dữ liệu không thỏa mãn, null nếu thỏa mãn</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        private object ValidateData(TEntity entity, Guid? entityId = null)
        {
            //Kiểm tra các trường bắt buộc nhập
            var checkRequiredField = CheckRequiredField(entity);
            if (checkRequiredField != null)
            {
                return checkRequiredField;
            }
            //Kiểm tra các trường không được phép trùng
            var checkUniqueField = CheckDuplication(entity, entityId); 
            if (checkUniqueField != null)
            {
                return checkUniqueField;
            }

            //Kiểm tra email(nếu có) có đúng định dạng không
            var checkEmailSyntax = CheckEmailSyntax(entity);
            if (checkEmailSyntax != null)
            {
                return checkEmailSyntax;
            }
            //Thỏa mãn hết
            return null;
        }


        /// <summary>
        /// Validate dữ liệu riêng
        /// </summary>
        /// <param name="entity">thông tin cần validate</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        protected virtual object ValidateCustom(TEntity entity)
        {
            return null;
        }

        /// <summary>
        /// Kiểm tra các trường bắt buộc nhập
        /// </summary>
        /// <param name="entity">Thông tin kiểm tra</param>
        /// <returns>object error nếu có trường để trống, null nếu thỏa mã</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        private object CheckRequiredField(TEntity entity)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                var propMisaRequired = property.GetCustomAttributes(typeof(MisaRequired), true);
                var propMisaDislayName = property.GetCustomAttributes(typeof(MisaDisplayName), true);
                if (propMisaRequired.Length > 0)
                {
                    var fieldName = propMisaDislayName.Length > 0 ? (propMisaDislayName[0] as MisaDisplayName).FieldName : property.Name;
                    if (property.GetValue(entity) == null || string.IsNullOrEmpty(property.GetValue(entity).ToString()) || property.GetValue(entity).ToString() == Guid.Empty.ToString())
                    {
                        var errorObj = new
                        {
                            devMessage = string.Format(Resources.ResourceVN.Exception_Required, property.Name),
                            userMsg = string.Format(Resources.ResourceVN.Exception_Required, fieldName),
                            errorCode = "MISA01",
                            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                        };
                        return errorObj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra những trường không được phép trùng
        /// </summary>
        /// <param name="entity">Thông tin cần kiểm tra</param>
        /// <returns>error object nếu không thỏa mãn, null nếu thỏa mãn</returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        private object CheckDuplication(TEntity entity, Guid? entityId = null)
        {
            var properties = entity.GetType().GetProperties();
            //Kiểm tra xem entity có trường yêu cầu không được phép trùng không
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(MisaUnique), false))
                {
                    TEntity entityResult = _baseRepository.GetEntityByProperty(property.Name, property.GetValue(entity));
                    if (entityResult != null)
                    {
                        var entityResultId = entityResult.GetType().GetProperty($"{_className}Id").GetValue(entityResult).ToString();
                        var isSelf = entityResultId.Equals(entityId.ToString());
                        //entityId == null : trường hợp thêm mới
                        //entityId != null : trường hợp chỉnh sửa
                        if (entityId == null || entityId != null && isSelf == false)
                        {
                            var propMisaDislayName = property.GetCustomAttributes(typeof(MisaDisplayName), true);
                            var fieldName = propMisaDislayName.Length > 0 ? (propMisaDislayName[0] as MisaDisplayName).FieldName : property.Name;
                            var errorObj = new
                            {
                                devMessage = string.Format(Resources.ResourceVN.Exception_Duplication, property.Name),
                                userMsg = string.Format(Resources.ResourceVN.Exception_Duplication, fieldName),
                                errorCode = "MISA01",
                                moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                                traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                            };
                            return errorObj;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra định dạng Email
        /// </summary>
        /// <param name="entity">thông tin cần kiểm tra</param>
        /// <returns></returns>
        /// CreatedBy: nvdien(27/8/2021)
        /// ModifiedBy: nvdien(27/8/2021)
        private object CheckEmailSyntax(TEntity entity)
        {
            var propEmail = entity.GetType().GetProperty("Email");
            if (propEmail != null)
            {
                var email = propEmail.GetValue(entity);
                if (email == null || email == "") return null;
                var regexEmail = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                var isValidEmail = Regex.IsMatch(email.ToString(), regexEmail);
                if (isValidEmail == false)
                {
                    var errorObj = new
                    {
                        devMessage = Resources.ResourceVN.Exception_EmployeeEmail,
                        userMsg = Resources.ResourceVN.Exception_EmployeeEmail,
                        errorCode = "MISA01",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return errorObj;
                }
            }
            return null;
        }


        #endregion
        #endregion
    }
}
