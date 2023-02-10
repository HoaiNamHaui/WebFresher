using Dapper;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.EmployeeDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewEmployeeCode()
        {
            string employeeCodeMax = _employeeDL.GetMaxEmployeeCode();
            string Code = employeeCodeMax.Replace("NV-", "");
            string newEmployeeCode = "NV-" + (int.Parse(Code) + 1);
            return newEmployeeCode;
        }

        /// <summary>
        /// Tìm nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>đối tượng nhân viên tìm được</returns>
        public Employee GetEmployeeById(Guid id)
        {
            Employee employee = new Employee();
            employee = _employeeDL.GetEmployeeById(id);
            return employee;
        }

        /// <summary>
        /// API lấy danh sách nhân viên lọc theo trang
        /// </summary>
        /// <returns>Danh sách nhân viên</returns
        public PagingResult<Employee> GetEmployeesByFilter(int pageNumber, int pageSize, string keyword)
        {
            var result = new PagingResult<Employee>();
            result = _employeeDL.GetEmployeesByFilter(pageNumber, pageSize, keyword);
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
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// 1: insert thành công
        /// 0: insert thất bại
        /// </returns>
        public dynamic InsertEmployee(Employee employee)
        {
            // Validate
            var validateResult = ValidateEmployee(employee);
            
            // Check trùng mã nhân viên
            if(_employeeDL.GetByEmployeeCode(employee.EmployeeCode) > 0)
            {
                validateResult.IsSuccess = false;
                validateResult.ListError.Add(Resource.DuplicateCode);
                return validateResult;
            }

            // Thêm dữ liệu cần thiết
            employee.CreatedDate = DateTime.Now;
            employee.ModifiedDate = DateTime.Now;
            employee.EmployeeId = Guid.NewGuid();

            // Xử lí kết quả trả về
            if (validateResult.IsSuccess)
            {
                var numberOfAffectedRows = _employeeDL.InsertEmployee(employee);
                return numberOfAffectedRows;
            }
            else
            {
                return validateResult;
            }
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>
        /// 1: Sửa thành công
        /// 0: Sửa thất bại
        /// </returns>
        public dynamic UpdateEmployee(Guid EmployeeId, Employee employee)
        {
            var validateResult = ValidateEmployee(employee);
            var existEmployee = _employeeDL.GetEmployeeById(EmployeeId);
            if(existEmployee == null)
            {
                validateResult.IsSuccess = false;
                return validateResult;
            }
            //Thêm dữ liệu
            employee.ModifiedDate = DateTime.Now;
            employee.EmployeeId = EmployeeId;
            if (validateResult.IsSuccess)
            {
                return _employeeDL.UpdateEmployee(EmployeeId, employee);
            }
            else
            {
                return validateResult;
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns></returns>
        public int DeleteEmployee(Guid EmployeeId)
        {
            return _employeeDL.DeleteEmployee(EmployeeId);
        }

        /// <summary>
        /// Validate thông tin
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên</param>
        /// <returns>
        /// kết quả validate
        /// </returns>
        private ValidateResult ValidateEmployee(Employee employee)
        {
            ValidateResult validateResult = new ValidateResult();
            validateResult.IsSuccess = true;

            // Lấy toàn bộ prop của Class Employee
            var properties = typeof(Employee).GetProperties();

            // Kiểm tra xem propertities nào có attribute là Require
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(employee);
                var requiredAttribute = (RequiredAttribute)prop.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && string.IsNullOrEmpty(propValue.ToString()))
                {
                    validateResult.ListError.Add(requiredAttribute.ErrorMessage);
                    validateResult.IsSuccess = false;
                    return validateResult;
                }
            }
            // Tuổi trên 18
            if (employee.DateOfBirth != null)
            {
                DateTime now = DateTime.Now;

                if (now.Year - employee.DateOfBirth.Value.Year < 18)
                {
                    validateResult.ListError.Add(Resource.DateOfBirthInvalid);
                    validateResult.IsSuccess = false;
                }

            }

            // Số điện thoại không đúng định dạng
            if (!string.IsNullOrEmpty(employee.PhoneNumber))
            {
                string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                if (!Regex.IsMatch(employee.PhoneNumber.Trim(), motif))
                {
                    validateResult.ListError.Add(Resource.PhoneNumberIncorrectFormat);
                    validateResult.IsSuccess = false;
                }
            }

            // Email không đúng định dạng
            if (!string.IsNullOrEmpty(employee.Email))
            {
                string motif = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (!Regex.IsMatch(employee.Email.Trim(), motif))
                {
                    validateResult.ListError.Add(Resource.EmailIncorrectFormat);
                    validateResult.IsSuccess = false;
                }
            }
            return validateResult;
        }
    }
}
