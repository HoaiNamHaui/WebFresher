using Dapper;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.EmployeeDL;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL):base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public string GetNewEmployeeCode()
        {
            string employeeCodeMax = _employeeDL.GetMaxEmployeeCode();
            string Code = employeeCodeMax.Replace("NV-", "");
            string newEmployeeCode = "NV-" + (int.Parse(Code) + 1);
            return newEmployeeCode;
        }

        /// <summary>
        /// Xuất khẩu dữ liệu ra excel
        /// </summary>
        /// <param name="data">dữ liệu lấy từ DL</param>
        /// <returns>data</returns>
        public List<Employee> ExportToExcel()
        {
            var data = _employeeDL.ExportToExcel();
            return data;
        }

        /// <summary>
        /// Validate custom theo đối tượng cụ thể
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần validate</param>
        /// <returns>kết quả validate</returns>
        /// CreatedBy: NHNam(3/2/2023)
        protected override ValidateResult ValidateCustom(Employee employee, Guid? id)
        {
            ValidateResult validateResult = new ValidateResult();
            validateResult.IsSuccess = true;
            //Trùng mã nhân viên
            var emp = _employeeDL.GetEmployeeByCode(id, employee.EmployeeCode);
            if(emp != null)
            {
                validateResult.ListError.Add(Resource.DuplicateCode);
                validateResult.IsSuccess = false;
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
