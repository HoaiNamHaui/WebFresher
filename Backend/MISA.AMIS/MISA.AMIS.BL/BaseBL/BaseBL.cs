using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm</param>
        /// <returns>trả về service result</returns>
        public ServiceResult InsertRecord(T record)
        {
            try
            {
                // Validate
                var validateResult = ValidateRequestData(record);
                // Thất bại
                if (!validateResult.IsSuccess)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.BAD_REQUEST,
                        message = Resource.DataInvalid,
                        Data = validateResult
                    };
                }
                // Thành công -> gọi vào DL để chạy Stored
                record = AddProperties(record, true);
                var numberOfAffectedRows = _baseDL.InsertRecord(record);

                // Xử lý kết quả trả về
                if (numberOfAffectedRows > 0)
                {
                    return new ServiceResult { IsSuccess = true };
                }
                else
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.INSERT_FAILED,
                        message = Resource.ServerError
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }        
        
        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần sửa</param>
        /// <returns>trả về service result</returns>
        public ServiceResult UpdateRecord(T record, Guid recordId)
        {
            // Validate
            var validateResult = ValidateRequestData(record);
            // Thất bại
            if (!validateResult.IsSuccess)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.BAD_REQUEST,
                    message = Resource.DataInvalid,
                    Data = validateResult
                };
            }
            // Thành công -> gọi vào DL để chạy Stored
            record = AddProperties(record, false);
            var numberOfAffectedRows = _baseDL.UpdateRecord(record, recordId);

            // Xử lý kết quả trả về
            if(numberOfAffectedRows > 0)
            {
                return new ServiceResult { IsSuccess = true };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.INSERT_FAILED,
                    message = Resource.ServerError
                };
            }
        }

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <returns>Danh sách</returns
        public PagingResult<T> GetByFilter(int pageNumber, int pageSize, string keyword)
        {
            var result = new PagingResult<T>();
            result = _baseDL.GetByFilter(pageNumber, pageSize, keyword);
            // TÍnh toán số bản ghi, tổng số trang
            if (result.TotalRecord % pageSize == 0)
            {
                result.TotalPage = result.TotalRecord / pageSize;
            }
            else
            {
                result.TotalPage = (result.TotalRecord / pageSize) + 1;
            }
            return result;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>bản ghi tìm được</returns>
        public T GetById(Guid id)
        {
            return _baseDL.GetById(id);
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>1 nếu thành công</returns>
        public int DeleteRecord(Guid id)
        {
            return _baseDL.DeleteRecord(id);
        }

        /// <summary>
        /// Thêm dữ liệu chung
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isInsert"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected T AddProperties(dynamic entity, bool isInsert = true, Guid? id = null)
        {
            entity.ModifiedDate = DateTime.Now;
            if (isInsert)
            {
                entity.CreatedDate = DateTime.Now;
                entity.GetType().GetProperty($"{typeof(T).Name}Id").SetValue(entity, Guid.NewGuid(), null);
            }
            else entity.GetType().GetProperty($"{typeof(T).Name}Id").SetValue(entity, id, null);

            return entity;
        }

        /// <summary>
        /// Validate chung
        /// </summary>
        /// <param name="record">Đối tượng thêm</param>
        /// <returns>kết quả validate</returns>
        protected virtual ValidateResult ValidateRequestData(T? record)
        {
            ValidateResult validateResult = new ValidateResult();
            validateResult.IsSuccess = true;

            // Lấy toàn bộ prop của Class Employee
            var properties = typeof(Employee).GetProperties();

            // Kiểm tra xem propertities nào có attribute là Require
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(record);
                var requiredAttribute = (RequiredAttribute)prop.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && string.IsNullOrEmpty(propValue.ToString()))
                {
                    validateResult.ListError.Add(requiredAttribute.ErrorMessage);
                    validateResult.IsSuccess = false;
                }
            }
            if(validateResult.ListError.Count > 0)
            {
                return validateResult;
            }
            // Validate riêng
            var result = ValidateCustom(record);
            if (!result.IsSuccess)
            {
                validateResult.IsSuccess = false;
                validateResult.ListError = result.ListError;
            }
            return validateResult;
        }

        /// <summary>
        /// Validate riêng
        /// </summary>
        /// <param name="record">Bản ghi</param>
        /// <returns>validate result</returns>
        protected virtual ValidateResult ValidateCustom(T? record)
        {
            return new ValidateResult { IsSuccess = true };
        }

        #endregion
    }
}
